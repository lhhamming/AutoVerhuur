﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AutoVerhuurJansen.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DB_A42A9A_lhhammingEntities1 : DbContext
    {
        public DB_A42A9A_lhhammingEntities1()
            : base("name=DB_A42A9A_lhhammingEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<Categorie> Categorie { get; set; }
        public virtual DbSet<Klanten> Klanten { get; set; }
        public virtual DbSet<Medewerkers> Medewerkers { get; set; }
        public virtual DbSet<PrijsHistorie> PrijsHistorie { get; set; }
        public virtual DbSet<Verhuren> Verhuren { get; set; }
        public virtual DbSet<Voertuigen> Voertuigen { get; set; }
    }
}
