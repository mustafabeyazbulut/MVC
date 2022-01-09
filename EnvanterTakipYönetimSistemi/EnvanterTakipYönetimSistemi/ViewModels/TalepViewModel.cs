using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EnvanterTakipYönetimSistemi.ViewModels
{
    public class TalepViewModel
    {
        public int TalepID { get; set; }
        public string TalepBilgi { get; set; }
        public string TalepDurum { get; set; }
        public DateTime TalepTarih { get; set; }
        public int Per_ID { get; set; }
        public int Env_ID { get; set; }
        public bool TalepKayit { get; set; }
        public List<TalepViewModel> TalepList { get; set; }
    }
}