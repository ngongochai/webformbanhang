using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using haymuasi.Models;
using PagedList.Mvc;
using PagedList;
using System.Data;
using System.IO;
namespace haymuasi.Controllers
{
    public class SearchController : Controller
    {
        // GET: Seach
        db cn = new db();
        public ActionResult Search(string curentfiter,string values, int ? page)
        {
            if (values != null)
            {
                page = 1;
            }
            else
            {
                values = curentfiter;
            }
            if (values == null)
            {           
                values="";
            }
            ViewBag.curentfiter = values;
            int pagesize = 9;
            int pagenumber = (page ?? 1);
            SqlConnection conn = new SqlConnection(cn.conn);
            conn.Open();
            SqlDataReader dr = null;
            SqlCommand cmd = new SqlCommand("sp_searchproduct", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@keywork", values);
            dr = cmd.ExecuteReader();
            List<Search> listProduct = new List<Search>();
            Search mn = new Search();
            while (dr.Read() == true)
            {
                mn = new Search();
                mn.categoryid = dr["categoryid"].ToString();
                mn.masp = dr["codeproduct"].ToString();
                mn.chitiet = dr["productname"].ToString();
                mn.hinhanh = dr["imagesname"].ToString();
                mn.giasp = Convert.ToUInt32(dr["price"]);
                mn.luotxem = dr["viewall"].ToString();
                mn.size = dr["size"].ToString();
                mn.productid = dr["productid"].ToString();
                listProduct.Add(mn);
            }
            
            ViewData["count"] = listProduct.Count();
            if(values=="")
                {
                    return View(listProduct.ToPagedList(pagenumber,pagesize));
                }
            
            ViewData["value"] = values;
            return View(listProduct.ToPagedList(pagenumber,pagesize));
        }
       
    }
}