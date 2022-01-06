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
    public class EnvanterController : Controller
    {
        private Models.EnvanterServisEntities db = new Models.EnvanterServisEntities();
        // GET: Home
        [LoginControl]
        public ActionResult Index(EnvanterViewModel model)
        {
            model.TabloEnvanterList = (from x in db.Tbl_Envanter.OrderByDescending(f => f.Env_ID)
                                       select new EnvanterViewModel
                                       {
                                           Envanter_ID = x.Env_ID,
                                           EnvanterCinsAdi = x.Tbl_P_EnvanterCinsi.EnvCins_Adi,
                                           EnvanterMarkaAdi = x.Tbl_P_EnvanterMarka.EnvMarka_Adi,
                                           EnvanterModel = x.Env_Model,
                                           EnvanterSeriNo = x.Env_SeriNo,
                                           EnvanterDurum = x.Env_Durum,
                                           EnvanterKayit = (bool)x.Env_Kayit,
                                           EnvanterKayitTarih = (DateTime)x.Env_Tarih,
                                           EnvanterKaydeden_ID = x.Tbl_Personel.Per_ID,
                                           EnvanterKaydeden = x.Tbl_Personel.Per_Ad + " " + x.Tbl_Personel.Per_Soyad,
                                           EnvanterSube = x.Tbl_P_Sube.Sube_Adi,
                                           EnvanterCins_ID= (int)x.EnvCins_ID,
                                           EnvanterSube_ID= (int)x.Sube_ID,
                                           EnvanterMarka_ID= (int)x.EnvMarka_ID,
                                           
                                       }).ToList();

            List<Tbl_P_EnvanterCinsi> cinsilist = db.Tbl_P_EnvanterCinsi.Where(f => f.EnvCins_Kayit == true).OrderBy(f => f.EnvCins_ID).ToList();
            model.CinsiList = (from x in cinsilist
                               select new SelectListItem
                               {
                                   Text = x.EnvCins_Adi,
                                   Value = x.EnvCins_ID.ToString()
                               }).ToList();
            model.CinsiList.Insert(0, new SelectListItem { Value = "", Text = "Seçiniz", Selected = true });

            List<Tbl_P_EnvanterMarka> markalist = db.Tbl_P_EnvanterMarka.Where(f => f.EnvMarka_Kayit == true).OrderBy(f => f.EnvMarka_ID).ToList();
            model.MarkaList = (from x in markalist
                               select new SelectListItem
                               {
                                   Text = x.EnvMarka_Adi,
                                   Value = x.EnvMarka_ID.ToString()
                               }).ToList();
            model.MarkaList.Insert(0, new SelectListItem { Value = "", Text = "Seçiniz", Selected = true });

            List<Tbl_P_Sube> subelist = db.Tbl_P_Sube.Where(f => f.Sube_Kayit == true).OrderBy(f => f.Sube_ID).ToList();
            model.SubeList = (from x in subelist
                              select new SelectListItem
                              {
                                  Text = x.Sube_Adi,
                                  Value = x.Sube_ID.ToString()
                              }).ToList();
            model.SubeList.Insert(0, new SelectListItem { Value = "", Text = "Seçiniz", Selected = true });


            List<Tbl_Personel> personellist = db.Tbl_Personel.Where(f => f.Per_Kayit == true).OrderBy(f => f.Sube_ID).ToList();
            model.ZimmetlenenList = (from x in personellist
                                     select new SelectListItem
                              {
                                  Text = x.Per_Ad+" "+x.Per_Soyad,
                                  Value = x.Per_ID.ToString()
                              }).ToList();
            model.ZimmetlenenList.Insert(0, new SelectListItem { Value = "", Text = "Seçiniz", Selected = true });



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
        public string EnvanterKayit(EnvanterViewModel model)
        {
           
            if (model.Envanter_ID > 0) // envanter 0 ise yeni kayıttır. 0 dan büyükse düzenleme işlemi vardır
            { //düzenleme
                var envanterguncelle= db.Tbl_Envanter.FirstOrDefault(f => f.Env_ID == model.Envanter_ID);
                if (envanterguncelle.Env_SeriNo == model.EnvanterSeriNo) // seri no kontrolü yapar. Aynıysa değişikliği uygular.
                {
                    envanterguncelle.EnvCins_ID = model.EnvanterCins_ID;
                    envanterguncelle.EnvMarka_ID = model.EnvanterMarka_ID;
                    envanterguncelle.Env_Model = model.EnvanterModel;
                    envanterguncelle.Env_SeriNo = model.EnvanterSeriNo;
                    envanterguncelle.Env_Durum = "Zimmetlenebilir";
                    envanterguncelle.Env_Kayit = true;
                    envanterguncelle.Env_Tarih = DateTime.Now;
                    envanterguncelle.Per_ID = Convert.ToInt32(Session["Per_ID"]);
                    envanterguncelle.Sube_ID = model.EnvanterSube_ID;
                    db.SaveChanges();
                }
                else //seri no değiştiyse buraya gelir. Değişen serino kullanıldımı diye kontrol eder
                {
                    var envkontrol = db.Tbl_Envanter.FirstOrDefault(f => f.Env_SeriNo == model.EnvanterSeriNo); // 
                    if (envkontrol == null)
                    {
                        envanterguncelle.EnvCins_ID = model.EnvanterCins_ID;
                        envanterguncelle.EnvMarka_ID = model.EnvanterMarka_ID;
                        envanterguncelle.Env_Model = model.EnvanterModel;
                        envanterguncelle.Env_SeriNo = model.EnvanterSeriNo;
                        envanterguncelle.Env_Durum = "Zimmetlenebilir";
                        envanterguncelle.Env_Kayit = true;
                        envanterguncelle.Env_Tarih = DateTime.Now;
                        envanterguncelle.Per_ID = Convert.ToInt32(Session["Per_ID"]);
                        envanterguncelle.Sube_ID = model.EnvanterSube_ID;
                        db.SaveChanges();
                    }
                    else
                    { // seri numarası kullanılıyorsa -1 döner
                        return "-1";
                    }
                }
                return "0"; // kayıt güncellendiyse 0 döner
            }
            else
            { // Yeni kayıt
                var envkontrol= db.Tbl_Envanter.FirstOrDefault(f => f.Env_SeriNo == model.EnvanterSeriNo);

                if(envkontrol==null)
                {
                    Tbl_Envanter yenienvanter = new Tbl_Envanter();
                    yenienvanter.EnvCins_ID = model.EnvanterCins_ID;
                    yenienvanter.EnvMarka_ID = model.EnvanterMarka_ID;
                    yenienvanter.Env_Model = model.EnvanterModel;
                    yenienvanter.Env_SeriNo = model.EnvanterSeriNo;
                    yenienvanter.Env_Durum = "Zimmetlenebilir";
                    yenienvanter.Env_Kayit = true;
                    yenienvanter.Env_Tarih = DateTime.Now;
                    yenienvanter.Per_ID =Convert.ToInt32( Session["Per_ID"]);
                    yenienvanter.Sube_ID = model.EnvanterSube_ID;

                    db.Tbl_Envanter.Add(yenienvanter);
                    db.SaveChanges();
                }
                else
                { // seri numarası kullanılıyorsa -1 döner
                    return "-1";
                }
                return "1"; // yeni kayıt oluştuysa 1 döner
            }
        }


        [HttpPost]
        public JsonResult EnvanterSil(List<string> values)
        {
            if (values == null) return Json(new { Result = "-1" });

            foreach (string ids in values)
            {
                
                int id = Convert.ToInt32(ids);

                var silEnv = (from x in db.Tbl_Envanter
                              where x.Env_ID == id
                              select x).FirstOrDefault();

                if (silEnv.Env_Kayit == true && silEnv.Env_Durum== "Zimmetlenebilir") // daha önce silindiyse herhangi bir işlem yapmasına gerek yok. Zimmetliyse silinemez
                {
                    silEnv.Per_ID = Convert.ToInt32(Session["Per_ID"]);
                    silEnv.Env_Kayit = false;
                    silEnv.Env_Durum = "Silindi";
                    db.SaveChanges();
                }
               
            }
            return Json(new { Result = "1" });
        }


        [HttpPost]
        public string E_ZimmetKayit(EnvanterViewModel model)
        {
            if (model == null) return "-1";


            foreach(var stringid in model.EnvanterStrng_ID[0].Split(','))
            {
                int id = Convert.ToInt32(stringid);
                var envanter = (from x in db.Tbl_Envanter.Where(f => (f.Env_ID == id))
                                select x).FirstOrDefault();

                if(envanter.Env_Durum == "Zimmetlenebilir" && envanter.Env_Kayit==true)
                {
                    Tbl_Zimmet yeniZimmet = new Tbl_Zimmet();
                    yeniZimmet.Kullanan_ID = model.Zimmetlenen_ID;
                    yeniZimmet.Env_ID = id;
                    yeniZimmet.Zimmetleyen_ID = Convert.ToInt32(Session["Per_ID"]);
                    yeniZimmet.Zim_Tarih = DateTime.Now;
                    yeniZimmet.Zim_IadeTarih = DateTime.Parse("0001-01-01");
                    yeniZimmet.Zim_Aciklama = "";
                    yeniZimmet.Zim_Kayit = true;
                    db.Tbl_Zimmet.Add(yeniZimmet);

                    envanter.Env_Durum = "Zimmetli";
                }
            }
            db.SaveChanges();

            return "1";
        }




    }
}