using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EnvanterTakipYönetimSistemi.ViewModels
{
    public class ServisViewModel
    {
        public int servisID { get; set; }
        public int servisFirmaID { get; set; }
        public string ServisFirmaAdi { get; set; }
        public DateTime ServisGonTarih { get; set; }
        public DateTime ServisGelmeTarih { get; set; }
        public int TalepID { get; set; }
        public string TalepAciklama { get; set; }
        public string T_EnvanterCinsAdi { get; set; }
        public string T_EnvanterMarkaAdi { get; set; }
        public string T_EnvanterModel { get; set; }
        public string T_EnvanterSeriNo { get; set; }

        public int perID { get; set; }
        public string perAd { get; set; }
        public bool servisKayit { get; set; }

        public List<ServisViewModel> ServisList { get; set; }
    }
}