//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EnvanterTakipYönetimSistemi.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Tbl_Ariza
    {
        public int Arz_ID { get; set; }
        public string Arz_Bilgi { get; set; }
        public string Arz_Durum { get; set; }
        public Nullable<System.DateTime> Arz_Tarih { get; set; }
        public Nullable<int> Per_ID { get; set; }
        public Nullable<int> Env_ID { get; set; }
        public Nullable<bool> Arz_Kayit { get; set; }
    
        public virtual Tbl_Envanter Tbl_Envanter { get; set; }
        public virtual Tbl_Personel Tbl_Personel { get; set; }
    }
}
