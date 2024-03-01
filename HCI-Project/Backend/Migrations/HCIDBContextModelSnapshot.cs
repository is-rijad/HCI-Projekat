﻿// <auto-generated />
using System;
using Backend.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Backend.Migrations
{
    [DbContext(typeof(HCIDBContext))]
    partial class HCIDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Backend.Data.Modeli.Aranzman", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("NazivAranzmana")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Aranzmani");
                });

            modelBuilder.Entity("Backend.Data.Modeli.Cijena", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("Id"));

                    b.Property<int>("BrojOsoba")
                        .HasColumnType("int");

                    b.Property<float>("CijenaSobe")
                        .HasColumnType("real");

                    b.Property<int>("SobaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SobaId");

                    b.ToTable("Cijene");
                });

            modelBuilder.Entity("Backend.Data.Modeli.KorisnickiNalog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DatumRodjenja")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("bit");

                    b.Property<string>("Lozinka")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Prezime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SlikaKorisnika")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("KorisnickiNalozi");

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("Backend.Data.Modeli.Krevet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Tip")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ZaOsoba")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Kreveti");
                });

            modelBuilder.Entity("Backend.Data.Modeli.Recenzija", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Datum")
                        .HasColumnType("datetime2");

                    b.Property<int>("GostId")
                        .HasColumnType("int");

                    b.Property<string>("Komentar")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Ocjena")
                        .HasColumnType("int");

                    b.Property<int>("SobaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GostId");

                    b.HasIndex("SobaId");

                    b.ToTable("Recenzije");
                });

            modelBuilder.Entity("Backend.Data.Modeli.Soba", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Balkon")
                        .HasColumnType("bit");

                    b.Property<bool>("Bazen")
                        .HasColumnType("bit");

                    b.Property<bool>("BesplatnoOtkazivanje")
                        .HasColumnType("bit");

                    b.Property<int>("BrojGostiju")
                        .HasColumnType("int");

                    b.Property<float>("CijenaZaDjecu")
                        .HasColumnType("real");

                    b.Property<float>("DjecaDo")
                        .HasColumnType("real");

                    b.Property<bool>("DozvoljeniLjubimci")
                        .HasColumnType("bit");

                    b.Property<bool>("Klima")
                        .HasColumnType("bit");

                    b.Property<bool>("Minibar")
                        .HasColumnType("bit");

                    b.Property<string>("NazivSobe")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Opis")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PrilagodjenInvalidima")
                        .HasColumnType("bit");

                    b.Property<bool>("Spa")
                        .HasColumnType("bit");

                    b.Property<bool>("Teretana")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Sobe");
                });

            modelBuilder.Entity("Backend.Data.Modeli.SobaAranzman", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("Id"));

                    b.Property<int>("AranzmanId")
                        .HasColumnType("int");

                    b.Property<float>("Doplata")
                        .HasColumnType("real");

                    b.Property<int>("SobaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AranzmanId");

                    b.HasIndex("SobaId");

                    b.ToTable("SobeAranzmani");
                });

            modelBuilder.Entity("Backend.Data.Modeli.SobaKrevet", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("Id"));

                    b.Property<int>("BrojKreveta")
                        .HasColumnType("int");

                    b.Property<int>("KrevetId")
                        .HasColumnType("int");

                    b.Property<int>("SobaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("KrevetId");

                    b.HasIndex("SobaId");

                    b.ToTable("SobeKreveti");
                });

            modelBuilder.Entity("Backend.Data.Modeli.ZauzetaSoba", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("BrojOsoba")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Cijena")
                        .HasColumnType("real");

                    b.Property<DateTime>("DatumDolaska")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DatumOdlaska")
                        .HasColumnType("datetime2");

                    b.Property<int>("SobaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SobaId");

                    b.ToTable("ZauzeteSobe");
                });

            modelBuilder.Entity("Backend.Data.Modeli.Gost", b =>
                {
                    b.HasBaseType("Backend.Data.Modeli.KorisnickiNalog");

                    b.Property<int>("BrojRezervacija")
                        .HasColumnType("int");

                    b.Property<string>("Drzava")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Grad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("Gosti");
                });

            modelBuilder.Entity("Backend.Data.Modeli.Menadzer", b =>
                {
                    b.HasBaseType("Backend.Data.Modeli.KorisnickiNalog");

                    b.ToTable("Menadzeri");
                });

            modelBuilder.Entity("Backend.Data.Modeli.Cijena", b =>
                {
                    b.HasOne("Backend.Data.Modeli.Soba", "Soba")
                        .WithMany("Cijene")
                        .HasForeignKey("SobaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Soba");
                });

            modelBuilder.Entity("Backend.Data.Modeli.Recenzija", b =>
                {
                    b.HasOne("Backend.Data.Modeli.Gost", "Gost")
                        .WithMany()
                        .HasForeignKey("GostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Backend.Data.Modeli.Soba", "Soba")
                        .WithMany()
                        .HasForeignKey("SobaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Gost");

                    b.Navigation("Soba");
                });

            modelBuilder.Entity("Backend.Data.Modeli.SobaAranzman", b =>
                {
                    b.HasOne("Backend.Data.Modeli.Aranzman", "Aranzman")
                        .WithMany()
                        .HasForeignKey("AranzmanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Backend.Data.Modeli.Soba", "Soba")
                        .WithMany("Aranzmani")
                        .HasForeignKey("SobaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Aranzman");

                    b.Navigation("Soba");
                });

            modelBuilder.Entity("Backend.Data.Modeli.SobaKrevet", b =>
                {
                    b.HasOne("Backend.Data.Modeli.Krevet", "Krevet")
                        .WithMany()
                        .HasForeignKey("KrevetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Backend.Data.Modeli.Soba", "Soba")
                        .WithMany("Kreveti")
                        .HasForeignKey("SobaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Krevet");

                    b.Navigation("Soba");
                });

            modelBuilder.Entity("Backend.Data.Modeli.ZauzetaSoba", b =>
                {
                    b.HasOne("Backend.Data.Modeli.Soba", "Soba")
                        .WithMany("ZauzetaSoba")
                        .HasForeignKey("SobaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Soba");
                });

            modelBuilder.Entity("Backend.Data.Modeli.Gost", b =>
                {
                    b.HasOne("Backend.Data.Modeli.KorisnickiNalog", null)
                        .WithOne()
                        .HasForeignKey("Backend.Data.Modeli.Gost", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Backend.Data.Modeli.Menadzer", b =>
                {
                    b.HasOne("Backend.Data.Modeli.KorisnickiNalog", null)
                        .WithOne()
                        .HasForeignKey("Backend.Data.Modeli.Menadzer", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Backend.Data.Modeli.Soba", b =>
                {
                    b.Navigation("Aranzmani");

                    b.Navigation("Cijene");

                    b.Navigation("Kreveti");

                    b.Navigation("ZauzetaSoba");
                });
#pragma warning restore 612, 618
        }
    }
}
