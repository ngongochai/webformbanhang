using System;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Collections.Generic;
using haymuasi.Models;


namespace haymuasi.Controllers
{
    public class Product1Controller : HomeController
    {
        // GET: Product1
        db cn=new db();       
        public ActionResult Product()
        {
            SqlConnection conn = new SqlConnection(cn.conn);
            conn.Open();
            SqlDataReader dr = null;
            SqlCommand cmd = new SqlCommand("select * from product ORDER BY categoryid desc", conn);
            dr = cmd.ExecuteReader();
            List<Product> listProduct = new List<Product>();
            List<Product> listcategory=new List<Product>();
            while (dr.Read() == true)
            {
                
                Product mn = new Product();
                Product ct = new Product();
                if (listcategory.Exists(x=>x.categoryid==dr["categoryid"].ToString())==false)
                {
                    ct.categoryid = dr["categoryid"].ToString();
                    listcategory.Add(ct);
                }
                mn.categoryid = dr["categoryid"].ToString();
                mn.masp = dr["codeproduct"].ToString();
                mn.chitiet = dr["productname"].ToString();
                mn.chitiet = dr["productname"].ToString();
                mn.size = dr["size"].ToString();
                mn.hinhanh = dr["imagesname"].ToString();
                mn.giasp = Convert.ToUInt32(dr["price"]);
                mn.luotxem = dr["viewall"].ToString();
                mn.productid = dr["productid"].ToString();
                listProduct.Add(mn);
                
            }
            
            for (int i = 0; i < listcategory.Count; i++)
            {
                for (int j = 0; j < listProduct.Count; j++)
                {
                    if (listcategory[i].categoryid==listProduct[j].categoryid)
                    {
                        listcategory[i].product.Add(listProduct[j]);
                    }
                }
            }
                ViewBag.data = listcategory;
            conn.Close();

            return View(listcategory);
        }   
        public ActionResult detail(int id)
        {
            SqlConnection conn = new SqlConnection(cn.conn);
            conn.Open();
            SqlDataReader dr ;
            SqlCommand cmd = new SqlCommand("SELECT *FROM product t2 where productid=" + id + " update product set viewall=viewall+1 where productid="+id+"", conn);
            dr = cmd.ExecuteReader();
            Product idp = new Product();
            while (dr.Read() == true)
            {

                idp = new Product();
                idp.categoryid = dr["categoryid"].ToString();
                idp.masp = dr["codeproduct"].ToString();
                idp.chitiet = dr["productname"].ToString();
                idp.hinhanh = dr["imagesname"].ToString();
                idp.giasp = Convert.ToUInt32(dr["price"]);
                idp.luotxem = dr["viewall"].ToString();
                idp.productid = dr["productid"].ToString();
                idp.size = dr["size"].ToString();
              
            }
            conn.Close();
            return View(idp);
        }
        public ActionResult ProductTopic()
        {
            SqlConnection conn = new SqlConnection(cn.conn);
            conn.Open();
            SqlDataReader dr = null;
            SqlCommand cmd = new SqlCommand("select * from producttopic", conn);
            dr = cmd.ExecuteReader();
            List<ProductTopic> listProduct = new List<ProductTopic>();
            while (dr.Read() == true)
            {
                ProductTopic mn = new ProductTopic();
                mn.topic = dr["topic"].ToString();
                listProduct.Add(mn);
            }
            conn.Close();
            return View(listProduct);
        }
        }
    }