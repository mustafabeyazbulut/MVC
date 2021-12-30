using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OzdilekBtServisTakipPortalı.ViewModel
{
    public class AdminViewModel
    {
        

        public int SubeID { get; set; }
        public string SubeAdi { get; set; }

        public int LokasyonID { get; set; }
        public string LokasyonAdi { get; set; }


        public int KullaniciID { get; set; }
        public string KullaniciAdi { get; set; }
        public string KullaniciSifre { get; set; }
        public string KullaniciSonGirisTarihi { get; set; }
        public int KullaniciYetki { get; set; }

        public string arama { get; set; }
        public List<AdminViewModel> Subelist { get; set; }
        public List<AdminViewModel> Lokasyonlist { get; set; }
        public List<AdminViewModel> Kullanicilist { get; set; }
    }
}