using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ScholarSyncMVC.Models;

namespace ScholarSyncMVC.Data
{
    public class ScholarSyncConext:IdentityDbContext<AppUser>
    {
        public ScholarSyncConext(DbContextOptions<ScholarSyncConext> options):base(options) 
        {
             
        }

        public DbSet<University> Universities { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Applicationn> Applications { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Requirements> Requirements { get; set; }
        public DbSet<Scholarship> Scholarships { get; set; }
        public DbSet<Scholarship> UserAccount { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }

        public DbSet<Education> Educations { get; set; }
        public DbSet<EduLevel> EduLevels { get; set; } // Add EduLevel DbSet

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the one-to-many relationship between AppUser and Education
            modelBuilder.Entity<Education>()
                .HasOne(e => e.AppUser)
                .WithMany(u => u.Educations)
                .HasForeignKey(e => e.AppUserId);

            // Configure the relationship between Education and EduLevel
            modelBuilder.Entity<Education>()
                .HasOne(e => e.EduLevel)
                .WithMany()
                .HasForeignKey(e => e.EduLevelId);
        }

    }
}
