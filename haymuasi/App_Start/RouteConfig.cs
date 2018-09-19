using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace haymuasi
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
 routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
           routes.MapRoute(
          name: "Product Detail",
          url: "ga-chong-tham/{metatitle}-{id}",
          defaults: new { controller = "Product1", action = "detail", id = UrlParameter.Optional },
          namespaces: new[] { "haymuasi.Controllers" }
        );
           routes.MapRoute(
                name: "Add Cart",
                url: "gio-hang-ga-chong-tham",
                defaults: new { controller = "Cart", action = "Cart", id = UrlParameter.Optional },
                namespaces: new[] { "haymuasi.Controllers" }
        );
                   routes.MapRoute(
        name: "Login",
        url: "dang-nhap",
        defaults: new { controller = "LoginUser", action = "LoginUser", id = UrlParameter.Optional },
        namespaces: new[] { "haymuasi.Controllers" }
        );
                   routes.MapRoute(
                    name: "tim kiem",
                    url: "tim-kiem-ga-chong-tham",
                    defaults: new { controller = "Search", action = "Search", id = UrlParameter.Optional },
                    namespaces: new[] { "haymuasi.Controllers" }
                    );
                   routes.MapRoute(
        name: "đặt hàng",
        url: "dat-hang-ga-chong-tham",
        defaults: new { controller = "Order", action = "Order", id = UrlParameter.Optional },
        namespaces: new[] { "haymuasi.Controllers" }
        );
                   routes.MapRoute(
           name: "Default",
           url: "{controller}/{action}/{id}",
           defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
           namespaces: new string[] { "haymuasi.Controllers" }
       );
        }
    }
}