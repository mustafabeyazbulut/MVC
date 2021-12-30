using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OzdilekBtServisTakipPortalı.ViewModel
{
    public class ServisViewModel
    {
        public int ServisID1 { get; set; }
        public int ServisID2 { get; set; }
        public int EnvanterID1 { get; set; }
        public string Sube1 { get; set; }
        public string Lokasyon1 { get; set; }
        public string ServisCinsi1 { get; set; }
        public string ServisMarka1 { get; set; }
        public string ServisModel1 { get; set; }
        public string ServisSeriNo1 { get; set; }
        public string ServisOzellik1 { get; set; }
        public string GonderenKisi1 { get; set; }
        public string GonderildigiTarih1 { get; set; }
        public string EkAksesuar1 { get; set; }
        public string ArizaSebebi1 { get; set; }
        public string ServisDurumu1 { get; set; }
        public string ServisDurumu2 { get; set; }
        public string Firma1 { get; set; }
        //public bool secildimisil { get; set; }
        //public bool secildimigeldi { get; set; }
       
        
        //açılır dropbaxlar
        public int FirmaID { get; set; }
        public List<SelectListItem> FirmaList { get; set; }

        //public string arama { get; set; }
        public string aramasecim { get; set; }
        public string servisarama { get; set; }
        public List<ServisViewModel> servislist { get; set; }
    }
}