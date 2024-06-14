
using eApoteka.Models;
using eApoteka.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace eApoteka.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Apotekar> Apotekari { get; set; }
        public DbSet<DetaljDostave> DetaljiDostave { get; set; }
        public DbSet<DetaljPlacanja> DetaljiPlacanja { get; set; }
        public DbSet<Dostavljac> Dostavljaci { get; set; }
        public DbSet<Korisnik> Korisnici { get; set; }
        public DbSet<Narudzba> Narudzbe { get; set; }
        public DbSet<Proizvod> Proizvodi { get; set; }
        public DbSet<StatusNarudzbe> StatusiNarudzbi { get; set; }
        public DbSet<StavkaNarudzbe> StavkeNarudzbe { get; set; }
        public DbSet<Upit> Upiti { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>().ToTable("Admin");
            modelBuilder.Entity<Apotekar>().ToTable("Apotekar");
            modelBuilder.Entity<DetaljDostave>().ToTable("DetaljDostave");
            modelBuilder.Entity<DetaljPlacanja>().ToTable("DetaljPlacanja");
            modelBuilder.Entity<Dostavljac>().ToTable("Dostavljac");
            modelBuilder.Entity<Korisnik>().ToTable("Korisnik");
            modelBuilder.Entity<Narudzba>().ToTable("Narudzba");
            modelBuilder.Entity<Proizvod>().ToTable("Proizvod");
            modelBuilder.Entity<StatusNarudzbe>().ToTable("StatusNarudzbe");
            modelBuilder.Entity<StavkaNarudzbe>().ToTable("StavkaNarudzbe");
            modelBuilder.Entity<Upit>().ToTable("Upit");

            modelBuilder.Entity<Proizvod>()
                .HasOne(p => p.Admin)
                .WithMany()
                .HasForeignKey(p => p.AdminId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Proizvod>()
                .HasOne(p => p.Apotekar)
                .WithMany()
                .HasForeignKey(p => p.ApotekarId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DetaljDostave>()
                .HasOne(dd => dd.Dostavljac)
                .WithMany()
                .HasForeignKey(dd => dd.DostavljacId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Upit>()
                .HasOne(u => u.Korisnik)
                .WithMany()
                .HasForeignKey(u => u.KorisnikId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Upit>()
                .HasOne(u => u.Apotekar)
                .WithMany()
                .HasForeignKey(u => u.ApotekarId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Narudzba>()
                .HasOne(n => n.Korisnik)
                .WithMany()
                .HasForeignKey(n => n.KorisnikId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Narudzba>()
                .HasOne(n => n.Apotekar)
                .WithMany()
                .HasForeignKey(n => n.ApotekarId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Narudzba>()
                .HasOne(n => n.DetaljPlacanja)
                .WithMany()
                .HasForeignKey(n => n.DetaljPlacanjaId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Narudzba>()
                .HasOne(n => n.DetaljDostave)
                .WithMany()
                .HasForeignKey(n => n.DetaljDostaveId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<eApoteka.Models.ViewModels.RegisterViewModel> RegisterViewModel { get; set; } = default!;
        public DbSet<eApoteka.Models.ViewModels.LoginViewModel> LoginViewModel { get; set; } = default!;
        public DbSet<eApoteka.Models.ViewModels.SearchViewModel> SearchViewModel { get; set; } = default!;
        public DbSet<eApoteka.Models.ViewModels.AdminPanelViewModel> AdminPanelViewModel { get; set; } = default!;
        public DbSet<eApoteka.Models.ViewModels.PlaceOrderViewModel> PlaceOrderViewModel { get; set; } = default!;
    }
}

