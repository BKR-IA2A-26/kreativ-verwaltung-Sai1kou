using System;
using System.Collections.Generic;
using Apex_database.Models;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace Apex_database.Data;

public partial class ApexDbContext : DbContext
{
    public ApexDbContext()
    {
    }

    public ApexDbContext(DbContextOptions<ApexDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Aufsatz> Aufsatzs { get; set; }

    public virtual DbSet<Charakter> Charakters { get; set; }

    public virtual DbSet<Faehigkeit> Faehigkeits { get; set; }

    public virtual DbSet<HeilItem> HeilItems { get; set; }

    public virtual DbSet<Karte> Kartes { get; set; }

    public virtual DbSet<LootKategorie> LootKategories { get; set; }

    public virtual DbSet<Munitionstyp> Munitionstyps { get; set; }

    public virtual DbSet<SpawnRate> SpawnRates { get; set; }

    public virtual DbSet<Waffe> Waffes { get; set; }

    public virtual DbSet<ZonePoi> ZonePois { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;database=\"apex_database\";user=root", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.4.3-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Aufsatz>(entity =>
        {
            entity.HasKey(e => e.AufsatzId).HasName("PRIMARY");

            entity.ToTable("aufsatz");

            entity.Property(e => e.AufsatzId)
                .ValueGeneratedNever()
                .HasColumnName("Aufsatz_ID");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Seltenheit).HasMaxLength(20);
            entity.Property(e => e.Typ).HasMaxLength(50);
        });

        modelBuilder.Entity<Charakter>(entity =>
        {
            entity.HasKey(e => e.CharakterId).HasName("PRIMARY");

            entity.ToTable("charakter");

            entity.Property(e => e.CharakterId)
                .ValueGeneratedNever()
                .HasColumnName("Charakter_ID");
            entity.Property(e => e.Klasse).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Faehigkeit>(entity =>
        {
            entity.HasKey(e => e.FaehigkeitId).HasName("PRIMARY");

            entity.ToTable("faehigkeit");

            entity.HasIndex(e => e.CharakterId, "Charakter_ID");

            entity.Property(e => e.FaehigkeitId)
                .ValueGeneratedNever()
                .HasColumnName("Faehigkeit_ID");
            entity.Property(e => e.CharakterId).HasColumnName("Charakter_ID");
            entity.Property(e => e.CooldownSek).HasColumnName("Cooldown_Sek");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Typ).HasMaxLength(50);

            entity.HasOne(d => d.Charakter).WithMany(p => p.Faehigkeits)
                .HasForeignKey(d => d.CharakterId)
                .HasConstraintName("faehigkeit_ibfk_1");
        });

        modelBuilder.Entity<HeilItem>(entity =>
        {
            entity.HasKey(e => e.HeilId).HasName("PRIMARY");

            entity.ToTable("heil_item");

            entity.Property(e => e.HeilId)
                .ValueGeneratedNever()
                .HasColumnName("Heil_ID");
            entity.Property(e => e.AnwendungsdauerSek)
                .HasPrecision(3, 1)
                .HasColumnName("Anwendungsdauer_Sek");
            entity.Property(e => e.HeilungHp).HasColumnName("Heilung_HP");
            entity.Property(e => e.HeilungSchild).HasColumnName("Heilung_Schild");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Karte>(entity =>
        {
            entity.HasKey(e => e.KarteId).HasName("PRIMARY");

            entity.ToTable("karte");

            entity.Property(e => e.KarteId)
                .ValueGeneratedNever()
                .HasColumnName("Karte_ID");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<LootKategorie>(entity =>
        {
            entity.HasKey(e => e.KategorieId).HasName("PRIMARY");

            entity.ToTable("loot_kategorie");

            entity.Property(e => e.KategorieId)
                .ValueGeneratedNever()
                .HasColumnName("Kategorie_ID");
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasMany(d => d.Heils).WithMany(p => p.Kategories)
                .UsingEntity<Dictionary<string, object>>(
                    "KategorieHeilItem",
                    r => r.HasOne<HeilItem>().WithMany()
                        .HasForeignKey("HeilId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("kategorie_heil_item_ibfk_2"),
                    l => l.HasOne<LootKategorie>().WithMany()
                        .HasForeignKey("KategorieId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("kategorie_heil_item_ibfk_1"),
                    j =>
                    {
                        j.HasKey("KategorieId", "HeilId")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("kategorie_heil_item");
                        j.HasIndex(new[] { "HeilId" }, "Heil_ID");
                        j.IndexerProperty<int>("KategorieId").HasColumnName("Kategorie_ID");
                        j.IndexerProperty<int>("HeilId").HasColumnName("Heil_ID");
                    });

            entity.HasMany(d => d.Waffes).WithMany(p => p.Kategories)
                .UsingEntity<Dictionary<string, object>>(
                    "KategorieWaffe",
                    r => r.HasOne<Waffe>().WithMany()
                        .HasForeignKey("WaffeId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("kategorie_waffe_ibfk_2"),
                    l => l.HasOne<LootKategorie>().WithMany()
                        .HasForeignKey("KategorieId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("kategorie_waffe_ibfk_1"),
                    j =>
                    {
                        j.HasKey("KategorieId", "WaffeId")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("kategorie_waffe");
                        j.HasIndex(new[] { "WaffeId" }, "Waffe_ID");
                        j.IndexerProperty<int>("KategorieId").HasColumnName("Kategorie_ID");
                        j.IndexerProperty<int>("WaffeId").HasColumnName("Waffe_ID");
                    });
        });

        modelBuilder.Entity<Munitionstyp>(entity =>
        {
            entity.HasKey(e => e.MunitionId).HasName("PRIMARY");

            entity.ToTable("munitionstyp");

            entity.Property(e => e.MunitionId)
                .ValueGeneratedNever()
                .HasColumnName("Munition_ID");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<SpawnRate>(entity =>
        {
            entity.HasKey(e => new { e.PoiId, e.KategorieId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("spawn_rate");

            entity.HasIndex(e => e.KategorieId, "Kategorie_ID");

            entity.Property(e => e.PoiId).HasColumnName("POI_ID");
            entity.Property(e => e.KategorieId).HasColumnName("Kategorie_ID");
            entity.Property(e => e.WahrscheinlichkeitProzent).HasColumnName("Wahrscheinlichkeit_Prozent");

            entity.HasOne(d => d.Kategorie).WithMany(p => p.SpawnRates)
                .HasForeignKey(d => d.KategorieId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("spawn_rate_ibfk_2");

            entity.HasOne(d => d.Poi).WithMany(p => p.SpawnRates)
                .HasForeignKey(d => d.PoiId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("spawn_rate_ibfk_1");
        });

        modelBuilder.Entity<Waffe>(entity =>
        {
            entity.HasKey(e => e.WaffeId).HasName("PRIMARY");

            entity.ToTable("waffe");

            entity.HasIndex(e => e.MunitionId, "Munition_ID");

            entity.Property(e => e.WaffeId)
                .ValueGeneratedNever()
                .HasColumnName("Waffe_ID");
            entity.Property(e => e.BasisSchaden).HasColumnName("Basis_Schaden");
            entity.Property(e => e.MunitionId).HasColumnName("Munition_ID");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.WaffenTyp)
                .HasMaxLength(50)
                .HasColumnName("Waffen_Typ");

            entity.HasOne(d => d.Munition).WithMany(p => p.Waffes)
                .HasForeignKey(d => d.MunitionId)
                .HasConstraintName("waffe_ibfk_1");

            entity.HasMany(d => d.Aufsatzs).WithMany(p => p.Waffes)
                .UsingEntity<Dictionary<string, object>>(
                    "WaffenKompatibilitaet",
                    r => r.HasOne<Aufsatz>().WithMany()
                        .HasForeignKey("AufsatzId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("waffen_kompatibilitaet_ibfk_2"),
                    l => l.HasOne<Waffe>().WithMany()
                        .HasForeignKey("WaffeId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("waffen_kompatibilitaet_ibfk_1"),
                    j =>
                    {
                        j.HasKey("WaffeId", "AufsatzId")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("waffen_kompatibilitaet");
                        j.HasIndex(new[] { "AufsatzId" }, "Aufsatz_ID");
                        j.IndexerProperty<int>("WaffeId").HasColumnName("Waffe_ID");
                        j.IndexerProperty<int>("AufsatzId").HasColumnName("Aufsatz_ID");
                    });
        });

        modelBuilder.Entity<ZonePoi>(entity =>
        {
            entity.HasKey(e => e.PoiId).HasName("PRIMARY");

            entity.ToTable("zone_poi");

            entity.HasIndex(e => e.KarteId, "Karte_ID");

            entity.Property(e => e.PoiId)
                .ValueGeneratedNever()
                .HasColumnName("POI_ID");
            entity.Property(e => e.KarteId).HasColumnName("Karte_ID");
            entity.Property(e => e.LootTier)
                .HasMaxLength(20)
                .HasColumnName("Loot_Tier");
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.Karte).WithMany(p => p.ZonePois)
                .HasForeignKey(d => d.KarteId)
                .HasConstraintName("zone_poi_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
