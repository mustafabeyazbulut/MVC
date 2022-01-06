using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EnvanterTakipYönetimSistemi.ViewModels
{
    public class EnvanterViewModel
    {
        public int Envanter_ID { get; set; }

        public string EnvanterCinsAdi { get; set; }

        public string EnvanterMarkaAdi { get; set; }
        public string EnvanterModel { get; set; }
        public string EnvanterSeriNo { get; set; }
        public string EnvanterDurum { get; set; }
        public bool EnvanterKayit { get; set; }

        public DateTime EnvanterKayitTarih { get; set; }
        public int EnvanterKaydeden_ID { get; set; }
        public string EnvanterKaydeden { get; set; }
        public string EnvanterSube { get; set; }
        public List<EnvanterViewModel> TabloEnvanterList { get; set; }


        public int EnvanterCins_ID { get; set; }
        public List<SelectListItem> CinsiList { get; set; }

        public int EnvanterMarka_ID { get; set; }
        public List<SelectListItem> MarkaList { get; set; }


        public int EnvanterSube_ID { get; set; }
        public List<SelectListItem> SubeList { get; set; }

        public int Zimmetlenen_ID { get; set; }
        public List<SelectListItem> ZimmetlenenList { get; set; }

        public string[] EnvanterStrng_ID { get; set; }

    }

}