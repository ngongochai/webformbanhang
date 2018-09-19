using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using haymuasi.Models;
using System.Data.SqlClient;
using PagedList.Mvc;
using PagedList;
namespace Admin.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin

        public ActionResult Index()
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("Login", "Login");
            }

            return View();
        }
    }
}