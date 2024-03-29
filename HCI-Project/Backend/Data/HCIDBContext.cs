﻿using Backend.Data.Modeli;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data;

public class HCIDBContext : DbContext
{
    public HCIDBContext(
        DbContextOptions options) : base(options)
    {
    }

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
    public DbSet<Tokeni> Tokeni { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Soba>().HasMany(x => x.ZauzetaSoba).WithOne(x => x.Soba).HasForeignKey(x => x.SobaId);
        modelBuilder.Entity<Soba>().HasMany(x => x.Kreveti).WithOne(x => x.Soba).HasForeignKey(x => x.SobaId);
        modelBuilder.Entity<Soba>().HasMany(x => x.Aranzmani).WithOne(x => x.Soba).HasForeignKey(x => x.SobaId);
        modelBuilder.Entity<Soba>().HasMany(x => x.Cijene).WithOne(x => x.Soba).HasForeignKey(x => x.SobaId);

        modelBuilder.Entity<Soba>().Navigation(e => e.ZauzetaSoba).AutoInclude();
        modelBuilder.Entity<Soba>().Navigation(e => e.Kreveti).AutoInclude();
        modelBuilder.Entity<Soba>().Navigation(e => e.Aranzmani).AutoInclude();
        modelBuilder.Entity<Soba>().Navigation(e => e.Cijene).AutoInclude();
        modelBuilder.Entity<SobaAranzman>().Navigation(sa => sa.Aranzman).AutoInclude();
        modelBuilder.Entity<SobaKrevet>().Navigation(sk => sk.Krevet).AutoInclude();
        modelBuilder.Entity<ZauzetaSoba>().Navigation(sk => sk.Gost).AutoInclude();
        modelBuilder.Entity<ZauzetaSoba>().Navigation(sk => sk.Soba).AutoInclude();
        modelBuilder.Entity<ZauzetaSoba>().Navigation(sk => sk.SobaAranzman).AutoInclude();

        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            relationship.DeleteBehavior = DeleteBehavior.Cascade;
    }
}