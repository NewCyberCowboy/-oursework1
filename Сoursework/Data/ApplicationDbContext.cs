using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

using Coursework.Models;
using Coursework.Services;

namespace Coursework.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        public DbSet<Patient> Patients { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Medicine>().ToTable("Medicine");
            modelBuilder.Entity<Prescription>().ToTable("Prescription");
            modelBuilder.Entity<Staff>().ToTable("Staff");
            modelBuilder.Entity<Patient>().ToTable("Patient");
            modelBuilder.Entity<Account>().ToTable("account");
        }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Account> Accounts { get; set; }
    }

}