using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {
        }
        public DbSet<Employee> Employees {get; set;}
        public DbSet<Account> Accounts {get; set;}
        public DbSet<Profiling> Profilings { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<University> Universitys { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
            .HasOne(a => a.Account)
            .WithOne(b => b.Employee)
            .HasForeignKey<Account>(b => b.Nik);

            modelBuilder.Entity<Account>()
            .HasOne(a => a.Profiling)
            .WithOne(b => b.Account)
            .HasForeignKey<Profiling>(b => b.Nik);

            modelBuilder.Entity<Education>()
            .HasMany(a => a.Profilings)
            .WithOne(b => b.Education);

            modelBuilder.Entity<University>()
            .HasMany(a => a.Educations)
            .WithOne(b => b.University);
        }
    }
}
