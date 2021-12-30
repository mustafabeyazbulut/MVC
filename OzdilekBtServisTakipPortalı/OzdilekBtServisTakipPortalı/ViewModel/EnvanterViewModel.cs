using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OzdilekBtServisTakipPortalı.ViewModel
{
    public class EnvanterViewModel
    {
        
        //envanterList ve envanter crate
        public int EnvanterID2 { get; set; }
        public string Sube2 { get; set; }
        public string Lokasyon2 { get; set; }
        public string EnvanterCinsi2 { get; set; }
        public string EnvanterMarka2 { get; set; }
        public string EnvanterModel2 { get; set; }
        public string EnvanterSeriNo2 { get; set; }
        public string EnvanterOzellik2 { get; set; }
        //

        //açılır dropbaxlar
        public int SubeID { get; set; }
        public List<SelectListItem> SubeList { get; set; }

        public int LokasyonID { get; set; }
        public List<SelectListItem> LokasyonList { get; set; }

        public int EnvanterCinsiID { get; set; }
        public List<SelectListItem> EnvanterCinsiList { get; set; }

        public int MarkaID { get; set; }
        public List<SelectListItem> MarkaList { get; set; }

        public int FirmaID { get; set; }
        public List<SelectListItem> FirmaList { get; set; }

        //




        public int ServisID3 { get; set; }
        public int EnvanterID3 { get; set; }
        public string Sube3 { get; set; }
        public string Lokasyon3 { get; set; }
        public string EnvanterCinsi3 { get; set; }
        public string EnvanterMarka3 { get; set; }
        public string EnvanterModel3 { get; set; }
        public string EnvanterSeriNo3 { get; set; }
        public string EnvanterOzellik3 { get; set; }
        public string GonderenKisi3 { get; set; }
        public string GonderildigiTarih3 { get; set; }     
        public string EkAksesuar3 { get; set; }
        public string ArizaSebebi3 { get; set; }
        public string ServisDurumu3 { get; set; }
        //public bool secildimisil1 { get; set; }
        //public bool secildimigeldi1 { get; set; }

        //public string arama { get; set; }
        public string aramasecim { get; set; }
        public string envanterarama { get; set; }
        public List<EnvanterViewModel> envanterlist { get; set; }
        //

    }
}