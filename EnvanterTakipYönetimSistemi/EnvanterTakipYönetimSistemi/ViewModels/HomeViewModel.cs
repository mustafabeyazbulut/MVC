using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EnvanterTakipYönetimSistemi.ViewModels
{
    public class HomeViewModel
    {
        public List<DuyuruViewModel> DuyuruList { get; set; }
        public List<DashboardViewModel> DashboardList { get; set; }
    }
    public class DuyuruViewModel
    {
        public int DuyuruID { get; set; }
        public string DuyuruKonu { get; set; }
        public string DuyuruIcerik { get; set; }
        public string DuyuruYayinlayan { get; set; }
        public string DuyuruYayinlayanSube { get; set; }

    }
    public class DashboardViewModel
    {
        public int EnvanterAdet { get; set; }
        public int TalepAdet { get; set; }
        public int ServisAdet { get; set; }

        public int EnvanterToplamAdet { get; set; }
        public int TalepToplamAdet { get; set; }
        public int ServisToplamAdet { get; set; }
    }

}