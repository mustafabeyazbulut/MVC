using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OzdilekBtServisTakipPortalı.ViewModel
{
    public class ServisGelenViewModel
    {
        public int ServisID4 { get; set; }
        public int EnvanterID4 { get; set; }
        public string Sube4 { get; set; }
        public string Lokasyon4 { get; set; }
        public string ServisgelenCinsi4 { get; set; }
        public string ServisgelenMarka4 { get; set; }
        public string ServisgelenModel4 { get; set; }
        public string ServisgelenSeriNo4 { get; set; }
        public string ServisgelenOzellik4 { get; set; }
        public string GonderenKisi4{ get; set; }
        public string GonderildigiTarih4 { get; set; }
        public string GeldiginiBelirtenKisi4 { get; set; }
        public string GeldigiTarih4 { get; set; }
        public string EkAksesuar4 { get; set; }
        public string ArizaSebebi4 { get; set; }
        public string YapilanMudahale4 { get; set; }
        public string Firma4 { get; set; }

        //arama işlemleri için

        //public string arama { get; set; }
        public string aramasecim { get; set; }
        public string servisarama { get; set; }
        public List<ServisGelenViewModel> servisgelenlist { get; set; }
    }
}