using Microsoft.EntityFrameworkCore;
using WycieczkiIO.Models;

namespace WycieczkiIO.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions options) : base(options)
        {
        }
        
        public DbSet<Adres> Adres { get; set; }
        
        public DbSet<Atrakcja> Atrakcja { get; set; }
        
        public DbSet<Kraj> Kraj { get; set; }
        
        public DbSet<Miasto> Miasto { get; set; }
        
        public DbSet<Platnosc> Platnosc { get; set; }
        
        public DbSet<Przewodnik> Przewodnik { get; set; }
        
        public DbSet<Transport> Transport { get; set; }
        
        public DbSet<Uczestnik> Uczestnik { get; set; }
        
        public DbSet<Wycieczka> Wycieczka { get; set; }
        
        public DbSet<WycieczkaAtrakcja> WycieczkaAtrakcja { get; set; }
        
        public DbSet<WycieczkaTransport> WycieczkaTransport { get; set; } 
        
        public DbSet<Zakwaterowanie> Zakwaterowanie { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WycieczkaAtrakcja>()
                        .HasKey(t => new {t.WycieczkaId, t.AtrakcjaId});

            modelBuilder.Entity<WycieczkaAtrakcja>()
                        .HasOne(cs => cs.Wycieczka)
                        .WithMany(p => p.WycieczkaAtrakcja)
                        .HasForeignKey(cs => cs.WycieczkaId);

            modelBuilder.Entity<WycieczkaAtrakcja>()
                        .HasOne(cs => cs.Atrakcja)
                        .WithMany(s => s.WycieczkaAtrakcja)
                        .HasForeignKey(cs => cs.AtrakcjaId);
            
            modelBuilder.Entity<WycieczkaTransport>()
                        .HasKey(t => new {t.WycieczkaId, t.TransportId});

            modelBuilder.Entity<WycieczkaTransport>()
                        .HasOne(cs => cs.Wycieczka)
                        .WithMany(p => p.WycieczkaTransport)
                        .HasForeignKey(cs => cs.WycieczkaId);

            modelBuilder.Entity<WycieczkaTransport>()
                        .HasOne(cs => cs.Transport)
                        .WithMany(s => s.WycieczkaTransport)
                        .HasForeignKey(cs => cs.TransportId);

            modelBuilder.Seed();
        }
    }
}