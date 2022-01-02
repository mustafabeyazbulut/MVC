using EnvanterTakipYönetimSistemi.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EnvanterTakipYönetimSistemi.ViewModels;

namespace EnvanterTakipYönetimSistemi.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        private Models.EnvanterServisEntities db = new Models.EnvanterServisEntities();


        [LoginControl]
        public ActionResult Index(HomeViewModel model)
        {

            model.DuyuruList = (from k in db.Tbl_Duyuru.Where(f => (f.D_Kayit == true)).OrderByDescending(f => f.D_ID)
                                select new DuyuruViewModel
                                {
                                    DuyuruID=k.D_ID,
                                    DuyuruKonu=k.D_Konu,
                                    DuyuruIcerik=k.D_Icerik,
                                    DuyuruYayinlayan=k.Tbl_Personel.Per_Ad +" " + k.Tbl_Personel.Per_Soyad,
                                    DuyuruYayinlayanSube=k.Tbl_Personel.Per_Sube
                                }).ToList();
            return View(model);
        }


    }
}