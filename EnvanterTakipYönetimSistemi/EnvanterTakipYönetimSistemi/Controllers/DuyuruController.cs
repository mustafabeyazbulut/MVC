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
            model.DuyuruList = (from k in db.Tbl_Duyuru.OrderByDescending(f => f.D_ID)
                                select new DuyuruViewModel
                                {
                                    DuyuruID = k.D_ID,
                                    DuyuruKonu = k.D_Konu,
                                    DuyuruIcerik = k.D_Icerik,
                                    DuyuruYayinlayan = k.Tbl_Personel.Per_Ad + " " + k.Tbl_Personel.Per_Soyad,
                                    DuyuruYayinlayanSube = k.Tbl_Personel.Tbl_P_Sube.Sube_Adi,
                                    DuyuruKayit= (bool)k.D_Kayit,
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


        [HttpPost]
        public JsonResult DuyuruSil(List<string> values)
        {
            if (values == null) return Json(new { Result = "-1" });

            foreach (string ids in values)
            {

                int id = Convert.ToInt32(ids);

                var silDuyuru = (from x in db.Tbl_Duyuru
                              where x.D_ID == id
                              select x).FirstOrDefault();

                if (silDuyuru.D_Kayit == true ) // daha önce silindiyse herhangi bir işlem yapmasına gerek yok. Zimmetliyse silinemez
                {
                    silDuyuru.Per_ID = Convert.ToInt32(Session["Per_ID"]);
                    silDuyuru.D_Kayit = false;
                    db.SaveChanges();
                }

            }
            return Json(new { Result = "1" });
        }


    }
}