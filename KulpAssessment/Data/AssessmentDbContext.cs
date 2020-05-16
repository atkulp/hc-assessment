using Microsoft.EntityFrameworkCore;
using KulpAssessment.Data.Entities;

namespace KulpAssessment.Data
{
    public class AssessmentDbContext : DbContext
    {
        public DbSet<Person> People { get; set; }

        public AssessmentDbContext()
        {
        }

        public AssessmentDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Obviously better web.config, appsettings.json, or Azure environment connection string
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Assessment;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Handy for creating database for demo.  Better to go with database-first and reduce
            // EF migration weirdness over time

            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.FirstName).IsRequired();
                entity.Property(e => e.LastName).IsRequired();
            });
        }
    }
}