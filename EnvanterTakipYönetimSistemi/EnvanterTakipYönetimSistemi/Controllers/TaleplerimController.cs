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
    public class TaleplerimController : Controller
    {
        // GET: Taleplerim
        private Models.EnvanterServisEntities db = new Models.EnvanterServisEntities();

        [LoginControl]
        public ActionResult Index(TaleplerimViewModel model)
        {
            int kullaniciID = Convert.ToInt32(Session["Per_ID"]);
            model.TaleplerimList = (from x in db.Tbl_Ariza
                                    where x.Arz_Kayit==true && x.Tbl_Personel.Per_ID==kullaniciID
                                    select new TaleplerimViewModel
                                    {
                                        TalepID=x.Arz_ID,
                                        TalepBilgi=x.Arz_Bilgi,
                                        TalepDurum=x.Arz_Durum,
                                        TalepTarih= (DateTime)x.Arz_Tarih,
                                        Per_ID= (int)x.Per_ID,
                                        PerAd=x.Tbl_Personel.Per_Ad+" "+x.Tbl_Personel.Per_Soyad,
                                        Env_ID= (int)x.Env_ID,
                                        T_EnvanterCinsAdi = x.Tbl_Envanter.Tbl_P_EnvanterCinsi.EnvCins_Adi,
                                        T_EnvanterMarkaAdi = x.Tbl_Envanter.Tbl_P_EnvanterMarka.EnvMarka_Adi,
                                        T_EnvanterModel = x.Tbl_Envanter.Env_Model,
                                        T_EnvanterSeriNo = x.Tbl_Envanter.Env_SeriNo,
                                        TalepKayit = (bool)x.Arz_Kayit
                                    }).ToList();

            List<Tbl_Zimmet> urunlist = db.Tbl_Zimmet.Where(f => f.Zim_Kayit == true).Where(f => f.Kullanan_ID==kullaniciID).OrderBy(f => f.Env_ID).ToList();
            model.UrunList = (from x in urunlist
                              select new SelectListItem
                              {
                                  Text = x.Tbl_Envanter.Tbl_P_EnvanterCinsi.EnvCins_Adi + " - " + x.Tbl_Envanter.Tbl_P_EnvanterMarka.EnvMarka_Adi + " - " + x.Tbl_Envanter.Env_Model + " - " + x.Tbl_Envanter.Env_SeriNo,
                                  Value = x.Env_ID.ToString()
                              }).ToList();
            model.UrunList.Insert(0, new SelectListItem { Value = "", Text = "Seçiniz", Selected = true });

            if (Request.IsAjaxRequest()) // index sayfası ilk defa açılmadıysa sadece tabloyu günceller
            {
                return PartialView("_TaleplerimTablo", model);
            }
            else
            {
                return View(model);
            }
        }
        [HttpPost]
        public string TalepKayit(TaleplerimViewModel model)
        {
            try
            {
                int kullaniciID = Convert.ToInt32(Session["Per_ID"]);


                var Ariza = (from x in db.Tbl_Ariza
                             where x.Arz_Kayit == true && x.Env_ID == model.Urun_ID
                             select x).FirstOrDefault();

                if (Ariza != null) return "-1"; // daha önce kayıt oluşturulduysa işleme izin vermeyecek

                Tbl_Ariza arizaYeni = new Tbl_Ariza();
                arizaYeni.Arz_Bilgi = model.TalepBilgiYeni;
                arizaYeni.Env_ID = model.Urun_ID;
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
        [HttpPost]
        public string TalepGuncelleme(TaleplerimViewModel model)
        {
            try
            {

                var Ariza = (from x in db.Tbl_Ariza
                             where x.Arz_ID==model.TalepIDGuncelle && x.Arz_Durum=="Talep" && x.Arz_Kayit==true
                             select x).FirstOrDefault();

                if (Ariza == null) return "-1"; // daha önce kayıt oluşturulduysa işleme izin vermeyecek

                Ariza.Arz_Bilgi = model.TalepBilgiGuncelle;

                db.SaveChanges();

                return "1";
            }
            catch
            {
                return "-1";
            }
        }

        [HttpPost]
        public JsonResult TalepSil(List<string> values)
        {
            if (values == null) return Json(new { Result = "-1" });

            foreach (string ids in values)
            {

                int id = Convert.ToInt32(ids);

                var talepKontrol = (from x in db.Tbl_Servis
                                    where x.Arz_ID == id
                                    select x).FirstOrDefault();

                if (talepKontrol == null)
                {

                    var silTalep = (from x in db.Tbl_Ariza
                                  where x.Arz_ID == id
                                  select x).FirstOrDefault();

                    if (silTalep.Arz_Kayit == true && silTalep.Arz_Durum == "Talep") // daha önce silindiyse herhangi bir işlem yapmasına gerek yok. Zimmetliyse silinemez
                    {
                        silTalep.Arz_Kayit = false;
                        silTalep.Arz_Durum = "Silindi";
                        db.SaveChanges();
                    }
                }else return Json(new { Result = "-2" });

            }
            return Json(new { Result = "1" });
        }


    }
}