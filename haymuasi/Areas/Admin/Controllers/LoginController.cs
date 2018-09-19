using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Admin.Models;
using System.Data;
using System.Runtime.Remoting.Contexts;
using haymuasi.Models;
namespace Admin.Controllers
{
    public class LoginController : Controller
    {
        db cn = new db();

        // GET: Login
        public ActionResult Login()
        {
            if (Session["Admin"] != null)
            {
                return RedirectToAction("Index", "Admin");
            }
            return View();
        }
        [HttpPost]
        public ActionResult Login(string UserName, string Password)
        {
            if (Session["Admin"] != null)
            {
                return RedirectToAction("Index", "Admin");
            }
            SqlConnection conn = new SqlConnection(cn.conn);
            SqlCommand cmd = new SqlCommand("sp_checkloginAdmin", conn);
                   cmd.CommandType = CommandType.StoredProcedure;
                   cmd.Parameters.AddWithValue("@username", UserName);
                   cmd.Parameters.AddWithValue("@password", Password);
                    var res = cmd.Parameters.Add("@set", SqlDbType.Int);
                    res.Direction = ParameterDirection.ReturnValue;
                   conn.Open();
                   cmd.ExecuteNonQuery();
                   var id = res.Value;
                  var idd= id.ToString();
                conn.Close();
                if (idd=="1")
                {
                    
                    Session["Admin"] = new Login() { Name=UserName};
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    ModelState.AddModelError("", "UserName or Password Incorrect");
                }
                return View();
            }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Admin");
        }
        public ActionResult ChangePassword(string Password,string Confirm)
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            if (Password == null || Password=="")
            {
                return View();
            }
            if (Password != Confirm)
            {
                ModelState.AddModelError("", "Xác nhận mật khẩu không đúng");
            }
            else
            {
                SqlConnection conn = new SqlConnection(cn.conn);
                SqlCommand cmd = new SqlCommand("sp_changepassword", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@password", Password);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                return RedirectToAction("Index", "Admin");
            }
            return View();
        }
    }
}