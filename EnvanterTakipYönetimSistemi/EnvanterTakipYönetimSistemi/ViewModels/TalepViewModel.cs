using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EnvanterTakipYönetimSistemi.ViewModels
{
    public class TalepViewModel
    {
        public int TalepID { get; set; }
        public string TalepBilgi { get; set; }
        public string TalepDurum { get; set; }
        public DateTime TalepTarih { get; set; }
        public int Per_ID { get; set; }
        public string PerAd { get; set; }
        public int Env_ID { get; set; }
        public bool TalepKayit { get; set; }
        public string T_EnvanterCinsAdi { get; set; }
        public string T_EnvanterMarkaAdi { get; set; }
        public string T_EnvanterModel { get; set; }
        public string T_EnvanterSeriNo { get; set; }
        public List<TalepViewModel> TalepList { get; set; }

        public string TalepBilgiYeni { get; set; }
        public string TalepBilgiGuncelle { get; set; }
        public int TalepIDGuncelle { get; set; }

        public int Talepid1 { get; set; }
        public int Firma_ID { get; set; }
        public bool islem { get; set; }
        public string servisAciklama { get; set; }
        public List<SelectListItem> FirmaList { get; set; }

        public int Urun_ID { get; set; }
        public List<SelectListItem> UrunList { get; set; }
    }
}