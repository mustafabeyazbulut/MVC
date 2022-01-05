using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EnvanterTakipYönetimSistemi.ViewModels
{
    public class DuyuruViewModel
    {

        public int DuyuruID { get; set; }
        public string DuyuruKonu { get; set; }
        public string DuyuruIcerik { get; set; }
        public string DuyuruYayinlayan { get; set; }
        public string DuyuruYayinlayanSube { get; set; }
        public bool DuyuruKayit { get; set; }
        public List<DuyuruViewModel> DuyuruList { get; set; }

        public int DuyuruYayinlayanID { get; set; }
        public int DuyuruYayinlayanSubeID { get; set; }

    }
}