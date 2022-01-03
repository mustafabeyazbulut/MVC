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


            List<DashboardHomeViewModel> dlist = new List<DashboardHomeViewModel>();
            dlist.Insert(0, new DashboardHomeViewModel { EnvanterToplamAdet = envanter.Count });

            model.DashboardList = dlist;

            
            return View(model);
        }


    }
}