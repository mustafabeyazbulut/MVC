﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DouaCosmeticShopping.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DouaCosmeticShoppingEntities : DbContext
    {
        public DouaCosmeticShoppingEntities()
            : base("name=DouaCosmeticShoppingEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Table_Customer> Table_Customer { get; set; }
        public virtual DbSet<Table_Order> Table_Order { get; set; }
        public virtual DbSet<Table_OrderProduct> Table_OrderProduct { get; set; }
        public virtual DbSet<Table_Product> Table_Product { get; set; }
    }
}
