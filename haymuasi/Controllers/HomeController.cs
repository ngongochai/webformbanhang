using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using haymuasi.Models;
using System.Data.SqlClient;
using System.IO;
using System.Data;
namespace haymuasi.Controllers
{
    public class HomeController : Controller
    {
        db cn = new db();
        public string RenderPartialViewToString(Controller Home, string viewName, Object model)
        {
            if (string.IsNullOrEmpty(viewName))
            {
                viewName = Home.ControllerContext.RouteData.GetRequiredString("action");
            }

            Home.ViewData.Model = model;

            using (var sw = new StringWriter())
            {
                // Find the partial view by its name and the current controller context.
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(Home.ControllerContext, viewName);

                // Create a view context.
                var viewContext = new ViewContext(Home.ControllerContext, viewResult.View, Home.ViewData, Home.TempData, sw);

                // Render the view using the StringWriter object.
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult MenuHeader()
        {
            List<MenuHeader> list = new List<MenuHeader>();
            MenuHeader mnh = new MenuHeader();
            mnh.Menu = "Sản Phẩm";
            list.Add(mnh);

            mnh = new MenuHeader();
            mnh.Menu = "Hướng dẫn mua hàng";
            list.Add(mnh);

            mnh = new MenuHeader();
            mnh.Menu = "thanh toán và vận chuyển";
            list.Add(mnh);

            mnh = new MenuHeader();
            mnh.Menu = "chính sách mua sỉ";
            list.Add(mnh);

            mnh = new MenuHeader();
            mnh.Menu = "chính sách đại lý va ctv";
            list.Add(mnh);

            mnh = new MenuHeader();
            mnh.Menu = "tin tức";
            list.Add(mnh);

            mnh = new MenuHeader();
            mnh.Menu = "liên hệ";
            list.Add(mnh);

            return View(list);
        }
        private const string CartSession = "CartSession";
        public ActionResult Countitem()
        {
            if (Session[CartSession] == null)
            {
                List<Product> listCart = new List<Product>();
                return View(listCart);
            }
            else
            {
                List<Product> listCart = (List<Product>)Session[CartSession];
                Session[CartSession] = listCart;
                return View(listCart);
            }
        }
        public ActionResult Selectquantity(string productid)
        {
            List<Product> list = new List<Product>();
            Product pr = new Product();
            pr.productid = productid;
            list.Add(pr);
            IEnumerable<Product> listid = list;
            string html = RenderPartialViewToString(this, "_Selectquantity", pr);
            return Json(html);
        }
        public ActionResult SearchAuto(string keywork)
        {
            SqlConnection conn = new SqlConnection(cn.conn);
            SqlCommand cmd = new SqlCommand("sp_searchproduct", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@keywork", keywork);
            SqlDataReader dr;
            conn.Open();
            dr = cmd.ExecuteReader();
            List<Product> listproduct = new List<Product>();
            Product mn = new Product();
            while (dr.Read() == true)
            {
                mn = new Product();
                mn.masp = dr["codeproduct"].ToString();
                mn.chitiet = dr["productname"].ToString();
                mn.hinhanh = dr["imagesname"].ToString();
                mn.giasp = Convert.ToInt32(dr["price"]);
                mn.luotxem = dr["viewall"].ToString();
                mn.productid = dr["productid"].ToString();
                mn.size = dr["size"].ToString();
                listproduct.Add(mn);
            }
            if (listproduct.Count() == 0)
            {
                return Json(0);
            }
            else
            {
                var html = RenderPartialViewToString(this, "_SearchAuto", listproduct);
                return Json(html);
            }
        }
        public ActionResult selectsize(){
                        SqlConnection conn = new SqlConnection(cn.conn);
            conn.Open();
            SqlDataReader dr ;
            SqlCommand cmd = new SqlCommand("SELECT size FROM product group by size", conn);
            dr = cmd.ExecuteReader();
            Product idp = new Product();
            List<Product> list = new List<Product>();
            idp = new Product();
            idp.size = "chọn kích thước";
            list.Add(idp);
            while (dr.Read() == true)
            {
                
                idp = new Product();
                idp.size = dr["size"].ToString();
                list.Add(idp);
            }
            conn.Close();
            return View(list);
        }
    }
}