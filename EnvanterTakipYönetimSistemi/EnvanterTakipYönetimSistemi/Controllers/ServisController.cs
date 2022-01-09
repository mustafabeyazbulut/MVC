using EnvanterTakipYönetimSistemi.Models;
using EnvanterTakipYönetimSistemi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EnvanterTakipYönetimSistemi.Controllers
{
    public class ServisController : Controller
    {
        private EnvanterServisEntities db = new Models.EnvanterServisEntities();
        // GET: Servis
        public ActionResult Index(ServisViewModel model)
        {
            int kullaniciID = Convert.ToInt32(Session["Per_ID"]);
            model.ServisList = (from x in db.Tbl_Servis.Where(k => k.Serv_Kayit == true ).OrderByDescending(f => f.Serv_ID)
                                     select new ServisViewModel
                                     {
                                         servisID = x.Serv_ID,
                                         servisFirmaID = (int)x.Serv_FirmaID,
                                         ServisFirmaAdi = x.Tbl_P_Firma.FirmaAdi,
                                         ServisGonTarih = (DateTime)x.Serv_GondTarih,
                                         ServisGelmeTarih = (DateTime)x.Serv_GelmeTarih,
                                         TalepID = (int)x.Arz_ID,
                                         TalepAciklama = x.Tbl_Ariza.Arz_Bilgi,
                                         T_EnvanterCinsAdi = x.Tbl_Ariza.Tbl_Envanter.Tbl_P_EnvanterCinsi.EnvCins_Adi,
                                         T_EnvanterMarkaAdi = x.Tbl_Ariza.Tbl_Envanter.Tbl_P_EnvanterMarka.EnvMarka_Adi,
                                         T_EnvanterModel = x.Tbl_Ariza.Tbl_Envanter.Env_Model,
                                         T_EnvanterSeriNo = x.Tbl_Ariza.Tbl_Envanter.Env_SeriNo,
                                         perID = (int)x.Per_ID,
                                         perAd = x.Tbl_Personel.Per_Ad + " " + x.Tbl_Personel.Per_Soyad,
                                         servisKayit = (bool)x.Serv_Kayit
                                     }).ToList();


            if (Request.IsAjaxRequest()) // index sayfası ilk defa açılmadıysa sadece tabloyu günceller
            {
                return PartialView("_ServislerimTablo", model);
            }
            else
            {
                return View(model);
            }
        }
    }
}