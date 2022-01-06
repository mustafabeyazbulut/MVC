using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EnvanterTakipYönetimSistemi.ViewModels
{
    public class ZimmetViewModel
    {
        public int Zimmet_ID { get; set; }
        public int Kullanan_ID { get; set; }
        public string Kullanan_AdSoyad { get; set; }
        public int Z_EnvanterID { get; set; }
        public string Z_EnvanterCinsAdi { get; set; }
        public string Z_EnvanterMarkaAdi { get; set; }
        public string Z_EnvanterModel { get; set; }
        public string Z_EnvanterSeriNo { get; set; }

        public int Zimmetleyen_ID { get; set; }
        public string Zimmetleyen_AdSoyad { get; set; }

        public DateTime ZimmetTarih { get; set; }
        public DateTime ZimmetIadeTarih { get; set; }
        public string ZimmetAciklama { get; set; }
        public bool ZimmetKayit { get; set; }
        
        public List<ZimmetViewModel> ZimmetList { get; set; }

        public int Kullanacak_ID { get; set; }
        public List<SelectListItem> KullanacakList { get; set; }

        public int Urun_ID { get; set; }
        public List<SelectListItem> UrunList { get; set; }

    }
}