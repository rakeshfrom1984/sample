﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Motormechs.Data.Entity
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;
    using System.Linq;
    
    public partial class MotorMechsEntities : DbContext
    {
        public MotorMechsEntities()
            : base("name=MotorMechsEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<role> roles { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<SpaPackage> SpaPackages { get; set; }
        public DbSet<SpaService> SpaServices { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserServicesDetail> UserServicesDetails { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<UserService> UserServices { get; set; }
    
        public virtual ObjectResult<GetForgetPasswordStatus_Result> GetForgetPasswordStatus(Nullable<System.Guid> fpc)
        {
            var fpcParameter = fpc.HasValue ?
                new ObjectParameter("fpc", fpc) :
                new ObjectParameter("fpc", typeof(System.Guid));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetForgetPasswordStatus_Result>("GetForgetPasswordStatus", fpcParameter);
        }
    }
}
