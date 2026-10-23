using Microsoft.EntityFrameworkCore;

namespace Tooded_EFCore
{
    public class ToodedContext : DbContext
    {
        public DbSet<Toode> Toodetabel { get; set; }
        public DbSet<Kategooria> Kategooriatabel { get; set; }

        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=(localdb)\MSSQLLocalDB;Database=Tooded_DB;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Kategooria>()
                .ToTable("Kategooriatabel");

            modelBuilder.Entity<Toode>()
                .ToTable("Toodetabel");

            modelBuilder.Entity<Kategooria>().HasData(
                new Kategooria
                {
                    Id = 1,
                    KategooriaNimetus = "Joogid"
                },
                new Kategooria
                {
                    Id = 2,
                    KategooriaNimetus = "Puu- ja köögiviljad"
                },
                new Kategooria
                {
                    Id = 3,
                    KategooriaNimetus = "Maiustused"
                }
            );
        }
    }
}