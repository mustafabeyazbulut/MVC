using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OzdilekBtServisTakipPortalı.ViewModel
{
    public class Admin1ViewModel
    {
        public int FirmaID { get; set; }
        public string FirmaAdi { get; set; }

        public int MarkaID { get; set; }
        public string MarkaAdi { get; set; }

        public string arama { get; set; }
        public string arama1 { get; set; }
        public List<Admin1ViewModel> Firmalist { get; set; }
        public List<Admin1ViewModel> Markalist { get; set; }
    }
}