using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using haymuasi.Models;
namespace haymuasi.Controllers
{
    public class SlideShowController : HomeController
    {
        // GET: SlideShow
        db cn = new db();
        public ActionResult Slideshow()
        {
             SqlConnection conn = new SqlConnection(cn.conn);
            conn.Open();
            SqlDataReader dr = null;
            SqlCommand cmd = new SqlCommand("select * from slideshow", conn);
            dr = cmd.ExecuteReader();
            SlideShow sl = new SlideShow();
            List<SlideShow> listslide = new List<SlideShow>();
            while (dr.Read() == true)
            {
                sl.slide1 = dr["slide1"].ToString();
                sl.slide2 = dr["slide2"].ToString();
                sl.slide3 = dr["slide3"].ToString();
                sl.slide4 = dr["slide4"].ToString();
                sl.slide5 = dr["slide5"].ToString();
            }
            return View(sl);
        }
    }
}