using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using eKhaya.Authorization.Roles;
using eKhaya.Authorization.Users;
using eKhaya.MultiTenancy;
using eKhaya.Domain.MaintenanceRequests;
using eKhaya.Domain.Leases;
using eKhaya.Domain.Payment;
using eKhaya.Domain.Users;
using eKhaya.Domain.Workers;
using eKhaya.Domain.Units;
using eKhaya.Domain.Properties;
using eKhaya.Domain.Amenities;
using eKhaya.Domain.Images;
using eKhaya.Domain.Documents;
using eKhaya.Domain.Applications;
using eKhaya.Domain.Address;
using eKhaya.Domain.PropertyAmenities;
using eKhaya.Domain.AgentsProperty;
using eKhaya.Domain.UnitsAmenities;


namespace eKhaya.EntityFrameworkCore
{
    public class eKhayaDbContext : AbpZeroDbContext<Tenant, Role, User, eKhayaDbContext>
    {
        /* Define a DbSet for each entity of the application */



        public DbSet<MaintenanceRequest> MaintenanceRequests { get; set; }


        public DbSet<Lease> Leases { get; set; }

        public DbSet<Payment> Payments { get; set; }

        public DbSet<Application> Applications { get; set; }

        public DbSet<Worker> Workers { get; set; }

        public DbSet<Unit> Units { get; set; }

        public DbSet<Addresses> Addresses { get; set; }

        public DbSet<Property> Properties { get; set; }
        //person
        public DbSet<Applicant> Applicants { get; set; }

        public DbSet<Resident> Residents { get; set; }
        public DbSet<PropertyManager> PropertyManagers { get; set; }

        public DbSet<Agent> Agents { get; set; }

        public DbSet<PropertyAmenity> propertyAmenities { get; set; }

        public DbSet<AgentProperty> propertyAgents { get; set; }

        //storefiles
        public DbSet<Document> Documents { get; set; }

        public DbSet<Image> Images { get; set; }
        //amenities
        public DbSet<Amenity> Amenities { get; set; }

       

        public DbSet<UnitsAmenities> UnitsAmenities{ get; set; }
        
        



       




        


        


        
        public eKhayaDbContext(DbContextOptions<eKhayaDbContext> options)
            : base(options)
        {
        }
    }
}
