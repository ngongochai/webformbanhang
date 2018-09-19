using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace haymuasi.Controllers
{
    public class InformationController : Controller
    {
        // GET: Information
        public ActionResult Information()
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("LoginUser", "LoginUser");
            }
            return View();
        }
    }
}