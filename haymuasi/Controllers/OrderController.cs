using haymuasi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace haymuasi.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        db cn = new db();
        public string RenderPartialViewToString(Controller Order, string viewName, Object model)
        {
            if (string.IsNullOrEmpty(viewName))
            {
                viewName = Order.ControllerContext.RouteData.GetRequiredString("action");
            }

            Order.ViewData.Model = model;

            using (var sw = new StringWriter())
            {
                // Find the partial view by its name and the current controller context.
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(Order.ControllerContext, viewName);

                // Create a view context.
                var viewContext = new ViewContext(Order.ControllerContext, viewResult.View, Order.ViewData, Order.TempData, sw);

                // Render the view using the StringWriter object.
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }
        public const string CartSession = "CartSession";
        public ActionResult Order(Order model)
        {
            if (model.email == null)
            {
                model.email = "";
            }
            var cart = Session[CartSession];
            List<Product> listcart = new List<Product>();
            listcart = (List<Product>)cart;
            if (cart == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (listcart.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            if (model.namecustomer == null)
            {
                return View(listcart);
            }
            int ac=0;            
            for (int i = 0; i < listcart.Count; i++)
            {
                if (ac != 0)
                {
                    ac = 1;
                }
                SqlConnection conn = new SqlConnection(cn.conn);
                conn.Open();
                SqlDataReader dr = null;
                SqlCommand cmd = new SqlCommand("sp_order", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@namecustomer", model.namecustomer);
                cmd.Parameters.AddWithValue("@phone",model.phone);
                cmd.Parameters.AddWithValue("@address",model.address );
                cmd.Parameters.AddWithValue("@city",model.city );
                cmd.Parameters.AddWithValue("@email",model.email );
                cmd.Parameters.AddWithValue("@codeproduct", listcart[i].masp);
                cmd.Parameters.AddWithValue("@size",listcart[i].size );
                cmd.Parameters.AddWithValue("@productname",listcart[i].chitiet );
                cmd.Parameters.AddWithValue("@imagesname", listcart[i].hinhanh);
                cmd.Parameters.AddWithValue("@price", listcart[i].giasp);
                cmd.Parameters.AddWithValue("@quantity", listcart[i].quantity);
                cmd.Parameters.AddWithValue("@total", listcart[i].total);
                cmd.Parameters.AddWithValue("@ac", ac);
                dr = cmd.ExecuteReader();
                conn.Close();
                ac++;
            }
            Session[CartSession] = null;
            var html = RenderPartialViewToString(this, "_Order", listcart); ;
            return Json(html);
        }
    }
}