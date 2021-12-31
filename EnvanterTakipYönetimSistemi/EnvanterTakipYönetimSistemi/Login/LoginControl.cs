using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EnvanterTakipYönetimSistemi.Login
{
    public class LoginControl : ActionFilterAttribute, IActionFilter
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
           // Şifre girişi yapılmadıysa diğer sayfaların kullanılmasını engelleyen kontrol
            if (HttpContext.Current.Session["Per_ID"] != null)
            {
                base.OnActionExecuting(filterContext);
            }
            else
            {
                HttpContext.Current.Response.Redirect("/Login/Index");

            }

        }
    }
}