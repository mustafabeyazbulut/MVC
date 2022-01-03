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
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        OzdilekBtServisTakipEntities db = new OzdilekBtServisTakipEntities();
 
        public ActionResult ServisTakip(ServisViewModel model)
        {
            //Alttaki try içindeki kod  Kullanıcı panelinden admin paneline geçilirse engellemek için
            try
            {
                List<Firma> FirmaList = db.Firma.OrderBy(f => f.FirmaAdi).ToList();
                model.FirmaList = (from s in FirmaList 
                                  select new SelectListItem
                                  {
                                      Text = s.FirmaAdi,
                                      Value = s.FirmaID.ToString()
                                  }).ToList();
                model.FirmaList.Insert(0, new SelectListItem { Value = "", Text = "Seçiniz", Selected = true });

                model.servislist = (from k in db.ServisTb.Where(f =>
                     (string.IsNullOrEmpty(model.servisarama) || f.ServisSeriNo.Contains(model.servisarama) || f.Sube.Contains(model.servisarama) || f.GonderildigiTarih.Contains(model.servisarama))
                                            ).OrderByDescending(f => f.ServisID)
                                      select new ServisViewModel
                                      {
                                          ServisID1 = k.ServisID,
                                          EnvanterID1 = k.EnvanterID,
                                          Sube1 = k.Sube,
                                          Lokasyon1 = k.Lokasyon,
                                          ServisCinsi1 = k.ServisCinsi,
                                          ServisMarka1 = k.ServisMarka,
                                          ServisModel1 = k.ServisModel,
                                          ServisSeriNo1 = k.ServisSeriNo,
                                          ServisOzellik1 = k.ServisOzellik,
                                          GonderenKisi1 = k.GonderenKisi,
                                          GonderildigiTarih1 = k.GonderildigiTarih,
                                          EkAksesuar1 = k.EkAksesuar,
                                          ArizaSebebi1 = k.ArizaSebebi,
                                          ServisDurumu1 = k.ServisDurumu,
                                          Firma1 = k.ServisFirma
                                      }).ToList();

                if (Request.IsAjaxRequest())
                {
                    return PartialView("servislist", model);
                }
                else
                {
                    ViewBag.kadi = Session["KulAdi"];
                    if (Session["UserID"].ToString() == null || Session["UserID"].ToString() == "1")
                    {
                        return View(model);
                    }
                    else if (Session["UserID"].ToString() == null || Session["UserID"].ToString() == "2")
                    {
                        return RedirectToAction("ServisTakipKullanici", "Kullanici");

                    }
                    else if (Session["UserID"].ToString() == null || Session["UserID"].ToString() == "3")
                    {
                        return RedirectToAction("YetkiveTanimlamalar", "Admin");
                    }
                    else
                    {
                        return RedirectToAction("Giris", "Giris");
                    }
                }

                
            }
            catch (Exception)
            {

                return RedirectToAction("Giris", "Giris");
            }

        }
        public ActionResult EnvanterTakip(EnvanterViewModel model)
        {
            try
            {


                List<Sube> SubeList = db.Sube.OrderBy(f => f.SubeAdi).ToList();
                model.SubeList = (from s in SubeList
                                  select new SelectListItem
                                  {
                                      Text = s.SubeAdi,
                                      Value = s.SubeID.ToString()
                                  }).ToList();
                model.SubeList.Insert(0, new SelectListItem { Value = "", Text = "Seçiniz", Selected = true });





                

                List<Lokasyon> LokasyonList = db.Lokasyon.OrderBy(f => f.LokasyonAdi).ToList();
                model.LokasyonList = (from s in LokasyonList
                                   select new SelectListItem
                                   {
                                       Text = s.LokasyonAdi,
                                       Value = s.LokasyonID.ToString()
                                   }).ToList();
                model.LokasyonList.Insert(0, new SelectListItem { Value = "", Text = "Seçiniz", Selected = true });

                List<EnvanterCinsi> EnvanterCinsiList = db.EnvanterCinsi.OrderBy(f => f.EnvanterCinsiAdi).ToList();
                model.EnvanterCinsiList = (from s in EnvanterCinsiList
                                      select new SelectListItem
                                      {
                                          Text = s.EnvanterCinsiAdi,
                                          Value = s.EnvanterCinsiID.ToString()
                                      }).ToList();
                model.EnvanterCinsiList.Insert(0, new SelectListItem { Value = "", Text = "Seçiniz", Selected = true });


                List<Marka> MarkaList = db.Marka.OrderBy(f => f.MarkaAdi).ToList();
                model.MarkaList = (from s in MarkaList
                                  select new SelectListItem
                                  {
                                      Text = s.MarkaAdi,
                                      Value = s.MarkaID.ToString()
                                  }).ToList();
                model.MarkaList.Insert(0, new SelectListItem { Value = "", Text = "Seçiniz", Selected = true });

                List<Firma> FirmaList = db.Firma.OrderBy(f => f.FirmaAdi).ToList();
                model.FirmaList = (from s in FirmaList
                                   select new SelectListItem
                                   {
                                       Text = s.FirmaAdi,
                                       Value = s.FirmaID.ToString()
                                   }).ToList();
                model.FirmaList.Insert(0, new SelectListItem { Value = "", Text = "Seçiniz", Selected = true });

                model.envanterlist = (from k in db.EnvanterTb.Where(f =>
                      (string.IsNullOrEmpty(model.envanterarama) || f.EnvanterSeriNo.Contains(model.envanterarama) || f.Sube.Contains(model.envanterarama))
                                             ).OrderByDescending(f => f.EnvanterID)
                                      select new EnvanterViewModel
                                         {
                                             EnvanterID2 = k.EnvanterID,
                                             Sube2 = k.Sube,
                                             Lokasyon2 = k.Lokasyon,
                                             EnvanterCinsi2 = k.EnvanterCinsi,
                                             EnvanterMarka2 = k.EnvanterMarka,
                                             EnvanterModel2 = k.EnvanterModel,
                                             EnvanterSeriNo2 = k.EnvanterSeriNo,
                                             EnvanterOzellik2 = k.EnvanterOzellik
                                         }).ToList();

                if (Request.IsAjaxRequest())
                {
                    return PartialView("envanterlist", model);
                }
                else
                {
                    ViewBag.kadi = Session["KulAdi"];
                    if (Session["UserID"].ToString() == null || Session["UserID"].ToString() == "1")
                    {
                        return View(model);
                    }
                    else if (Session["UserID"].ToString() == null || Session["UserID"].ToString() == "2")
                    {
                        return RedirectToAction("ServisTakipKullanici", "Kullanici");

                    }
                    else if (Session["UserID"].ToString() == null || Session["UserID"].ToString() == "3")
                    {
                        return RedirectToAction("YetkiveTanimlamalar", "Admin");
                    }
                    else
                    {
                        return RedirectToAction("Giris", "Giris");
                    }
                }


            }
            catch (Exception)
            {

                return RedirectToAction("Giris", "Giris");
            }

        }
       
        public ActionResult ServisGelen(ServisGelenViewModel model)
        {
            try
            {

                model.servisgelenlist = (from k in db.ServisGelenTb.Where(f =>
                      (string.IsNullOrEmpty(model.servisarama) || f.GonderildigiTarih.Contains(model.servisarama) || f.GeldigiTarih.Contains(model.servisarama) || f.ServisGelenSeriNo.Contains(model.servisarama) || f.Sube.Contains(model.servisarama))
                                             ).OrderByDescending(f => f.ServisID)
                                         select new ServisGelenViewModel
                                         {
                                             ServisID4 = k.ServisID,
                                             EnvanterID4 = k.EnvanterID,
                                             Sube4 = k.Sube,
                                             Lokasyon4 = k.Lokasyon,
                                             ServisgelenCinsi4 = k.ServisGelenCinsi,
                                             ServisgelenMarka4 = k.ServisGelenMarka,
                                             ServisgelenModel4 = k.ServisGelenModel,
                                             ServisgelenSeriNo4 = k.ServisGelenSeriNo,
                                             ServisgelenOzellik4 = k.ServisGelenOzellik,
                                             GonderenKisi4 = k.GonderenKisi,
                                             GonderildigiTarih4 = k.GonderildigiTarih,
                                             GeldiginiBelirtenKisi4 = k.GeldiginiBelirtenKisi,
                                             GeldigiTarih4 = k.GeldigiTarih,
                                             EkAksesuar4 = k.EkAksesuar,
                                             ArizaSebebi4 = k.ArizaSebebi,
                                             YapilanMudahale4 = k.YapilanMudahale,
                                             Firma4=k.ServisFirma
                                         }).ToList();

                if (Request.IsAjaxRequest())
                {
                    return PartialView("servisgelenlist", model);
                }
                else
                {
                    ViewBag.kadi = Session["KulAdi"];
                    if (Session["UserID"].ToString() == null || Session["UserID"].ToString() == "1")
                    {
                        return View(model);
                    }
                    else if (Session["UserID"].ToString() == null || Session["UserID"].ToString() == "2")
                    {
                        return RedirectToAction("ServisTakipKullanici", "Kullanici");

                    }
                    else if (Session["UserID"].ToString() == null || Session["UserID"].ToString() == "3")
                    {
                        return RedirectToAction("YetkiveTanimlamalar", "Admin");
                    }
                    else
                    {
                        return RedirectToAction("Giris", "Giris");
                    }
                }


            }
            catch (Exception)
            {

                return RedirectToAction("Giris", "Giris");
            }

        } 

        //public PartialViewResult servislist()
        //{
        //    //11
        //    List<ServisViewModel> servislist = (from k in db.ServisTb
        //                                        select new ServisViewModel
        //                                        {
        //                                            ServisID1=k.ServisID,
        //                                            EnvanterID1 = k.EnvanterID,
        //                                            Sube1 = k.Sube,
        //                                            Lokasyon1 = k.Lokasyon,
        //                                            ServisCinsi1 = k.ServisCinsi,
        //                                            ServisMarka1 = k.ServisMarka,
        //                                            ServisModel1 = k.ServisModel,
        //                                            ServisSeriNo1 = k.ServisSeriNo,
        //                                            ServisOzellik1 = k.ServisOzellik,
        //                                            GonderenKisi1 = k.GonderenKisi,
        //                                            GonderildigiTarih1 = k.GonderildigiTarih,
        //                                            EkAksesuar1 = k.EkAksesuar,
        //                                            ArizaSebebi1 = k.ArizaSebebi,
        //                                            ServisDurumu1 = k.ServisDurumu,
        //                                            Firma1=k.ServisFirma
        //                                        }).OrderByDescending(f => f.ServisID1).ToList();

        //    return PartialView(servislist);
        //}

        //public PartialViewResult envanterlist()
        //{
        //    //11
        //    List<EnvanterViewModel> envanterlist = (from k in db.EnvanterTb
        //                                          select new EnvanterViewModel
        //                                          {
        //                                              EnvanterID2 = k.EnvanterID,
        //                                              Sube2=k.Sube,
        //                                              Lokasyon2=k.Lokasyon,
        //                                              EnvanterCinsi2=k.EnvanterCinsi,
        //                                              EnvanterMarka2=k.EnvanterMarka,
        //                                              EnvanterModel2=k.EnvanterModel,
        //                                              EnvanterSeriNo2=k.EnvanterSeriNo,
        //                                              EnvanterOzellik2=k.EnvanterOzellik
        //                                          }).OrderByDescending(f => f.EnvanterID2).ToList();

        //    return PartialView(envanterlist);
        //}
        //public PartialViewResult servisgelenlist()
        //{
        //    //11
        //    List<ServisGelenViewModel> servisgelenlist = (from k in db.ServisGelenTb
        //                                               select new ServisGelenViewModel
        //                                            {
        //                                                ServisID4=k.ServisID,
        //                                                EnvanterID4 = k.EnvanterID,
        //                                                Sube4 = k.Sube,
        //                                                Lokasyon4 = k.Lokasyon,
        //                                                ServisgelenCinsi4 = k.ServisGelenCinsi,
        //                                                ServisgelenMarka4 = k.ServisGelenMarka,
        //                                                ServisgelenModel4 = k.ServisGelenModel,
        //                                                ServisgelenSeriNo4 = k.ServisGelenSeriNo,
        //                                                ServisgelenOzellik4 = k.ServisGelenOzellik,
        //                                                GonderenKisi4=k.GonderenKisi,
        //                                                GonderildigiTarih4=k.GonderildigiTarih,
        //                                                GeldiginiBelirtenKisi4=k.GeldiginiBelirtenKisi,
        //                                                GeldigiTarih4=k.GeldigiTarih,
        //                                                EkAksesuar4=k.EkAksesuar,
        //                                                ArizaSebebi4=k.ArizaSebebi,
        //                                                YapilanMudahale4=k.YapilanMudahale
        //                                            }).OrderByDescending(f => f.ServisID4).ToList();
        //    return PartialView(servisgelenlist);
        //}


        [HttpPost]
        public string EnvanterCreate(EnvanterViewModel model)
        {
            try
            {
                //11
                EnvanterTb envanterkayit = null;
                Sube subetb = null;
                Lokasyon lokasyontb = null;
                EnvanterCinsi envantercinsitb = null;
                Marka markatb = null;
                EnvanterTb envanterfiltre = null;
  
                             if (model.EnvanterID2 > 0)
                             {
            
                                     //Güncelleme
                                     subetb = db.Sube.FirstOrDefault(f => f.SubeID == model.SubeID);
                                     lokasyontb = db.Lokasyon.FirstOrDefault(f => f.LokasyonID == model.LokasyonID);
                                     envantercinsitb = db.EnvanterCinsi.FirstOrDefault(f => f.EnvanterCinsiID == model.EnvanterCinsiID);
                                     markatb = db.Marka.FirstOrDefault(f => f.MarkaID == model.MarkaID);


                                     envanterkayit = db.EnvanterTb.FirstOrDefault(f => f.EnvanterID == model.EnvanterID2);
                                        //mevcut envanter id de ki seri no ile modelden gelen seri no 
                                        //karşılaştığında aynı ise kaydı gunceller değil ise farklı alanda yeni girilen seri no var mı diye bakar
                                     if (envanterkayit.EnvanterSeriNo == model.EnvanterSeriNo2)
                                     {
                                         envanterkayit.Sube = subetb.SubeAdi;
                                         envanterkayit.Lokasyon = lokasyontb.LokasyonAdi;
                                         envanterkayit.EnvanterCinsi = envantercinsitb.EnvanterCinsiAdi;
                                         envanterkayit.EnvanterMarka = markatb.MarkaAdi;
                                         envanterkayit.EnvanterModel = model.EnvanterModel2;
                                         envanterkayit.EnvanterSeriNo = model.EnvanterSeriNo2;
                                         envanterkayit.EnvanterOzellik = model.EnvanterOzellik2;
                                     }
                                     else
                                     {
                                                         //Seri no aynı değilse kayıt
                                                         envanterfiltre = db.EnvanterTb.FirstOrDefault(f => f.EnvanterSeriNo == model.EnvanterSeriNo2);
                                                         if (envanterfiltre == null)
                                                         {
                                                             envanterkayit.Sube = subetb.SubeAdi;
                                                             envanterkayit.Lokasyon = lokasyontb.LokasyonAdi;
                                                             envanterkayit.EnvanterCinsi = envantercinsitb.EnvanterCinsiAdi;
                                                             envanterkayit.EnvanterMarka = markatb.MarkaAdi;
                                                             envanterkayit.EnvanterModel = model.EnvanterModel2;
                                                             envanterkayit.EnvanterSeriNo = model.EnvanterSeriNo2;
                                                             envanterkayit.EnvanterOzellik = model.EnvanterOzellik2;
                                                         }
                                                         else
                                                         {
                                                             //aynı seri no var ise 11 döner.
                                                             return "11";
                                                         }
                                     }
                                     db.SaveChanges();
                                     return "2";

                             }
                             else
                             {
                                         //Seri no aynı değilse kayıt
                                         envanterfiltre = db.EnvanterTb.FirstOrDefault(f => f.EnvanterSeriNo == model.EnvanterSeriNo2);
                                         if (envanterfiltre == null)
                                         {
                                 //Yeni Kayıt
                                 envanterkayit = new EnvanterTb();


                                 subetb = db.Sube.FirstOrDefault(f => f.SubeID == model.SubeID);
                                 lokasyontb = db.Lokasyon.FirstOrDefault(f => f.LokasyonID == model.LokasyonID);
                                 envantercinsitb = db.EnvanterCinsi.FirstOrDefault(f => f.EnvanterCinsiID == model.EnvanterCinsiID);
                                 markatb = db.Marka.FirstOrDefault(f => f.MarkaID == model.MarkaID);


                                 envanterkayit.Sube = subetb.SubeAdi;
                                 envanterkayit.Lokasyon = lokasyontb.LokasyonAdi;
                                 envanterkayit.EnvanterCinsi = envantercinsitb.EnvanterCinsiAdi;
                                 envanterkayit.EnvanterMarka = markatb.MarkaAdi;
                                 envanterkayit.EnvanterModel = model.EnvanterModel2;
                                 envanterkayit.EnvanterSeriNo = model.EnvanterSeriNo2;
                                 envanterkayit.EnvanterOzellik = model.EnvanterOzellik2;
                                
                                         }
                                         else
                                         {
                                         return "11";
                                         }
                             }
                             if (model.EnvanterID2 == 0)
                                 db.EnvanterTb.Add(envanterkayit);
                             db.SaveChanges();
                             return "1";
         

            }
            catch
            {
                return "-1";
            }

        }
        [HttpPost]
        public string EnvanterDelete(int id)
        {
            try
            {
                //11
                var envantersil = db.EnvanterTb.FirstOrDefault(f => f.EnvanterID == id);
                db.EnvanterTb.Remove(envantersil);
                db.SaveChanges();
                return "2";
            }
            catch
            {
                
                return "-2";
            }
        }
        [HttpPost]
        public JsonResult EnvanterGetir(int id)
        {
            //11
      
                EnvanterTb envanterkayit = db.EnvanterTb.FirstOrDefault(f => f.EnvanterID == id);
                Sube subekay = db.Sube.FirstOrDefault(f => f.SubeAdi == envanterkayit.Sube);
                Lokasyon lokasyonkay = db.Lokasyon.FirstOrDefault(f => f.LokasyonAdi == envanterkayit.Lokasyon);
                EnvanterCinsi envantercinsikay = db.EnvanterCinsi.FirstOrDefault(f => f.EnvanterCinsiAdi == envanterkayit.EnvanterCinsi);
                Marka markakay = db.Marka.FirstOrDefault(f => f.MarkaAdi == envanterkayit.EnvanterMarka);

                EnvanterViewModel model4 = new EnvanterViewModel();

                    model4.EnvanterID2 = envanterkayit.EnvanterID;
                    model4.EnvanterModel2 = envanterkayit.EnvanterModel;
                    model4.EnvanterSeriNo2 = envanterkayit.EnvanterSeriNo;
                    model4.EnvanterOzellik2 = envanterkayit.EnvanterOzellik;
                    model4.SubeID = subekay.SubeID;
                    model4.LokasyonID = lokasyonkay.LokasyonID;
                    model4.EnvanterCinsiID = envantercinsikay.EnvanterCinsiID;
                    model4.MarkaID = markakay.MarkaID;

                return Json(model4, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        public string ServisDelete(int id)
        {
            try
            {
                //11
                var servissil = db.ServisTb.FirstOrDefault(f => f.ServisID== id);
                db.ServisTb.Remove(servissil);
                db.SaveChanges();
                return "3";
            }
            catch
            {

                return "-3";
            }
        }
        [HttpPost]
        public string Create(ServisViewModel model3,EnvanterViewModel model5)
        {

            try
            {
               //11
                ServisTb serviskayit = null;
                ServisTb servisfiltre = null;

                if (model3.EnvanterID1 > 0)
                {

                    serviskayit = db.ServisTb.FirstOrDefault(f => f.ServisID == model3.ServisID1);
                    Firma firmakay = db.Firma.FirstOrDefault(f => f.FirmaID == model5.FirmaID);

                    serviskayit.EnvanterID = model3.EnvanterID1;
                    serviskayit.Sube = model3.Sube1;
                    serviskayit.Lokasyon = model3.Lokasyon1;
                    serviskayit.ServisMarka = model3.ServisMarka1;
                    serviskayit.ServisModel = model3.ServisModel1;
                    serviskayit.ServisSeriNo = model3.ServisSeriNo1;
                    //serviskayit.GonderenKisi = Session["KulAdi"].ToString();
                    serviskayit.GonderenKisi = model3.GonderenKisi1;
                    serviskayit.GonderildigiTarih = model3.GonderildigiTarih1;
                    serviskayit.EkAksesuar = model3.EkAksesuar1;
                    serviskayit.ArizaSebebi = model3.ArizaSebebi1;
                    serviskayit.ServisDurumu = model3.ServisDurumu1;
                    serviskayit.ServisFirma = firmakay.FirmaAdi;
                }
                else
                {
                    EnvanterTb envanterkayits = db.EnvanterTb.FirstOrDefault(f => f.EnvanterID == model5.EnvanterID3);
                    Firma firmakay = db.Firma.FirstOrDefault(f => f.FirmaID == model5.FirmaID);
                    
                      //Seri no aynı değilse kayıt
                                                         servisfiltre = db.ServisTb.FirstOrDefault(f => f.EnvanterID == model5.EnvanterID3);
                                                         if (servisfiltre == null)
                                                         {

                                                             serviskayit = new ServisTb();

                                                             serviskayit.EnvanterID = envanterkayits.EnvanterID;
                                                             serviskayit.Sube = envanterkayits.Sube;
                                                             serviskayit.Lokasyon = envanterkayits.Lokasyon;
                                                             serviskayit.ServisCinsi = envanterkayits.EnvanterCinsi;
                                                             serviskayit.ServisMarka = envanterkayits.EnvanterMarka;
                                                             serviskayit.ServisModel = envanterkayits.EnvanterModel;
                                                             serviskayit.ServisSeriNo = envanterkayits.EnvanterSeriNo;
                                                             serviskayit.ServisOzellik = envanterkayits.EnvanterOzellik;
                                                             serviskayit.GonderenKisi = Session["KulAdi"].ToString();
                                                             serviskayit.GonderildigiTarih = DateTime.Now.ToString();
                                                             serviskayit.EkAksesuar = model5.EkAksesuar3;
                                                             serviskayit.ArizaSebebi = model5.ArizaSebebi3;
                                                             serviskayit.ServisDurumu = model5.ServisDurumu3;
                                                             serviskayit.ServisFirma = firmakay.FirmaAdi;
                                                         }
                                                         else
                                                         {
                                                             return "11";
                                                         }

                }

                    //if (model3.secildimigeldi == true & model3.secildimisil == true)
                    //{                    }

 
                if (model3.ServisID1 == 0)
                    db.ServisTb.Add(serviskayit);
                    db.SaveChanges();         
                return "1";
            }
            catch 
            {
               return "-1";
            }

        }
        [HttpPost]
        public JsonResult ServisGetir(int id)
        {
            //11


            try
            {
                ServisTb serviskayit = db.ServisTb.FirstOrDefault(f => f.ServisID == id);
                Firma firmakay = db.Firma.FirstOrDefault(f => f.FirmaAdi == serviskayit.ServisFirma);

                ServisViewModel model3 = new ServisViewModel();
                model3.ServisID1 = serviskayit.ServisID;
                model3.EnvanterID1 = serviskayit.EnvanterID;
                model3.Sube1 = serviskayit.Sube;
                model3.Lokasyon1 = serviskayit.Lokasyon;
                model3.ServisMarka1 = serviskayit.ServisMarka;
                model3.ServisModel1 = serviskayit.ServisModel;
                model3.ServisSeriNo1 = serviskayit.ServisSeriNo;
                model3.GonderenKisi1 = serviskayit.GonderenKisi;
                model3.GonderildigiTarih1 = serviskayit.GonderildigiTarih;
                model3.EkAksesuar1 = serviskayit.EkAksesuar;
                model3.ArizaSebebi1 = serviskayit.ArizaSebebi;
                model3.ServisDurumu1 = serviskayit.ServisDurumu;
                model3.Firma1 = serviskayit.ServisFirma;
                model3.FirmaID = firmakay.FirmaID;

                return Json(model3, JsonRequestBehavior.AllowGet);
            }
            catch 
            {
                ServisTb serviskayit = db.ServisTb.FirstOrDefault(f => f.ServisID == id);
                
                ServisViewModel model3 = new ServisViewModel();
                model3.ServisID1 = serviskayit.ServisID;
                model3.EnvanterID1 = serviskayit.EnvanterID;
                model3.Sube1 = serviskayit.Sube;
                model3.Lokasyon1 = serviskayit.Lokasyon;
                model3.ServisMarka1 = serviskayit.ServisMarka;
                model3.ServisModel1 = serviskayit.ServisModel;
                model3.ServisSeriNo1 = serviskayit.ServisSeriNo;
                model3.GonderenKisi1 = serviskayit.GonderenKisi;
                model3.GonderildigiTarih1 = serviskayit.GonderildigiTarih;
                model3.EkAksesuar1 = serviskayit.EkAksesuar;
                model3.ArizaSebebi1 = serviskayit.ArizaSebebi;
                model3.ServisDurumu1 = serviskayit.ServisDurumu;
                model3.Firma1 = serviskayit.ServisFirma;
                model3.FirmaID = 0;

                return Json(model3, JsonRequestBehavior.AllowGet);
                
            }
        }

        [HttpPost]
        public string ServisGelenCreate(ServisGelenViewModel model6,ServisViewModel model7,string deleteTR)
        {

            try
            {
                //11
                ServisGelenTb servisgelenkayit = null;
                
                
                ServisTb serviskayits = db.ServisTb.FirstOrDefault(f => f.ServisID == model7.ServisID2);

                servisgelenkayit = new ServisGelenTb();

                servisgelenkayit.ServisID=serviskayits.ServisID;
                servisgelenkayit.EnvanterID=serviskayits.EnvanterID;
                servisgelenkayit.Sube=serviskayits.Sube;
                servisgelenkayit.Lokasyon=serviskayits.Lokasyon;
                servisgelenkayit.ServisGelenCinsi=serviskayits.ServisCinsi;
                servisgelenkayit.ServisGelenMarka=serviskayits.ServisMarka;
                servisgelenkayit.ServisGelenModel=serviskayits.ServisModel;
                servisgelenkayit.ServisGelenSeriNo=serviskayits.ServisSeriNo;
                servisgelenkayit.ServisGelenOzellik=serviskayits.ServisOzellik;
                servisgelenkayit.GonderenKisi=serviskayits.GonderenKisi;
                servisgelenkayit.GonderildigiTarih = serviskayits.GonderildigiTarih;
                servisgelenkayit.GeldiginiBelirtenKisi=Session["KulAdi"].ToString();
                servisgelenkayit.GeldigiTarih = DateTime.Now.ToString();
                servisgelenkayit.EkAksesuar=serviskayits.EkAksesuar;
                servisgelenkayit.ArizaSebebi=serviskayits.ArizaSebebi;
                servisgelenkayit.YapilanMudahale=model7.ServisDurumu2;
                servisgelenkayit.ServisFirma = serviskayits.ServisFirma ;  
                    
                db.ServisGelenTb.Add(servisgelenkayit);
                db.SaveChanges();

                return "6";
                 
             

                //if (model3.secildimigeldi == true & model3.secildimisil == true)
                //{                    }    
                    
            }
            catch
            {
                return "-6";
            }
            

        }

        public void EnvanterExportToExcel()
        {
                            string dosyaAdi = "EnvanterListesi";
                            //var table = db.EnvanterTb.OrderByDescending(f => f.EnvanterID).ToList();
                            var table = db.EnvanterTb.OrderByDescending(f => f.EnvanterID).ToList();
            
                            var grid = new GridView();
                            grid.DataSource = table;
                            grid.DataBind();
 
                            Response.ClearContent();
                            Response.AddHeader("content-disposition", "attachment; filename=" + dosyaAdi + ".xls");
 
                            Response.ContentType = "application/excel";
                            Response.ContentEncoding = System.Text.Encoding.Unicode;
                            Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
                            StringWriter sw = new StringWriter();

                            HtmlTextWriter htw = new HtmlTextWriter(sw);
 
                            grid.RenderControl(htw);
 
                            Response.Write(sw.ToString());
                            Response.End();
        
        }
        public void ServisGelenExportToExcel()
        {
            string dosyaAdi = "ServisGelenListesi";
            //var table = db.EnvanterTb.OrderByDescending(f => f.EnvanterID).ToList();
            var table = db.ServisGelenTb.OrderByDescending(f => f.ServisID).ToList();

            var grid = new GridView();
            grid.DataSource = table;
            grid.DataBind();

            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=" + dosyaAdi + ".xls");

            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.Unicode;
            Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            grid.RenderControl(htw);

            Response.Write(sw.ToString());
            Response.End();

        }
        public void ServisExportToExcel(ServisViewModel model)
        { 
            string dosyaAdi = "ServisListesi";
            DataTable dtEmployees = new DataTable();

            var table = db.ServisTb.OrderByDescending(f => f.ServisID).ToList();
                
            var grid = new GridView();
            grid.DataSource = table;

            grid.DataBind();

            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=" + dosyaAdi + ".xls");

            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.Unicode;
            Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            grid.RenderControl(htw);

            Response.Write(sw.ToString());
            Response.End();

        }

       }
}
