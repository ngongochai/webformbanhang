using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using haymuasi.Models;
using PagedList.Mvc;
using PagedList;
using System.IO;
using System.Data;

namespace Admin.Controllers
{
    public class ManagerOrderController : Controller
    {
        // GET: ManagerOrder
        db cn=new db();
        public string RenderPartialViewToString(Controller ManagerOrder, string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
            {
                viewName = ManagerOrder.ControllerContext.RouteData.GetRequiredString("action");
            }

            ManagerOrder.ViewData.Model = model;

            using (var sw = new StringWriter())
            {
                // Find the partial view by its name and the current controller context.
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(ManagerOrder.ControllerContext, viewName);

                // Create a view context.
                var viewContext = new ViewContext(ManagerOrder.ControllerContext, viewResult.View, ManagerOrder.ViewData, ManagerOrder.TempData, sw);

                // Render the view using the StringWriter object.
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }
        public ActionResult ManagerOrder(int? page)
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            SqlConnection conn = new SqlConnection(cn.conn);
            conn.Open();
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("select * from ordercustome order by customerid desc", conn);
            dr = cmd.ExecuteReader();
            Order idp = new Order();
            List<Order> list = new List<Order>();
            while (dr.Read() == true)
            {

                idp = new Order();
                idp.customerid = dr["customerid"].ToString();
                idp.namecustomer = dr["namecustomer"].ToString();
                idp.phone = dr["phone"].ToString();
                idp.address = dr["address"].ToString();
                idp.city = dr["city"].ToString();
                idp.email = dr["email"].ToString();
                idp.note = dr["note"].ToString();
                list.Add(idp);
            }
            int pagesize = 30;
            int pagenumber = (page ?? 1);
            return View(list.ToPagedList(pagenumber, pagesize));
        }
        public ActionResult DetailsOrder(int productid)
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            SqlConnection conn = new SqlConnection(cn.conn);
            conn.Open();
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("select * from detailorder where customerid="+productid+" ", conn);
            dr = cmd.ExecuteReader();
            List<Product> list = new List<Product>();
            Product pr = new Product();
            while (dr.Read() == true)
            {

                pr = new Product();
                pr.masp = dr["codeproduct"].ToString();
                pr.chitiet = dr["productname"].ToString();
                pr.hinhanh = dr["imagesname"].ToString();
                pr.giasp = Convert.ToUInt32(dr["price"]);
                pr.quantity = Convert.ToInt32(dr["quantity"]);
                pr.total = Convert.ToInt32(dr["total"]);
                pr.size = dr["size"].ToString();
                list.Add(pr);
            }
            string html = RenderPartialViewToString(this, "DetailOrder", list);
            return Json(html);
        }
        public ActionResult DeleteProduct(FormCollection selectcheck)
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            SqlConnection conn = new SqlConnection(cn.conn);
            string sa = selectcheck["customertid"];
            if (sa == null)
            {
                return Redirect(Request.UrlReferrer.ToString());
            }
            string[] productid = selectcheck["customertid"].Split(new char[] { ',' });
            foreach (string id in productid)
            {
                SqlCommand cmd = new SqlCommand("sp_deletecustomer", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@customerid", id);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            return Redirect(Request.UrlReferrer.ToString());
        }
        public ActionResult SaveNote(int customerid,string note)
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            SqlConnection conn = new SqlConnection(cn.conn);
            SqlCommand cmd = new SqlCommand("updatenote", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@customerid", customerid);
            cmd.Parameters.AddWithValue("@note", note);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            return Json(note);
        }
    }
}