using LR4.Core.Model;
using Microsoft.EntityFrameworkCore;

namespace LR4.Persistence.Data
{
    public class DbDataContext : DbContext
    {
        public DbDataContext(DbContextOptions<DbDataContext> options) : base(options) { }

        public DbSet<Book> books { get; set; }
        public DbSet<DentistryPatient> DentistryPatients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .Property(b => b.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<DentistryPatient>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();

            // Унікальність: лікар + час прийому
            modelBuilder.Entity<DentistryPatient>()
                .HasIndex(p => new { p.DoctorCode, p.TimeVisit })
                .IsUnique();
        }

    }
}
