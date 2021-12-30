using OzdilekBtServisTakipPortalı.Models;
using OzdilekBtServisTakipPortalı.Models.Giris;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OzdilekBtServisTakipPortalı.Controllers
{
    public class GirisController : Controller
    {
        //
        // GET: /Giris/

        public ActionResult Giris()
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult Giris(string username, string password)
        {
            //LoginState.cs ye gider 
            //Login State true gelirse Servis Takibi açar
            if (new LoginState().IsLoginSuccess(username, password))
            {
                //şifre girişi doğruysa burası
                //return RedirectToAction("ServisTakip", "Home");

                if (Session["UserID"].ToString() == null || Session["UserID"].ToString() == "1")
                {
                    return RedirectToAction("ServisTakip", "Home");
                }
                else if(Session["UserID"].ToString() == null || Session["UserID"].ToString() == "2")
                {
                        return RedirectToAction("ServisTakipKullanici", "Kullanici");
                 
                }
                else if (Session["UserID"].ToString() == null || Session["UserID"].ToString() == "3")
                {
                    return RedirectToAction("YetkiveTanimlamalar", "Admin");
                }
              }
            
            //şifre girişi yanlışsa burdan devam eder
            ViewBag.mesaj = "Kullanıcı adı veya şifre hatalı";
            return View();
            //
        }

        public ActionResult LogOut()
        {
            Session.Clear();
            return RedirectToAction("Giris", "Giris");
        }

    }
}
