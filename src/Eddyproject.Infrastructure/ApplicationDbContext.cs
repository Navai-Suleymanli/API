using Eddyproject.Common.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Eddyproject.Infrastructure;

public class ApplicationDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
{
    // dbset edirik. 
    public DbSet<Address> Adresses { get; set; }

    public DbSet<Budget> Budgets { get; set; }
     
    public DbSet<Course> Courses { get; set; }

    public DbSet<Student> Students { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }

    // fileni yaradib, databaseni configure edirik
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Filename = EddyDatabase.db");
        base.OnConfiguring(optionsBuilder);     
    }

    //modeli configure edirik
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Address>().HasKey(e => e.Id);
        builder.Entity<Budget>().HasKey(e => e.Id);
        builder.Entity<Course>().HasKey(e => e.Id);
        builder.Entity<Student>().HasKey(e => e.Id);

        builder.Entity<Student>().HasOne(e => e.Budget);
        builder.Entity<Student>().HasOne(e => e.Address).WithMany(e => e.Students); // bir adresi ve adresin coxlu sagirdleri

        builder.Entity<Course>().HasMany(e => e.Students).WithMany(e =>e.Courses); // coxlu sagirdleri, ve sagirdlerin ocxlu kurslari



        base.OnModelCreating(builder);  
    }

}
