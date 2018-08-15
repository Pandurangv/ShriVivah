namespace ShriVivah.Models.Entities
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Data.Entity.ModelConfiguration;

    public class ShreeVivahDbContext : DbContext
    {
        // Your context has been configured to use a 'ShreeVivahDbContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'ShriVivah.Models.Entities.ShreeVivahDbContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'ShreeVivahDbContext' 
        // connection string in the application configuration file.
        public ShreeVivahDbContext()
            : base("name=ApplicationServices")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public ShreeVivahDbContext(string Connectionstring)
            : base("name=" + Connectionstring)
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<ProfileImage> tblProfileImages { get; set; }

        public DbSet<EventManagement> EventManagements { get; set; }


        public DbSet<tblRelatived> tblRelativeds { get; set; }

        public DbSet<tblVendorType> tblVendorTypes { get; set; }

        public DbSet<tblVendor> tblVendors { get; set; }

        public DbSet<tblFamilyDetails> tblFamilyDetailss { get; set; }

        public DbSet<tblUser> tblUsers { get; set; }

        public DbSet<tblMessages> tblMessages { get; set; }
        
        public DbSet<tblBloodGroup> tblBloodGroups { get; set; }

        public DbSet<tblReligion> tblReligions { get; set; }

        public DbSet<tblQualification> tblQualifications { get; set; }

        public DbSet<tblRequest> tblRequests { get; set; }

        public DbSet<tblVisitorDetails> tblVisitorDetailss { get; set; }

        public DbSet<PackageMaster> PackageMasters { get; set; }

        public DbSet<LoginDetails> LoginDetailss { get; set; }

        public DbSet<tblOTP> tblOTPs { get; set; }
        //public DbSet<aspnet_UsersInRole> aspnet_UsersInRoles { get; set; }

        //public DbSet<aspnet_Membership> aspnet_Memberships { get; set; }

        public DbSet<tblExpectation> tblExpectations { get; set; }

        public DbSet<tblCountry> tblCounties { get; set; }

        public DbSet<tblJobDetails> tblJobDetailss { get; set; }

        public DbSet<tblGan> tblGans { get; set; }

        public DbSet<tblGotra> tblGotras { get; set; }

        public DbSet<tblHeight> tblHeights { get; set; }

        public DbSet<tblOras> tblOrass { get; set; }

        public DbSet<tblState> tblStates { get; set; }

        public DbSet<tblCity> tblCities { get; set; }

        public DbSet<UserRequests> UserRequest { get; set; }

        public DbSet<tblCast> tblCasts { get; set; }

        public DbSet<tblContactDetails> tblContactDetailss { get; set; }

        public DbSet<UserRequests_Vendor> viewUserRequests_Vendor { get; set; }
        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}