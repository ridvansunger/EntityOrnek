﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EntityOrnek
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class DbSinavOgrenciEntities : DbContext
    {
        public DbSinavOgrenciEntities()
            : base("name=DbSinavOgrenciEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<TBLDERSLER> TBLDERSLER { get; set; }
        public virtual DbSet<TBLNOTLAR> TBLNOTLAR { get; set; }
        public virtual DbSet<TBLOGRENCİ> TBLOGRENCİ { get; set; }
        public virtual DbSet<TBLKULUPLER> TBLKULUPLER { get; set; }
    
        public virtual ObjectResult<Sp_Not_Listesi_Result> Sp_Not_Listesi()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Sp_Not_Listesi_Result>("Sp_Not_Listesi");
        }
    }
}
