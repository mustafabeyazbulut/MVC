using EnvanterTakipYönetimSistemi.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EnvanterTakipYönetimSistemi.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string eposta,string parola)
        {//Giriş Yapma

            if (new LoginState().IsLoginSuccess(eposta, parola)) // loginstate sınıfına gider ve parola kontrolü yapar. Doğruysa return true döner Session["Per_ID"] kontrolü yapılabilir.
            {
                return RedirectToAction("Index", "Home");
                
            }
            
            // şifre girişi yanlışsa buraya gelir ve ekrana uyarı verir
            ViewBag.mesaj = "Eposta veya Parola hatalıdır.";
            return View();
        }

        [HttpPost]
        public ActionResult Indexs(string isim, string eposta)
        {//Şifre Talebi


            ViewBag.mesaj1 = "İsim veya Eposta hatalıdır.";
            return View("Index");
        }

        public ActionResult LogOut()
        {
            Session.Clear();
            return RedirectToAction("Index", "Login");
        }
    }
}