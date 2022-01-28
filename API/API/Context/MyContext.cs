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
        public DbSet<AccountRole> AccountRoles { get; set; }
        public DbSet<Role> Roles { get; set; }
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

            modelBuilder.Entity<AccountRole>()
            .HasKey(a => new { a.Account_Nik, a.Role_Id });

            modelBuilder.Entity<AccountRole>()
            .HasOne(a => a.Account)
            .WithMany(b => b.AccountRoles)
            .HasForeignKey(b => b.Account_Nik);

            modelBuilder.Entity<AccountRole>()
            .HasOne(a => a.Role)
            .WithMany(b => b.AccountRoles)
            .HasForeignKey(a => a.Role_Id);

        }
    }
}
