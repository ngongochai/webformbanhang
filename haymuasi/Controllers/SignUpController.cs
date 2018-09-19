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
    public class SignUpController : Controller
    {
        // GET: SignUp
        db cdtb=new db();
        public ActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignUp(User model)
        {
            SqlConnection conn=new SqlConnection(cdtb.conn);
            SqlCommand cmd = new SqlCommand("sp_signup", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@username", model.UserName);
            cmd.Parameters.AddWithValue("@email", model.Email);
            cmd.Parameters.AddWithValue("@password", model.UserName);
            cmd.Parameters.AddWithValue("@name", model.UserName);
            var res1 = cmd.Parameters.Add("@set", SqlDbType.Int);
            res1.Direction = ParameterDirection.ReturnValue;
            conn.Open();
            cmd.ExecuteNonQuery();
            var id1 = res1.Value.ToString();
            if(id1=="1")
            {
                ModelState.AddModelError("", "Tên đăng nhập đã tồn tại");
            }
            else if (id1 == "2")
            {
                ModelState.AddModelError("", "Email đã được sủ dụng");
            }
            else
            {
                ModelState.AddModelError("", "Đăng Ký thành công");
            }
            return View();
        }
    }
}