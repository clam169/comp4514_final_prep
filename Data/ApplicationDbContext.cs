using System;
using final_mock.Models;
using Microsoft.EntityFrameworkCore;

namespace final_mock.Data
{
  public partial class ApplicationDbContext : DbContext
  {
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Person> People { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      OnModelCreatingPartial(modelBuilder);

      // Seeds the DB
      modelBuilder.Entity<Person>().HasData(SeedData.GetPeople());
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      if (!optionsBuilder.IsConfigured)
      {
        var serverVersion = new MySqlServerVersion(new Version(8, 0, 0));
        optionsBuilder.UseMySql("server=localhost; userid=root; pwd=secret; port=8888; database=mock; SslMode=none;", serverVersion);
      }
    }
  }
}