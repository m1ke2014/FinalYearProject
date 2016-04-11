using FinalYearProject.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

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
        public DbSet<Person> People { get; set; }
        public DbSet<Staff> StaffMembers { get; set; }
        public DbSet<ToDoList> ToDoLists { get; set; }
        public DbSet<JobRole> JobRoles { get; set; }
        public DbSet<Privleges> Privleges { get; set; }
        public DbSet<Chemical> Chemicals { get; set; }
        public DbSet<Priority> Priorities { get; set; }
        public DbSet<Comment> Comments { get; set; }

        // Remove pluralisation of table names
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

        }

        public System.Data.Entity.DbSet<FinalYearProject.Models.Customer> Customers { get; set; }

    }
}