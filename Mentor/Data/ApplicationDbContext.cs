using Mentor.Models;
using Mentor.Models.Job;
using Mentor.Models.Learn;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Mentor.ViewModels;

namespace Mentor.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        public DbSet<JobCategory> JobCategories { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<ApplyForJob> ApplyForJobs { get; set; }
        public DbSet<ApplyRequest> ApplyRequests { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseSubscriptions> Subscriptions { get; set; }
        public DbSet<CourseComments> Comments { get; set; }
        public DbSet<EmployerProfile> EmployerProfiles { get; set; }
        public DbSet<CourseUnit> CourseUnits { get; set; }
        public DbSet<CourseLesson> CourseLessons { get; set; }
        public DbSet<Instructor>  Instructors{ get; set; }
        public DbSet<SavedJob> SavedJobs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>().ToTable("Users");
            builder.Entity<IdentityRole>().ToTable("Roles");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserToken");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UsersClaim");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UsersLogin");
        }

        public DbSet<Mentor.ViewModels.RolesViewModel> RolesViewModel { get; set; }
    }
}