using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using haymuasi.Models;
namespace haymuasi.Controllers
{
    public class CartController : Controller
    {
        db cn = new db();       
        // GET: Cart
        public const string CartSession = "CartSession";
        private int isExisting(string productid,string size)
        {
            List<Product> listCart = (List<Product>)Session[CartSession];
            for (int i = 0; i < listCart.Count(); i++)
            {
                if (listCart[i].giasp == 0 && listCart[i].size==size)
                {
                    return i;
                }
                else if(listCart[i].productid==productid)
                {
                    return i;

                }
            }
            return -1;

        }
        public ActionResult Cart()
        {
            var cart = Session[CartSession];
            List<Product> list = new List<Product>();
            if (cart != null)
            {   
                list = (List<Product>)cart;
            }
            return View(list);
        }
        public ActionResult AddItem(string productid,int quantity,string size)
        {
            SqlConnection conn = new SqlConnection(cn.conn);
            conn.Open();
            SqlDataReader dr = null;
            SqlCommand cmd = new SqlCommand("sp_cart", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@productid", productid); 
            dr = cmd.ExecuteReader();
            List<Product> listProduct = new List<Product>();
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
                mn.quantity = quantity;
                if (size == null)
                {
                    mn.size = dr["size"].ToString();
                }
                else
                {
                    mn.size = size;
                }
                listProduct.Add(mn);
            }
            conn.Close();
            var cart = Session[CartSession];
            List<Product> listCart = new List<Product>();
            if (cart == null)
            {

                listCart=listProduct;
                Session[CartSession] = listCart;
                mn.total = quantity * mn.giasp;
             }
            else
            {
                listCart = (List<Product>)Session[CartSession];
                
                    if (listCart.Exists(x=>x.productid==productid) && listCart.Exists(x=>x.size==listProduct[0].size))
                    {
                        foreach (var item in listCart)
                        {
                            if (item.productid == productid && item.size == listProduct[0].size)
                            {
                                item.quantity +=quantity;
                                item.total = item.quantity * item.giasp;
                            }
                           
                        }
                    }
                    else
                    {
                            listCart = (List<Product>)Session[CartSession];
                            listCart.AddRange(listProduct);
                            Session[CartSession] = listCart;
                            mn.total = quantity * mn.giasp;
                        
                    }
                

            }
            return RedirectToAction("Cart");
        }
        public ActionResult updatequantity(string productid, int quantity,string size){
             List<Product> listCart = new List<Product>();
             listCart = (List < Product >) Session[CartSession];
             long total=0;
             long pay=0;
                 foreach (Product pr in listCart)
                 {
                     if (pr.productid == productid && pr.size==size)
                     {
                         pr.quantity = quantity;
                         total = pr.total = quantity * pr.giasp;
                     }
                 }
                 foreach (var item in listCart)
                 {
                     pay+=item.total;
                 }
                 var result = new { tongtien1 = total, tongtien2 = pay };
                 return Json(result);
             
        }
        public ActionResult UpdateSize(string productid, string newsize,string oldsize)
        {
            List<Product> listCart = new List<Product>();
            listCart = (List<Product>)Session[CartSession];
            foreach (Product pr in listCart)
            {
                if (pr.size == oldsize&&pr.productid==productid)
                {
                    pr.size=newsize;
                    break;
                }
            }
            var result = new { size=newsize };
            return Json(result);

        }
        public ActionResult RemoveItem(string productid, int quantity,string size)
        {
            int delete = isExisting(productid,size);
            List<Product> listCart = (List<Product>)Session[CartSession];
            listCart.RemoveAt(delete);
            Session[CartSession] = listCart;
            listCart = (List<Product>)Session[CartSession];
            long total = 0;
            long pay = 0;
            foreach (Product pr in listCart)
            {
                if (pr.productid == productid)
                {
                    pr.quantity = quantity;
                    total = pr.total = quantity * pr.giasp;
                }
            }
            foreach (var item in listCart)
            {
                pay += item.total;
            }
            var count = listCart.Count;
            var result = new { tongtien1 = total, tongtien2 = pay,count=count };
            return Json(result);
        }
    }
}