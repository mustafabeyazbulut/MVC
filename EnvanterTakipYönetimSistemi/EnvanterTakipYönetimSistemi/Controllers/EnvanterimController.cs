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
    
    public class EnvanterimController : Controller
    {

        private Models.EnvanterServisEntities db = new Models.EnvanterServisEntities();
        // GET: Envanterim
        [LoginControl]
        public ActionResult Index(EnvanterimViewModel model)
        {
            DateTime iadetarih = DateTime.Parse("0001-01-01");
            int kullaniciID = Convert.ToInt32(Session["Per_ID"]);
            model.EnvanterimList = (from x in db.Tbl_Zimmet.Where(k=>k.Kullanan_ID==kullaniciID && k.Tbl_Envanter.Env_Durum != "Silindi")
                                    .Where(k => k.Zim_Kayit == false && k.Zim_IadeTarih != iadetarih || k.Zim_Kayit == true).OrderByDescending(f => f.Zim_ID)
                                    select new EnvanterimViewModel
                                {
                                    Zimmet_ID = x.Zim_ID,
                                    Z_EnvanterID = (int)x.Env_ID,
                                    Z_EnvanterCinsAdi = x.Tbl_Envanter.Tbl_P_EnvanterCinsi.EnvCins_Adi,
                                    Z_EnvanterMarkaAdi = x.Tbl_Envanter.Tbl_P_EnvanterMarka.EnvMarka_Adi,
                                    Z_EnvanterModel = x.Tbl_Envanter.Env_Model,
                                    Z_EnvanterSeriNo = x.Tbl_Envanter.Env_SeriNo,
                                    ZimmetTarih = (DateTime)x.Zim_Tarih,
                                    ZimmetIadeTarih = (DateTime)x.Zim_IadeTarih,
                                    ZimmetAciklama = x.Zim_Aciklama,
                                    ZimmetKayit = (bool)x.Zim_Kayit
                                }).ToList();


            if (Request.IsAjaxRequest()) // index sayfası ilk defa açılmadıysa sadece tabloyu günceller
            {
                return PartialView("_EnvanterTablo", model);
            }
            else
            {
                return View(model);
            }
        }

        [HttpPost]
        public string EnvanterimDestek(EnvanterimViewModel model)
        {
            try
            {
                int kullaniciID = Convert.ToInt32(Session["Per_ID"]);
                var env = (from x in db.Tbl_Envanter
                           where x.Env_Kayit == true && x.Env_SeriNo == model.Z_EnvanterSeriNo
                           select x).FirstOrDefault();
                
                var Ariza = (from x in db.Tbl_Ariza
                             where x.Arz_Kayit == true && x.Tbl_Zimmet.Env_ID == env.Env_ID
                             select x).FirstOrDefault();

                if (Ariza != null) return "-2"; // daha önce kayıt oluşturulduysa işleme izin vermeyecek

                Tbl_Ariza arizaYeni = new Tbl_Ariza();
                arizaYeni.Arz_Bilgi = model.ArizaBilgi;
                arizaYeni.Per_ID = env.Env_ID; // düzeltme gelsin
                arizaYeni.Arz_Durum = "Talep";
                arizaYeni.Per_ID = kullaniciID;
                arizaYeni.Arz_Tarih = DateTime.Now;
                arizaYeni.Arz_Kayit = true;
                db.Tbl_Ariza.Add(arizaYeni);
                db.SaveChanges();

                return "1";
            }
            catch 
            {
                return "-1";
            }
        }
    }
}