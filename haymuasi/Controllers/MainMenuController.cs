using haymuasi.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace haymuasi.Controllers
{
    public class MainMenuController : HomeController
    {
        // GET: MainMenu
        db cn = new db();
        public ActionResult MainMenu()
        {
            SqlConnection conn = new SqlConnection(cn.conn);
            conn.Open();
            SqlDataReader dr = null;
            SqlCommand cmd = new SqlCommand("select * from menu", conn);
            dr = cmd.ExecuteReader();
            List<MainMenu> listmenu = new List<MainMenu>();
            while (dr.Read() == true)
            {
                MainMenu mmn = new MainMenu();
                mmn.menucontent = dr["menucontent"].ToString();
                mmn.hinh = dr["nameimg"].ToString();
                mmn.ID = dr["id"].ToString();
                mmn.Parent_id = dr["parent_id"].ToString();
                mmn.submenu = dr["submenu"].ToString();
                listmenu.Add(mmn);
            }
            return View(listmenu);
        }
    }
}