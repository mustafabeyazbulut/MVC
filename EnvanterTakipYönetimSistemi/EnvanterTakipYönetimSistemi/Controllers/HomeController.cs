using EnvanterTakipYönetimSistemi.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EnvanterTakipYönetimSistemi.ViewModels;

namespace EnvanterTakipYönetimSistemi.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        private Models.EnvanterServisEntities db = new Models.EnvanterServisEntities();


        [LoginControl]
        public ActionResult Index(HomeViewModel model)
        {

            model.DuyuruList = (from k in db.Tbl_Duyuru.Where(f => (f.D_Kayit == true)).OrderByDescending(f => f.D_ID)
                                select new DuyuruHomeViewModel
                                {
                                    DuyuruID=k.D_ID,
                                    DuyuruKonu=k.D_Konu,
                                    DuyuruIcerik=k.D_Icerik,
                                    DuyuruYayinlayan=k.Tbl_Personel.Per_Ad +" " + k.Tbl_Personel.Per_Soyad,
                                    DuyuruYayinlayanSube=k.Tbl_Personel.Tbl_P_Sube.Sube_Adi,
                                }).ToList();

            


            var envanter = (from k in db.Tbl_Envanter
                                   where k.Env_Kayit == true 
                                   select k).ToList();

            var zimmet = (from k in db.Tbl_Zimmet
                            where k.Zim_Kayit == true
                            select k).ToList();

            var talep = (from k in db.Tbl_Ariza
                          where k.Arz_Kayit == true && k.Arz_Durum == "Talep"
                         select k).ToList();

            int perid = Convert.ToInt32(Session["Per_ID"]);
            int envanterim = zimmet.Where(k => k.Kullanan_ID == perid).ToList().Count;
            int taleplerim = talep.Where(k => k.Per_ID == perid ).ToList().Count;

            List<DashboardHomeViewModel> dlist = new List<DashboardHomeViewModel>();
            dlist.Insert(0, new DashboardHomeViewModel { EnvanterToplamAdet = envanter.Count, TalepAdet= taleplerim, ServisAdet=0, Envanterim = envanterim, EnvanterToplamKullanilanAdet=zimmet.Count, TalepToplamAdet=talep.Count, ServisToplamAdet=0 });

            model.DashboardList = dlist;

            
            return View(model);
        }


    }
}