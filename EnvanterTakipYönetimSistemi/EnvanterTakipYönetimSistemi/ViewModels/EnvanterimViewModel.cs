using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EnvanterTakipYönetimSistemi.ViewModels
{
    public class EnvanterimViewModel
    {

        public int Zimmet_ID { get; set; }
        public int Z_EnvanterID { get; set; }
        public string Z_EnvanterCinsAdi { get; set; }
        public string Z_EnvanterMarkaAdi { get; set; }
        public string Z_EnvanterModel { get; set; }
        public string Z_EnvanterSeriNo { get; set; }
        public DateTime ZimmetTarih { get; set; }
        public DateTime ZimmetIadeTarih { get; set; }
        public string ZimmetAciklama { get; set; }
        public bool ZimmetKayit { get; set; }

        public List<EnvanterimViewModel> EnvanterimList { get; set; }

    }
}