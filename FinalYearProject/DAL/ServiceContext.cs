using FinalYearProject.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace FinalYearProject.DAL
{
    public class ServiceContext : DbContext
    {
        public ServiceContext() : base("ServiceContext")
        { }

        public DbSet<DeclarationOfConformity> DOCs { get; set; }
        public DbSet<RMA> RMAs { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<WorkAllocation> WorkAllocations { get; set; }
        public DbSet<ServiceCall> ServiceCalls { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Fault> Faults { get; set; }
        public DbSet<Staff> StaffMembers { get; set; }
        public DbSet<ToDoList> ToDoLists { get; set; }
        public DbSet<JobRole> JobRoles { get; set; }
        public DbSet<Privleges> Privleges { get; set; }
        public DbSet<Chemical> Chemicals { get; set; }

        // Remove pluralisation of table names
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DeclarationOfConformity>()
                .HasOptional(f => f.RMA)
                .WithRequired(s => s.DOCs);

            modelBuilder.Entity<RMA>()
                .HasOptional(c => c.ApplicationUsers)
                .WithRequired()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

        }

        //public System.Data.Entity.DbSet<FinalYearProject.Models.Chemical> Chemicals { get; set; }
    }
}