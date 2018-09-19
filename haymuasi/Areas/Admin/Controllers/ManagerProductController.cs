using haymuasi.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;
using System.Data;
using System.IO;

namespace Admin.Controllers
{
    public class ManagerProductController : Controller
    {
        // GET: ManagerProduct
        db cn = new db();
        SqlConnection conn = new SqlConnection();
        public string RenderPartialViewToString(Controller ManagerProduct, string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
            {
                viewName = ManagerProduct.ControllerContext.RouteData.GetRequiredString("action");
            }

            ManagerProduct.ViewData.Model = model;

            using (var sw = new StringWriter())
            {
                // Find the partial view by its name and the current controller context.
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(ManagerProduct.ControllerContext, viewName);

                // Create a view context.
                var viewContext = new ViewContext(ManagerProduct.ControllerContext, viewResult.View, ManagerProduct.ViewData, ManagerProduct.TempData, sw);

                // Render the view using the StringWriter object.
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }
        public ActionResult DisplayProduct(int? page, string searchcode)
        {
            SqlConnection conn = new SqlConnection(cn.conn);
            conn.Open();
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("select * from Product ORDER BY categoryid desc ", conn);
            dr = cmd.ExecuteReader();
            List<Product> list = new List<Product>();
            Product pr = new Product();
            while (dr.Read() == true)
            {
                pr = new Product();
                pr.categoryid = dr["categoryid"].ToString();
                pr.masp = dr["codeproduct"].ToString();
                pr.chitiet = dr["productname"].ToString();
                pr.hinhanh = dr["imagesname"].ToString();
                pr.giasp = Convert.ToUInt32(dr["price"]);
                pr.productid = dr["productid"].ToString();
                pr.size = dr["size"].ToString();
                list.Add(pr);
            }
            if (Session["Admin"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            int pagesize = 10;
            int pagenumber = (page ?? 1);
            return View(list.ToPagedList(pagenumber, pagesize));

        }
        public ActionResult SearchCode(string searchcode, string ac)
        {
            SqlConnection conn = new SqlConnection(cn.conn);
            SqlCommand cmd = new SqlCommand("sp_searchcode", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@keywork", searchcode);
            cmd.Parameters.AddWithValue("@action", ac);
            SqlDataReader dr;
            conn.Open();
            dr = cmd.ExecuteReader();
            List<Product> list = new List<Product>();
            Product pr = new Product();
            while (dr.Read() == true)
            {

                pr = new Product();
                pr.masp = dr["codeproduct"].ToString();
                pr.categoryid = dr["categoryid"].ToString();
                pr.chitiet = dr["productname"].ToString();
                pr.hinhanh = dr["imagesname"].ToString();
                pr.giasp = Convert.ToUInt32(dr["price"]);
                pr.productid = dr["productid"].ToString();
                pr.size = dr["size"].ToString();
                list.Add(pr);
            }

            if (ac == "1")
            {
                return Json(list);
            }
            else
            {
                if (list == null)
                {
                    var result = 1;
                    return Json(result);
                }
                else
                {
                    string html = RenderPartialViewToString(this, "Searchpartial", list);
                    return Json(html);
                }

            }
        }
        [HttpPost]
        public ActionResult AddProduct(Product model)
        {
            SqlConnection conn = new SqlConnection(cn.conn);
            if (Request.Files.Count > 0)
            {
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var file = Request.Files[i];

                    var imgname = Path.GetFileName(file.FileName);

                    var path = Path.Combine(Server.MapPath("~/Content/img"), imgname);
                    file.SaveAs(path);
                    return Json("img");
                }
            }
            else
            {

                SqlCommand cmd = new SqlCommand("sp_managerproduct", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@categoryid", model.categoryid);
                cmd.Parameters.AddWithValue("@productname", model.chitiet);
                cmd.Parameters.AddWithValue("@imgname", model.hinhanh);
                cmd.Parameters.AddWithValue("@price", model.price);
                cmd.Parameters.AddWithValue("@codeproduct", model.masp);
                cmd.Parameters.AddWithValue("@size", model.size);
                var res = cmd.Parameters.Add("@set", SqlDbType.Int);
                res.Direction = ParameterDirection.ReturnValue;
                conn.Open();
                cmd.ExecuteNonQuery();
                var result = res.Value.ToString();
                return Json(result);
            }
            return Json(0);
        }
        [HttpPost]
        public ActionResult DeleteProduct(FormCollection selectcheck)
        {
            SqlConnection conn = new SqlConnection(cn.conn);
            string sa = selectcheck["productid"];
            if (sa == null)
            {
                return Redirect(Request.UrlReferrer.ToString());
            }
            string[] productid = selectcheck["productid"].Split(new char[] { ',' });
            foreach (string id in productid)
            {
                SqlCommand cmd = new SqlCommand("sp_deleteproduct", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@productid", id);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            return Redirect(Request.UrlReferrer.ToString());
        }
        public ActionResult EditProduct(Product model, string ac)
        {

            if (Request.Files.Count > 0)
            {
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var file = Request.Files[i];

                    var imgname = Path.GetFileName(file.FileName);

                    var path = Path.Combine(Server.MapPath("~/Content/img"), imgname);
                    file.SaveAs(path);
                    return Json("img");
                }
            }
            else
            {
                SqlConnection conn = new SqlConnection(cn.conn);
                SqlCommand cmd = new SqlCommand("sp_editproduct", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@productid", model.productid);
                cmd.Parameters.AddWithValue("@categoryid", model.categoryid);
                cmd.Parameters.AddWithValue("@productname", model.chitiet);
                cmd.Parameters.AddWithValue("@imgname", model.hinhanh);
                cmd.Parameters.AddWithValue("@price", model.price);
                cmd.Parameters.AddWithValue("@codeproduct", model.masp);
                cmd.Parameters.AddWithValue("@size", model.size);
                cmd.Parameters.AddWithValue("@action", ac);
                var res = cmd.Parameters.Add("@set", SqlDbType.Int);
                res.Direction = ParameterDirection.ReturnValue;
                conn.Open();
                cmd.ExecuteNonQuery();
                var result = res.Value.ToString();
                return Json(result);
            }
            return Json(0);
        }
        public ActionResult EditSlideShow(string ac,string imgname)
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            if (Request.Files.Count > 0)
            {
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var file = Request.Files[i];

                   var imgnames = Path.GetFileName(file.FileName);

                    var path = Path.Combine(Server.MapPath("~/Content/img"), imgnames);
                    file.SaveAs(path);
                }
            }
            else if(ac!=null)
            {
                SqlConnection conn = new SqlConnection(cn.conn);
                SqlCommand cmd = new SqlCommand("sp_slideshow", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@slide", imgname);
                cmd.Parameters.AddWithValue("@ac", ac);
                conn.Open();
                cmd.ExecuteNonQuery();
                return Json(1);
            }
            if (ac == null)
            {
                return View();
            }
            return View();
            
        }
    }
}