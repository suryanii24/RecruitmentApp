using Microsoft.EntityFrameworkCore;
using RecruitmentApp.Models;

namespace RecruitmentApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Biodata> Biodatas { get; set; }

        public DbSet<Education> Educations { get; set; }

        public DbSet<Training> Trainings { get; set; }

        public DbSet<WorkExperience> WorkExperiences { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Biodata)
                .WithOne(b => b.User)
                .HasForeignKey<Biodata>(b => b.UserId);

            modelBuilder.Entity<Biodata>()
                .HasMany(b => b.Educations)
                .WithOne(e => e.Biodata)
                .HasForeignKey(e => e.BiodataId);

            modelBuilder.Entity<Biodata>()
                .HasMany(b => b.Trainings)
                .WithOne(t => t.Biodata)
                .HasForeignKey(t => t.BiodataId);

            modelBuilder.Entity<Biodata>()
                .HasMany(b => b.WorkExperiences)
                .WithOne(w => w.Biodata)
                .HasForeignKey(w => w.BiodataId);
        }
    }
}