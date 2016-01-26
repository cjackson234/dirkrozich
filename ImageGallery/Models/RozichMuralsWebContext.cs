using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace RozichMurals.Web.Models
{
    public class RozichMuralsWebContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, add the following
        // code to the Application_Start method in your Global.asax file.
        // Note: this will destroy and re-create your database with every model change.
        // 
        // System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseIfModelChanges<RozichMurals.Web.Models.RozichMuralsWebContext>());

        public DbSet<RozichMurals.Web.Models.Image> Images { get; set; }

        public DbSet<RozichMurals.Web.Models.Album> Albums { get; set; }

        public DbSet<RozichMurals.Web.Models.Category> Categories { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Maps to the expected many-to-many join table name for roles to users.
            modelBuilder.Entity<User>()
            .HasMany(u => u.Roles)
            .WithMany(r => r.Users)
            .Map(m =>
            {
                m.ToTable("RoleMemberships");
                m.MapLeftKey("UserName");
                m.MapRightKey("RoleName");
            });
        }
    }

   public class DBInitializer : DropCreateDatabaseIfModelChanges<RozichMuralsWebContext>
   {
       protected override void Seed(RozichMuralsWebContext context)
       {
           // Create some roles.
           var roles = new List<Role>{
                new Role{RoleName = "Administrator"},
                new Role{RoleName = "User"},
                new Role{RoleName = "PowerUser"}
            };
           roles.ForEach(r => context.Roles.Add(r));

           // Create some users.
           MembershipCreateStatus status = new MembershipCreateStatus();
           Membership.CreateUser("admin", "password", "admin@user.com");
           if (status == MembershipCreateStatus.Success)
           {
               // Add the role.
               User admin = context.Users.Find("admin");
               Role adminRole = context.Roles.Find("Administrator");
               admin.Roles = new List<Role>();
               admin.Roles.Add(adminRole);
           }
           Membership.CreateUser("user", "password", "user@user.com");
           if (status == MembershipCreateStatus.Success)
           {
               // Add the role.
               User user = context.Users.Find("user");
               Role userRole = context.Roles.Find("User");
               user.Roles = new List<Role>();
               user.Roles.Add(userRole);
           }

       }
   }
}