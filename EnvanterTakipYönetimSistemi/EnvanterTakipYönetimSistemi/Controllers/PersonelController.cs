using EnvanterTakipYönetimSistemi.Login;
using EnvanterTakipYönetimSistemi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EnvanterTakipYönetimSistemi.ViewModels
{
    public class PersonelController : Controller
    {
        private Models.EnvanterServisEntities db = new Models.EnvanterServisEntities();
        // GET: Personel

        [LoginControl]
        public ActionResult Index( PersonelViewModel model)
        {
            model.PersonelList = (from x in db.Tbl_Personel.OrderByDescending(f => f.Per_ID)
                                       select new PersonelViewModel
                                       {
                                           PersonelID=x.Per_ID,
                                           PersonelAd=x.Per_Ad,
                                           PersonelSoyad=x.Per_Soyad,
                                           PersonelSube=x.Tbl_P_Sube.Sube_Adi,
                                           PersonelEposta=x.Per_Eposta,
                                          
                                           PersonelRol=x.Tbl_P_Rol.Rol_Value,
                                           PersonelSonGirisTarih= (DateTime)x.Per_SonGirisTarihi,
                                           PersonelKayit= (bool)x.Per_Kayit,
                                           PerSubeID = (int)x.Sube_ID
                                           
                                       }).ToList();


            List<Tbl_P_Sube> subelist = db.Tbl_P_Sube.Where(f => f.Sube_Kayit == true).OrderBy(f => f.Sube_ID).ToList();
            model.PersonelSubeList = (from x in subelist
                              select new SelectListItem
                              {
                                  Text = x.Sube_Adi,
                                  Value = x.Sube_ID.ToString()
                              }).ToList();
            model.PersonelSubeList.Insert(0, new SelectListItem { Value = "", Text = "Seçiniz", Selected = true });



            List<Tbl_P_Rol> perRolList = db.Tbl_P_Rol.Where(f => f.Rol_Kayit == true).OrderBy(f => f.Rol_Value).ToList();
            model.PersonelRolList = (from x in perRolList
                                      select new SelectListItem
                                      {
                                          Text = x.Rol_Aciklama,
                                          Value = x.Rol_Value
                                      }).ToList();
            model.PersonelRolList.Insert(0, new SelectListItem { Value = "", Text = "Seçiniz", Selected = true });

            if (Request.IsAjaxRequest()) // index sayfası ilk defa açılmadıysa sadece tabloyu günceller
            {
                return PartialView("_PersonelTablo", model);
            }
            else
            {
                return View(model);
            }
        }
    }
}