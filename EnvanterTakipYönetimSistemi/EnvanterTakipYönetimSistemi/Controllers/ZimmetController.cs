using EnvanterTakipYönetimSistemi.Login;
using EnvanterTakipYönetimSistemi.Models;
using EnvanterTakipYönetimSistemi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EnvanterTakipYönetimSistemi.Controllers
{
    public class ZimmetController : Controller
    {
        private Models.EnvanterServisEntities db = new Models.EnvanterServisEntities();
        // GET: Zimmet

        [LoginControl]
        public ActionResult Index(ZimmetViewModel model)
        {
            model.ZimmetList = (from x in db.Tbl_Zimmet.OrderByDescending(f => f.Zim_ID)
                                select new ZimmetViewModel
                                {
                                    Zimmet_ID = x.Zim_ID,
                                    Kullanan_ID = (int)x.Kullanan_ID,
                                    Kullanan_AdSoyad = x.Tbl_Personel.Per_Ad + " " + x.Tbl_Personel.Per_Soyad,
                                    Z_EnvanterID = (int)x.Env_ID,
                                    Z_EnvanterCinsAdi = x.Tbl_Envanter.Tbl_P_EnvanterCinsi.EnvCins_Adi,
                                    Z_EnvanterMarkaAdi = x.Tbl_Envanter.Tbl_P_EnvanterMarka.EnvMarka_Adi,
                                    Z_EnvanterModel = x.Tbl_Envanter.Env_Model,
                                    Z_EnvanterSeriNo = x.Tbl_Envanter.Env_SeriNo,

                                    Zimmetleyen_ID = (int)x.Zimmetleyen_ID,
                                    Zimmetleyen_AdSoyad = x.Tbl_Personel1.Per_Ad + " " + x.Tbl_Personel1.Per_Soyad,

                                    ZimmetTarih = (DateTime)x.Zim_Tarih,
                                    ZimmetIadeTarih = (DateTime)x.Zim_IadeTarih,
                                    ZimmetAciklama = x.Zim_Aciklama,
                                    ZimmetKayit = (bool)x.Zim_Kayit
                                }).ToList();


            List<Tbl_Personel> kullanacaklist = db.Tbl_Personel.Where(f => f.Per_Kayit == true).OrderBy(f => f.Per_ID).ToList();
            model.KullanacakList = (from x in kullanacaklist
                                    select new SelectListItem
                                    {
                                        Text = x.Per_Ad + " " + x.Per_Soyad,
                                        Value = x.Per_ID.ToString()
                                    }).ToList();
            model.KullanacakList.Insert(0, new SelectListItem { Value = "", Text = "Seçiniz", Selected = true });


            List<Tbl_Envanter> urunlist = db.Tbl_Envanter.Where(f => f.Env_Kayit == true).Where(f => f.Env_Durum == "Zimmetlenebilir").OrderBy(f => f.Env_ID).ToList();
            model.UrunList = (from x in urunlist
                              select new SelectListItem
                              {
                                  Text = x.Tbl_P_EnvanterCinsi.EnvCins_Adi + " - " + x.Tbl_P_EnvanterMarka.EnvMarka_Adi + " - " + x.Env_Model + " - " + x.Env_SeriNo,
                                  Value = x.Env_ID.ToString()
                              }).ToList();
            model.UrunList.Insert(0, new SelectListItem { Value = "", Text = "Seçiniz", Selected = true });


            if (Request.IsAjaxRequest()) // index sayfası ilk defa açılmadıysa sadece tabloyu günceller
            {
                return PartialView("_ZimmetTablo", model);
            }
            else
            {
                return View(model);
            }
        }
        [HttpPost]
        public string ZimmetKayit(ZimmetViewModel model)
        {
            if (model == null) return "-1";

            Tbl_Zimmet yeniZimmet = new Tbl_Zimmet();
            yeniZimmet.Kullanan_ID = model.Kullanacak_ID;
            yeniZimmet.Env_ID = model.Urun_ID;
            yeniZimmet.Zimmetleyen_ID =Convert.ToInt32( Session["Per_ID"]);
            yeniZimmet.Zim_Tarih = DateTime.Now;
            yeniZimmet.Zim_IadeTarih =DateTime.Parse( "0001-01-01");
            yeniZimmet.Zim_Aciklama = "";
            yeniZimmet.Zim_Kayit = true;
            db.Tbl_Zimmet.Add(yeniZimmet);

            var envanter = (from x in db.Tbl_Envanter.Where(f => (f.Env_ID == model.Urun_ID))
                                       select x).FirstOrDefault();
            envanter.Env_Durum = "Zimmetli";

            db.SaveChanges();

            return "1";
        }

        [HttpPost]
        public string ZimmetIade(ZimmetViewModel model)
        {
            var iadeZimmet = (from x in db.Tbl_Zimmet
                              where x.Zim_ID == model.Zimmet_ID
                              select x).FirstOrDefault();



            if (iadeZimmet == null) return "-1";
            iadeZimmet.Zimmetleyen_ID = Convert.ToInt32(Session["Per_ID"]);
            iadeZimmet.Zim_IadeTarih = DateTime.Now;
            iadeZimmet.Zim_Aciklama = model.ZimmetAciklama;
            iadeZimmet.Zim_Kayit = false;

            var envanter = (from x in db.Tbl_Envanter.Where(f => (f.Env_ID == iadeZimmet.Env_ID))
                            select x).FirstOrDefault();
            envanter.Env_Durum = "Zimmetlenebilir";

            db.SaveChanges();
            return "1";
        }
        [HttpPost]
        public JsonResult ZimmetSil(List<string> values)
        {
            if (values == null) return Json(new { Result = "-1" });

            foreach (string ids in values)
            {
                int id = Convert.ToInt32(ids);
                var silZimmet = (from x in db.Tbl_Zimmet
                                 where x.Zim_ID == id 
                                 select x).FirstOrDefault();

                if (silZimmet.Zim_Kayit == true) // daha önce silindiyse herhangi bir işlem yapmasına gerek yok.
                {
                    silZimmet.Zimmetleyen_ID = Convert.ToInt32(Session["Per_ID"]);
                    silZimmet.Zim_Kayit = false;

                    var envanter = (from x in db.Tbl_Envanter.Where(f => (f.Env_ID == silZimmet.Env_ID))
                                    select x).FirstOrDefault();
                    if (envanter != null)
                        envanter.Env_Durum = "Zimmetlenebilir";

                    db.SaveChanges();

                }

            }
            return Json(new { Result = "1"});
        }

    }
}