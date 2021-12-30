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
    public class KullaniciController : Controller
    {
        //
        // GET: /Kullanici/
        OzdilekBtServisTakipEntities db = new OzdilekBtServisTakipEntities();
        public ActionResult ServisTakipKullanici(KullaniciViewModel model)
        {
          
            try
            {
                //(from k in db.ServisTb.Where(f =>
                //      (string.IsNullOrEmpty(model.Servisarama) || f.ServisSeriNo.Contains(model.Servisarama))
                //                             ).OrderByDescending(f => f.ServisID)

                

                model.subekullanici = Session["KulAdi"].ToString();
   
                model.servislist = (from k in db.ServisTb.Where(f =>
                      (f.Sube.Contains(model.subekullanici) )
                                             ).OrderByDescending(f => f.ServisID)
                                         select new KullaniciViewModel
                                         {
                                             ServisID=k.ServisID,
                                             EnvanterID=k.EnvanterID,
                                             Sube=k.Sube,
                                             Lokasyon=k.Lokasyon,
                                             ServisCinsi=k.ServisCinsi,
                                             ServisMarka=k.ServisMarka,
                                             ServisModel=k.ServisModel,
                                             ServisSeriNo=k.ServisSeriNo,
                                             ServisOzellik=k.ServisOzellik,
                                             GonderenKisi=k.GonderenKisi,
                                             GonderildigiTarih=k.GonderildigiTarih,
                                             EkAksesuar=k.EkAksesuar,
                                             ArizaSebebi=k.ArizaSebebi,
                                             ServisDurumu=k.ServisDurumu,
                                             Firma=k.ServisFirma 
                                         }).ToList();
                model.servislist = (from k in model.servislist.Where(f =>
                     (string.IsNullOrEmpty(model.Servisarama) || f.ServisSeriNo.Contains(model.Servisarama))
                                            ).OrderByDescending(f => f.ServisID)
                                    select new KullaniciViewModel
                                    {
                                        ServisID = k.ServisID,
                                        EnvanterID = k.EnvanterID,
                                        Sube = k.Sube,
                                        Lokasyon = k.Lokasyon,
                                        ServisCinsi = k.ServisCinsi,
                                        ServisMarka = k.ServisMarka,
                                        ServisModel = k.ServisModel,
                                        ServisSeriNo = k.ServisSeriNo,
                                        ServisOzellik = k.ServisOzellik,
                                        GonderenKisi = k.GonderenKisi,
                                        GonderildigiTarih = k.GonderildigiTarih,
                                        EkAksesuar = k.EkAksesuar,
                                        ArizaSebebi = k.ArizaSebebi,
                                        ServisDurumu = k.ServisDurumu,
                                        Firma = k.Firma
                                    }).ToList();
                


                  if (Request.IsAjaxRequest())
                  {
                      return PartialView("kullaniciservislist", model);

                  }
                  else
                  {

                      //Alttaki try içindeki kod  admin panelinden kullanıcı paneline geçilirse engellemek için
                      ViewBag.kadi = Session["KulAdi"];
                      if (Session["UserID"].ToString() == null || Session["UserID"].ToString() == "1")
                      {
                          return RedirectToAction("ServisTakip", "Home");
                      }
                      else if (Session["UserID"].ToString() == null || Session["UserID"].ToString() == "2")
                      {
                         
                          return View(model);

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

        public ActionResult ServisTakipKullanici1(KullaniciViewModel model)
        {
            try
            {
                model.subekullanici = Session["KulAdi"].ToString();
                model.servisgelenlist = (from c in db.ServisGelenTb.Where(f =>
                                     (f.Sube.Contains(model.subekullanici))).OrderByDescending(f => f.ServisID)
                                         select new KullaniciViewModel
                                         {
                                             ServisID4 = c.ServisID,
                                             EnvanterID4 = c.EnvanterID,
                                             Sube4 = c.Sube,
                                             Lokasyon4 = c.Lokasyon,
                                             ServisgelenCinsi4 = c.ServisGelenCinsi,
                                             ServisgelenMarka4 = c.ServisGelenMarka,
                                             ServisgelenModel4 = c.ServisGelenModel,
                                             ServisgelenSeriNo4 = c.ServisGelenSeriNo,
                                             ServisgelenOzellik4 = c.ServisGelenOzellik,
                                             GonderenKisi4 = c.GonderenKisi,
                                             GonderildigiTarih4 = c.GonderildigiTarih,
                                             GeldiginiBelirtenKisi4 = c.GeldiginiBelirtenKisi,
                                             GeldigiTarih4 = c.GeldigiTarih,
                                             EkAksesuar4 = c.EkAksesuar,
                                             ArizaSebebi4 = c.ArizaSebebi,
                                             YapilanMudahale4 = c.YapilanMudahale,
                                             Firma4 = c.ServisFirma
                                         }).ToList();
                model.servisgelenlist = (from c in model.servisgelenlist.Where(f =>
                     (string.IsNullOrEmpty(model.ServisGelenarama) || f.ServisgelenSeriNo4.Contains(model.ServisGelenarama))
                                            ).OrderByDescending(f => f.ServisID)
                                         select new KullaniciViewModel
                                         {
                                             ServisID4 = c.ServisID4,
                                             EnvanterID4 = c.EnvanterID4,
                                             Sube4 = c.Sube4,
                                             Lokasyon4 = c.Lokasyon4,
                                             ServisgelenCinsi4 = c.ServisgelenCinsi4,
                                             ServisgelenMarka4 = c.ServisgelenMarka4,
                                             ServisgelenModel4 = c.ServisgelenModel4,
                                             ServisgelenSeriNo4 = c.ServisgelenSeriNo4,
                                             ServisgelenOzellik4 = c.ServisgelenOzellik4,
                                             GonderenKisi4 = c.GonderenKisi4,
                                             GonderildigiTarih4 = c.GonderildigiTarih4,
                                             GeldiginiBelirtenKisi4 = c.GeldiginiBelirtenKisi4,
                                             GeldigiTarih4 = c.GeldigiTarih4,
                                             EkAksesuar4 = c.EkAksesuar4,
                                             ArizaSebebi4 = c.ArizaSebebi4,
                                             YapilanMudahale4 = c.YapilanMudahale4,
                                             Firma4 = c.Firma4
                                         }).ToList();



                if (Request.IsAjaxRequest())
                {
                    return PartialView("kullaniciservisgelenlist", model);

                }
                else
                {

                    //Alttaki try içindeki kod  admin panelinden kullanıcı paneline geçilirse engellemek için
                    ViewBag.kadi = Session["KulAdi"];
                    if (Session["UserID"].ToString() == null || Session["UserID"].ToString() == "1")
                    {
                        return RedirectToAction("ServisTakip", "Home");
                    }
                    else if (Session["UserID"].ToString() == null || Session["UserID"].ToString() == "2")
                    {

                        return View(model);

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
           
       }


    }

