//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EnvanterTakipY├ÂnetimSistemi.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Tbl_Servis
    {
        public int Serv_ID { get; set; }
        public Nullable<int> Serv_FirmaID { get; set; }
        public string Serv_Bilgi { get; set; }
        public Nullable<System.DateTime> Serv_GondTarih { get; set; }
        public Nullable<System.DateTime> Serv_GelmeTarih { get; set; }
        public Nullable<int> Arz_ID { get; set; }
        public Nullable<int> Per_ID { get; set; }
        public Nullable<bool> Serv_Kayit { get; set; }
    
        public virtual Tbl_Ariza Tbl_Ariza { get; set; }
        public virtual Tbl_P_Firma Tbl_P_Firma { get; set; }
        public virtual Tbl_Personel Tbl_Personel { get; set; }
    }
}
