using Microsoft.EntityFrameworkCore;
using src.Models;

namespace src.Persistence;
public class DatabaseContext : DbContext
{
  public DatabaseContext
  (
    DbContextOptions<DatabaseContext> options
  ) : base(options)
  {
    
  }
  public DbSet<Person> People { get; set; }
  public DbSet<Contract> Contracts { get; set; }

  protected override void OnModelCreating(ModelBuilder builder){
    builder.Entity<Person>(tabela => {
      tabela.HasKey(e => e.Id);
      tabela
        .HasMany(e => e.contracts)
        .WithOne()
        .HasForeignKey(c => c.PersonId);
    });

    builder.Entity<Contract>(tabela => {
      tabela.HasKey(e => e.Id);
    });
  }
}