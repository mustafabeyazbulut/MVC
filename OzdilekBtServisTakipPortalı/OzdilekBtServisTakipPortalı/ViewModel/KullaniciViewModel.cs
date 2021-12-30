using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OzdilekBtServisTakipPortalı.ViewModel
{
    public class KullaniciViewModel
    {
        //public string arama { get; set; }
        public string Servisarama { get; set; }
        public List<KullaniciViewModel> servislist { get; set; }
        //public string arama { get; set; }
        public string ServisGelenarama { get; set; }
        public List<KullaniciViewModel> servisgelenlist { get; set; }

        public int ServisID { get; set; }
        public int EnvanterID { get; set; }
        public string Sube { get; set; }
        public string Lokasyon { get; set; }
        public string ServisCinsi { get; set; }
        public string ServisMarka { get; set; }
        public string ServisModel { get; set; }
        public string ServisSeriNo { get; set; }
        public string ServisOzellik { get; set; }
        public string GonderenKisi { get; set; }
        public string GonderildigiTarih { get; set; }
        public string EkAksesuar { get; set; }
        public string ArizaSebebi { get; set; }
        public string ServisDurumu { get; set; }
        public string Firma { get; set; }


        public int ServisID4 { get; set; }
        public int EnvanterID4 { get; set; }
        public string Sube4 { get; set; }
        public string Lokasyon4 { get; set; }
        public string ServisgelenCinsi4 { get; set; }
        public string ServisgelenMarka4 { get; set; }
        public string ServisgelenModel4 { get; set; }
        public string ServisgelenSeriNo4 { get; set; }
        public string ServisgelenOzellik4 { get; set; }
        public string GonderenKisi4 { get; set; }
        public string GonderildigiTarih4 { get; set; }
        public string GeldiginiBelirtenKisi4 { get; set; }
        public string GeldigiTarih4 { get; set; }
        public string EkAksesuar4 { get; set; }
        public string ArizaSebebi4 { get; set; }
        public string YapilanMudahale4 { get; set; }
        public string Firma4 { get; set; }




        public string subekullanici { get; set; }


    }
}