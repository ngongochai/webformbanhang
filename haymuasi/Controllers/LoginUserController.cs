using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using haymuasi.Models;
using System.Data.SqlClient;
using System.Data;

namespace haymuasi.Controllers
{
    public class LoginUserController : Controller
    {
        // GET: LoginUser
        db dtb=new db();
        public ActionResult LoginUser()
        {
            if (Session["User"] != null)
            {
                return RedirectToAction("Information", "Information");
            }
            return View();
        }
        [HttpPost]
        public ActionResult LoginUser(string username, string password)
        {
            SqlDataReader reader;
                SqlConnection conn = new SqlConnection(dtb.conn);
            SqlCommand cmd = new SqlCommand("sp_checkloginUser", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_checkloginUser";
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password", password);
            //data//
            conn.Open();
           reader= cmd.ExecuteReader();
            List<User> listuser = new List<User>();
            User us = new User();
            if (reader.Read() == true)
            {
                us = new User();
                us.UserName = reader["UserName"].ToString();
                us.Password = reader["Password"].ToString();
                us.Email=reader["Email"].ToString();
                us.Sex=reader["Sex"].ToString();
                us.Phone=reader["Sex"].ToString();
                Session["User"] = new User() {UserName=us.UserName,Email=us.Email,Sex=us.Sex,Phone=us.Phone };
                return RedirectToAction("Information", "Information");
            }
            else
            {
                ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không chính xác");
            }
            return View();
        }
        public ActionResult LogoutUser()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}