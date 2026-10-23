using Microsoft.EntityFrameworkCore;

namespace Tooded_EFCore
{
    public class AppDbContext : DbContext
    {
        public DbSet<Toode> Tooted { get; set; }

        public DbSet<Kategooria> Kategooriad { get; set; }

        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    @"Server=(localdb)\MSSQLLocalDB;Database=ToodedDB;Trusted_Connection=True;TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(
            ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Toode>()
                .Property(t => t.Hind)
                .HasColumnType("real");

            modelBuilder.Entity<Toode>()
                .HasOne(t => t.Kategooria)
                .WithMany(k => k.Tooted)
                .HasForeignKey(t => t.KategooriaId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}