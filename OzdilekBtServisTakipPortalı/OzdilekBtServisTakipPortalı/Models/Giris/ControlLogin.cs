using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace OzdilekBtServisTakipPortalı.Models.Giris
{
    public class ControlLogin : ActionFilterAttribute, IActionFilter
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            try
            {
                string userıd1 = HttpContext.Current.Session["UserID"].ToString();
                if (userıd1 == null || userıd1 != "1")
                {
                    //1 değilse buraya girer
                    if (userıd1 == null || userıd1 != "2")
                    {
                        //2 değilse buraya girer
                        
                        if (userıd1 == null || userıd1 != "3")
                        {
                            // değilse buraya girer
                            HttpContext.Current.Response.Redirect("/Giris/Giris");
                        }
                        else
                        {

                            //3 se buraya girer

                            //kaldığı kontrollerden devam eder (Login State)

                            base.OnActionExecuting(filterContext);
                        }
                    }
                    else
                    {
                       
                        //1 değil ama 2yse buraya girer

                        //kaldığı kontrollerden devam eder (Login State)

                        base.OnActionExecuting(filterContext);
                    }

                }
                else
                {
                    //1 Se buraya girer
                    //kaldığı kontrollerden devam eder (Login State)
                    base.OnActionExecuting(filterContext);

                }
            }
            catch (Exception)
            {

                HttpContext.Current.Response.Redirect("/Giris/Giris");
            }

        }

    }
}