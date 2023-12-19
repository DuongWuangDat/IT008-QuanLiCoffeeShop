﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QuanLiCoffeeShop.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class QuanLiCoffeShopEntities : DbContext
    {
        public QuanLiCoffeShopEntities()
            : base("name=QuanLiCoffeShopEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bill>().HasOptional(p => p.Customer).WithMany(c => c.Bill).HasForeignKey(p => p.IDCus);
            base.OnModelCreating(modelBuilder);
            //throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Bill> Bill { get; set; }
        public virtual DbSet<BillInfo> BillInfo { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Error> Error { get; set; }
        public virtual DbSet<GenreProduct> GenreProduct { get; set; }
        public virtual DbSet<GenreSeat> GenreSeat { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Seat> Seat { get; set; }
        public virtual DbSet<Staff> Staff { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
    }
}
