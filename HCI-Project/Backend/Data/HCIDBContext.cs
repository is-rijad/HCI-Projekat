using Backend.Data.Modeli;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Backend.Data {
    public class HCIDBContext : DbContext {
        public DbSet<KorisnickiNalog> KorisnickiNalozi { get; set; }
        public DbSet<Soba> Sobe { get; set; }
        public DbSet<Gost> Gosti { get; set; }
        public DbSet<Menadzer> Menadzeri { get; set; }


        public HCIDBContext(
            DbContextOptions options) : base(options) {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) {
                relationship.DeleteBehavior = DeleteBehavior.Cascade;
            }
        }
    }
}
