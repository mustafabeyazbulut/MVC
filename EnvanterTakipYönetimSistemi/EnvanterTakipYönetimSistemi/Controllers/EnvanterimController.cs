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
            int kullaniciID = Convert.ToInt32(Session["Per_ID"]);
            model.EnvanterimList = (from x in db.Tbl_Zimmet.Where(k=>k.Kullanan_ID==kullaniciID).OrderByDescending(f => f.Zim_ID)
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
        public string EnvanterimDestek()
        {
            return "1";
        }
    }
}