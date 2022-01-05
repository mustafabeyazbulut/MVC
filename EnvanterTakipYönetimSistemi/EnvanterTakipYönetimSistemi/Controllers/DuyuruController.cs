using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EnvanterTakipYönetimSistemi.Login;
using EnvanterTakipYönetimSistemi.Models;
using EnvanterTakipYönetimSistemi.ViewModels;

namespace EnvanterTakipYönetimSistemi.Controllers
{
    public class DuyuruController : Controller
    {
        // GET: Duyuru
        private Models.EnvanterServisEntities db = new Models.EnvanterServisEntities();
        [LoginControl]
        public ActionResult Index(DuyuruViewModel model)
        {
            model.DuyuruList = (from k in db.Tbl_Duyuru.Where(f => (f.D_Kayit == true)).OrderByDescending(f => f.D_ID)
                                select new DuyuruViewModel
                                {
                                    DuyuruID = k.D_ID,
                                    DuyuruKonu = k.D_Konu,
                                    DuyuruIcerik = k.D_Icerik,
                                    DuyuruYayinlayan = k.Tbl_Personel.Per_Ad + " " + k.Tbl_Personel.Per_Soyad,
                                    DuyuruYayinlayanSube = k.Tbl_Personel.Tbl_P_Sube.Sube_Adi,
                                }).ToList();

            if (Request.IsAjaxRequest()) // index sayfası ilk defa açılmadıysa sadece tabloyu günceller
            {
                return PartialView("_DuyuruTablo", model);
            }
            else
            {
                return View(model);
            }
        }

        [HttpPost]
        public string DuyuruKayit(DuyuruViewModel model)
        {
            if (model.DuyuruID > 0) // envanter 0 ise yeni kayıttır. 0 dan büyükse düzenleme işlemi vardır
            { //düzenleme
                var duyuruGuncelle = db.Tbl_Duyuru.FirstOrDefault(f => f.D_ID == model.DuyuruID);

                duyuruGuncelle.D_Konu = model.DuyuruKonu;
                duyuruGuncelle.D_Icerik = model.DuyuruIcerik;
                duyuruGuncelle.Per_ID =Convert.ToInt32( Session["Per_ID"]);
               
                db.SaveChanges();

                return "0"; // kayıt güncellendiyse 0 döner
            }
            else
            { // Yeni kayıt
                Tbl_Duyuru yeniDuyuru = new Tbl_Duyuru();

                yeniDuyuru.D_Konu = model.DuyuruKonu;
                yeniDuyuru.D_Icerik = model.DuyuruIcerik;
                yeniDuyuru.Per_ID = Convert.ToInt32(Session["Per_ID"]);
                yeniDuyuru.D_Kayit = true;
                db.Tbl_Duyuru.Add(yeniDuyuru);
                db.SaveChanges();
                return "1"; // yeni kayıt oluştuysa 1 döner
            }

          
        }
    }
}