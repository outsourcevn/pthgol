﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace tbcng.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class thietbicnEntities : DbContext
    {
        public thietbicnEntities()
            : base("name=thietbicnEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<cat> cats { get; set; }
        public virtual DbSet<product_img> product_img { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<UserClaim> UserClaims { get; set; }
        public virtual DbSet<UserLogin> UserLogins { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<product> products { get; set; }
        public virtual DbSet<customer> customers { get; set; }
        public virtual DbSet<product_customer_order> product_customer_order { get; set; }
        public virtual DbSet<product_order> product_order { get; set; }
    }
}
