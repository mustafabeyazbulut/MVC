using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EnvanterTakipYönetimSistemi.ViewModels
{
    public class PersonelViewModel
    {
        public int PersonelID { get; set; }
        public string PersonelAd { get; set; }
        public string PersonelSoyad { get; set; }
        public int PerSubeID { get; set; }
        public string PersonelSube { get; set; }

        [Required(ErrorMessage = " ")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email address")]
        [MaxLength(50)]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter correct email")]
        public string PersonelEposta { get; set; }
        public string Personel_Sifre { get; set; }
        public string PersonelRol { get; set; }
        public DateTime PersonelSonGirisTarih { get; set; }

        public bool PersonelKayit { get; set; }
        public List<PersonelViewModel> PersonelList { get; set; }


        
        public List<SelectListItem> PersonelSubeList { get; set; }

        
        public List<SelectListItem> PersonelRolList { get; set; }

    }
}