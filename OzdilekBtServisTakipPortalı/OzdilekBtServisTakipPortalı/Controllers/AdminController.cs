using System;
using OzdilekBtServisTakipPortalı.Models.Giris;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OzdilekBtServisTakipPortalı.Models;
using OzdilekBtServisTakipPortalı.ViewModel;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;
using System.Data;
using OfficeOpenXml;

namespace OzdilekBtServisTakipPortalı.Controllers
{
    [ControlLogin]
    public class AdminController : Controller
    {
        //
        // GET: /Admin/
        OzdilekBtServisTakipEntities db = new OzdilekBtServisTakipEntities();
        public ActionResult YetkiveTanimlamalar(AdminViewModel model)
        {
            try
            {
                model.Kullanicilist = (from k in db.KullaniciTb.Where(f =>
                    (string.IsNullOrEmpty(model.arama) || f.KullaniciAdi.Contains(model.arama))
                                           ).OrderByDescending(f => f.KullaniciID)
                                   select new AdminViewModel
                                   {
                                       KullaniciID=k.KullaniciID,
                                       KullaniciAdi=k.KullaniciAdi,
                                       KullaniciSifre=k.Sifre,
                                       KullaniciSonGirisTarihi=k.SonGirisTarihi,
                                       KullaniciYetki=k.KullaniciYetki
                                   }).ToList();
                  model.Lokasyonlist = (from k in db.Lokasyon.Where(f =>
                      (string.IsNullOrEmpty(model.arama) || f.LokasyonAdi.Contains(model.arama))
                                             ).OrderByDescending(f => f.LokasyonID)
                                    select new AdminViewModel
                                    {
                                        LokasyonID=k.LokasyonID,
                                        LokasyonAdi=k.LokasyonAdi
                                    }).ToList();
                  model.Subelist = (from k in db.Sube.Where(f =>
                      (string.IsNullOrEmpty(model.arama) || f.SubeAdi.Contains(model.arama))
                                             ).OrderByDescending(f => f.SubeID)
                                    select new AdminViewModel
                                    {
                                        SubeID = k.SubeID,
                                        SubeAdi = k.SubeAdi
                                    }).ToList();

                  if (Request.IsAjaxRequest())
                  {
                      return PartialView("adminlist", model);
                  }
                  else
                  {
                      ViewBag.kadi = Session["KulAdi"];
                      if (Session["UserID"].ToString() == null || Session["UserID"].ToString() == "1")
                      {
                          return RedirectToAction("ServisTakip", "Home");
                      }
                      else if (Session["UserID"].ToString() == null || Session["UserID"].ToString() == "2")
                      {

                          return RedirectToAction("ServisTakipKullanici", "Kullanici");

                      }
                      else if (Session["UserID"].ToString() == null || Session["UserID"].ToString() == "3")
                      {

                          return View(model);
                          
                      }
                      else
                      {
                          return RedirectToAction("Giris", "Giris");
                      }
                  }
            }
            catch 
            {
                return RedirectToAction("Giris", "Giris");
            }
        }

        public ActionResult YetkiveTanimlamalar1(Admin1ViewModel model)
        {
            try
            {
                model.Markalist = (from k in db.Marka.Where(f =>
                    (string.IsNullOrEmpty(model.arama) || f.MarkaAdi.Contains(model.arama))
                                           ).OrderByDescending(f => f.MarkaID)
                                   select new Admin1ViewModel
                                   {
                                       MarkaID = k.MarkaID,
                                       MarkaAdi = k.MarkaAdi
                                   }).ToList();

                model.Firmalist = (from k in db.Firma.Where(f =>
                     (string.IsNullOrEmpty(model.arama1) || f.FirmaAdi.Contains(model.arama1))
                                            ).OrderByDescending(f => f.FirmaID)
                                   select new Admin1ViewModel
                                   {
                                       FirmaID = k.FirmaID,
                                       FirmaAdi = k.FirmaAdi
                                   }).ToList();

                if (Request.IsAjaxRequest())
                {
                    return PartialView("adminlist1", model);
                }
                else
                {
                    ViewBag.kadi = Session["KulAdi"];
                    if (Session["UserID"].ToString() == null || Session["UserID"].ToString() == "1")
                    {
                        return RedirectToAction("ServisTakip", "Home");
                    }
                    else if (Session["UserID"].ToString() == null || Session["UserID"].ToString() == "2")
                    {

                        return RedirectToAction("ServisTakipKullanici", "Kullanici");

                    }
                    else if (Session["UserID"].ToString() == null || Session["UserID"].ToString() == "3")
                    {

                        return View(model);

                    }
                    else
                    {
                        return RedirectToAction("Giris", "Giris");
                    }
                }
            }
            catch
            {
                return RedirectToAction("Giris", "Giris");
            }
        }

        //Marka Tablosu İşlemleri
        [HttpPost]
        public string MarkaCreate(Admin1ViewModel model)
        {
            try
            {
                Marka Markatablosu = null;
                if (model.MarkaID > 0)
                {
                    Markatablosu = db.Marka.FirstOrDefault(f => f.MarkaID == model.MarkaID);
                    Markatablosu.MarkaAdi = model.MarkaAdi;
                    db.SaveChanges();
                    return "2";
                }
                else
                {
                    //Yeni Kayıt
                    Markatablosu = new Marka();
                    Markatablosu.MarkaAdi = model.MarkaAdi;
                }
                if (model.MarkaID == 0)
                    db.Marka.Add(Markatablosu);
                db.SaveChanges();
                return "1";

            }
            catch
            {
                return "-1";
            }

        }
        [HttpPost]
        public JsonResult MarkaGetir(int id, Admin1ViewModel model)
        {
            Marka Markatablosu = db.Marka.FirstOrDefault(f => f.MarkaID == id);
            model.MarkaID = Markatablosu.MarkaID;
            model.MarkaAdi = Markatablosu.MarkaAdi;
            return Json(model, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public string MarkaDelete(int id)
        {
            try
            {
                //11
                var markadelete = db.Marka.FirstOrDefault(f => f.MarkaID == id);
                db.Marka.Remove(markadelete);
                db.SaveChanges();
                return "2";
            }
            catch
            {

                return "-2";
            }
        }
        //Marka Tablosu İşlemleri



        //Firma Tablosu İşlemleri
        [HttpPost]
        public string FirmaCreate(Admin1ViewModel model)
        {
            try
            {
                Firma Firmatablosu = null;
                if (model.FirmaID > 0)
                {
                    Firmatablosu = db.Firma.FirstOrDefault(f => f.FirmaID == model.FirmaID);
                    Firmatablosu.FirmaAdi = model.FirmaAdi;
                    db.SaveChanges();
                    return "2";
                }
                else
                {
                    //Yeni Kayıt
                    Firmatablosu = new Firma();
                    Firmatablosu.FirmaAdi = model.FirmaAdi;
                }
                if (model.FirmaID == 0)
                    db.Firma.Add(Firmatablosu);
                db.SaveChanges();
                return "1";

            }
            catch
            {
                return "-1";
            }

        }
        [HttpPost]
        public JsonResult FirmaGetir(int id, Admin1ViewModel model)
        {
            Firma Firmatablosu = db.Firma.FirstOrDefault(f => f.FirmaID == id);
            model.FirmaID = Firmatablosu.FirmaID;
            model.FirmaAdi = Firmatablosu.FirmaAdi;
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public string FirmaDelete(int id)
        {
            try
            {
                //11
                var firmadelete = db.Firma.FirstOrDefault(f => f.FirmaID == id);
                db.Firma.Remove(firmadelete);
                db.SaveChanges();
                return "2";
            }
            catch
            {

                return "-2";
            }
        }
        //Firma Tablosu İşlemleri


        //Kullanici Tablosu İşlemleri
        [HttpPost]
        public string KullaniciCreate(AdminViewModel model)
        {
            try
            {
                if (model.KullaniciYetki != 0)
                {
                    KullaniciTb kullanicitablosu = null;
                    if (model.KullaniciID > 0)
                    {

                        kullanicitablosu = db.KullaniciTb.FirstOrDefault(f => f.KullaniciID == model.KullaniciID);
                        kullanicitablosu.KullaniciAdi = model.KullaniciAdi;
                        kullanicitablosu.Sifre = model.KullaniciSifre;
                        kullanicitablosu.KullaniciYetki = model.KullaniciYetki;
                        kullanicitablosu.SonGirisTarihi = model.KullaniciSonGirisTarihi;
                        db.SaveChanges();
                        return "2";

                    }
                    else
                    {
                        //Yeni Kayıt
                        kullanicitablosu = new KullaniciTb();
                        kullanicitablosu.KullaniciAdi = model.KullaniciAdi;
                        kullanicitablosu.Sifre = model.KullaniciSifre;
                        kullanicitablosu.KullaniciYetki = model.KullaniciYetki;
                        kullanicitablosu.SonGirisTarihi = DateTime.Now.ToString();

                    }
                    if (model.KullaniciID == 0)
                        db.KullaniciTb.Add(kullanicitablosu);
                    db.SaveChanges();
                    return "1";
                }
                return "0";
            }
            catch
            {
                return "-1";
            }

        }
        [HttpPost]
        public JsonResult KullaniciGetir(int id, AdminViewModel model)
        {
            
            KullaniciTb kullanicitablosu = db.KullaniciTb.FirstOrDefault(f => f.KullaniciID == id);
            //AdminViewModel model = new AdminViewModel();

            model.KullaniciID = kullanicitablosu.KullaniciID;
            model.KullaniciAdi = kullanicitablosu.KullaniciAdi;
            model.KullaniciSifre = kullanicitablosu.Sifre;
            model.KullaniciSonGirisTarihi = kullanicitablosu.SonGirisTarihi;
            model.KullaniciYetki = kullanicitablosu.KullaniciYetki;

            return Json(model, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public string KullaniciDelete(int id)
        {
            try
            {
                //11
                var kullanicidelete = db.KullaniciTb.FirstOrDefault(f => f.KullaniciID == id);
                db.KullaniciTb.Remove(kullanicidelete);
                db.SaveChanges();
                return "2";
            }
            catch
            {

                return "-2";
            }
        }
        //Kullanici Tablosu İşlemleri


        //ŞubeTablosu İşlemleri
        [HttpPost]
        public string SubeCreate(AdminViewModel model)
        {
            try
            {
                    Sube subetablosu = null;
                    if (model.SubeID > 0)
                    {
                        subetablosu = db.Sube.FirstOrDefault(f => f.SubeID == model.SubeID);
                        subetablosu.SubeAdi = model.SubeAdi;
                        db.SaveChanges();
                        return "2";
                    }
                    else
                    {
                        //Yeni Kayıt
                        subetablosu = new Sube();
                        subetablosu.SubeAdi = model.SubeAdi;
                    }
                    if (model.SubeID == 0)
                        db.Sube.Add(subetablosu);
                    db.SaveChanges();
                    return "1";
             
            }
            catch
            {
                return "-1";
            }

        }
        [HttpPost]
        public JsonResult SubeGetir(int id, AdminViewModel model)
        {
            Sube subetablosu = db.Sube.FirstOrDefault(f => f.SubeID == id);
            model.SubeID = subetablosu.SubeID;
            model.SubeAdi = subetablosu.SubeAdi;
            return Json(model, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public string SubeDelete(int id)
        {
            try
            {
                //11
                var subedelete = db.Sube.FirstOrDefault(f => f.SubeID == id);
                db.Sube.Remove(subedelete);
                db.SaveChanges();
                return "2";
            }
            catch
            {

                return "-2";
            }
        }
        //ŞubeTablosu İşlemleri


        //LokasyonTablosu İşlemleri
        [HttpPost]
        public string LokasyonCreate(AdminViewModel model)
        {
            try
            {
                Lokasyon Lokasyontablosu = null;
                if (model.LokasyonID > 0)
                {
                    Lokasyontablosu = db.Lokasyon.FirstOrDefault(f => f.LokasyonID == model.LokasyonID);
                    Lokasyontablosu.LokasyonAdi = model.LokasyonAdi;
                    db.SaveChanges();
                    return "2";
                }
                else
                {
                    //Yeni Kayıt
                    Lokasyontablosu = new Lokasyon();
                    Lokasyontablosu.LokasyonAdi = model.LokasyonAdi;
                }
                if (model.LokasyonID == 0)
                    db.Lokasyon.Add(Lokasyontablosu);
                db.SaveChanges();
                return "1";

            }
            catch
            {
                return "-1";
            }

        }
        [HttpPost]
        public JsonResult LokasyonGetir(int id, AdminViewModel model)
        {
            Lokasyon Lokasyontablosu = db.Lokasyon.FirstOrDefault(f => f.LokasyonID == id);
            model.LokasyonID = Lokasyontablosu.LokasyonID;
            model.LokasyonAdi = Lokasyontablosu.LokasyonAdi;
            return Json(model, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public string LokasyonDelete(int id)
        {
            try
            {
                //11
                var lokasyondelete = db.Lokasyon.FirstOrDefault(f => f.LokasyonID == id);
                db.Lokasyon.Remove(lokasyondelete);
                db.SaveChanges();
                return "2";
            }
            catch
            {

                return "-2";
            }
        }
        //LokasyonTablosu İşlemleri

    }
}
