using Backend.Data.Modeli;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Backend.Data {
    public class HCIDBContext : DbContext {
        public DbSet<KorisnickiNalog> KorisnickiNalozi { get; set; }
        public DbSet<Soba> Sobe { get; set; }
        public DbSet<Gost> Gosti { get; set; }
        public DbSet<Menadzer> Menadzeri { get; set; }
        public DbSet<Krevet> Kreveti { get; set; }
        public DbSet<Recenzija> Recenzije { get; set; }
        public DbSet<SobaKrevet> SobeKreveti { get; set; }
        public DbSet<ZauzetaSoba> ZauzeteSobe { get; set; }
        public DbSet<Aranzman> Aranzmani { get; set; }
        public DbSet<SobaAranzman> SobeAranzmani { get; set; }
        public DbSet<Cijena> Cijene { get; set; }
        



        public HCIDBContext(
            DbContextOptions options) : base(options) {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Soba>().HasMany(x => x.ZauzetaSoba).WithOne(x => x.Soba).HasForeignKey(x => x.SobaId);
            modelBuilder.Entity<Soba>().HasMany(x => x.Kreveti).WithOne(x => x.Soba).HasForeignKey(x => x.SobaId);
            modelBuilder.Entity<Soba>().HasMany(x => x.Aranzmani).WithOne(x => x.Soba).HasForeignKey(x => x.SobaId);
            modelBuilder.Entity<Soba>().HasMany(x => x.SobaCijene).WithOne(x => x.Soba).HasForeignKey(x => x.SobaId);

            modelBuilder.Entity<Soba>().Navigation(e => e.ZauzetaSoba).AutoInclude();
            modelBuilder.Entity<Soba>().Navigation(e => e.Kreveti).AutoInclude();
            modelBuilder.Entity<Soba>().Navigation(e => e.Aranzmani).AutoInclude();
            modelBuilder.Entity<Soba>().Navigation(e => e.SobaCijene).AutoInclude();

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) {
                relationship.DeleteBehavior = DeleteBehavior.Cascade;
            }
        }
    }
}
