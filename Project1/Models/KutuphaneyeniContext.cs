﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Project1.Models;

public partial class KutuphaneyeniContext : DbContext
{
    public KutuphaneyeniContext()
    {
    }

    public KutuphaneyeniContext(DbContextOptions<KutuphaneyeniContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Anonimkitaplar> Anonimkitaplars { get; set; }

    public virtual DbSet<Islem> Islems { get; set; }

    public virtual DbSet<Kitap> Kitaps { get; set; }

    public virtual DbSet<Kitap2> Kitap2s { get; set; }

    public virtual DbSet<Ogrenci> Ogrencis { get; set; }

    public virtual DbSet<Siniflar> Siniflars { get; set; }

    public virtual DbSet<Tur> Turs { get; set; }

    public virtual DbSet<Yazar> Yazars { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-VTHHUCU; Initial Catalog=kutuphaneyeni; Trusted_Connection=True; Integrated Security=True; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Anonimkitaplar>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("anonimkitaplar");

            entity.Property(e => e.Ad)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("ad");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Sayi).HasColumnName("sayi");
            entity.Property(e => e.Yazarno).HasColumnName("yazarno");
        });

        modelBuilder.Entity<Islem>(entity =>
        {
            entity.HasKey(e => e.Islemno);

            entity.ToTable("islem");

            entity.Property(e => e.Islemno).HasColumnName("islemno");
            entity.Property(e => e.Atarih)
                .HasColumnType("date")
                .HasColumnName("atarih");
            entity.Property(e => e.Kitapno).HasColumnName("kitapno");
            entity.Property(e => e.Ogrno).HasColumnName("ogrno");
            entity.Property(e => e.Vtarih)
                .HasColumnType("date")
                .HasColumnName("vtarih");

            entity.HasOne(d => d.KitapnoNavigation).WithMany(p => p.Islems)
                .HasForeignKey(d => d.Kitapno)
                .HasConstraintName("FK_islem_kitap");

            entity.HasOne(d => d.OgrnoNavigation).WithMany(p => p.Islems)
                .HasForeignKey(d => d.Ogrno)
                .HasConstraintName("FK_islem_ogrenci");
        });

        modelBuilder.Entity<Kitap>(entity =>
        {
            entity.HasKey(e => e.Kitapno);

            entity.ToTable("kitap");

            entity.Property(e => e.Kitapno).HasColumnName("kitapno");
            entity.Property(e => e.Ad)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("ad");
            entity.Property(e => e.Puan).HasColumnName("puan");
            entity.Property(e => e.Sayfasayisi).HasColumnName("sayfasayisi");
            entity.Property(e => e.Turno).HasColumnName("turno");
            entity.Property(e => e.Yazarno).HasColumnName("yazarno");

            entity.HasOne(d => d.TurnoNavigation).WithMany(p => p.Kitaps)
                .HasForeignKey(d => d.Turno)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_kitap_tur");

            entity.HasOne(d => d.YazarnoNavigation).WithMany(p => p.Kitaps)
                .HasForeignKey(d => d.Yazarno)
                .HasConstraintName("FK_kitap_yazar1");
        });

        modelBuilder.Entity<Kitap2>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("kitap2");

            entity.Property(e => e.Ad)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("ad");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Sayi).HasColumnName("sayi");
            entity.Property(e => e.Yazarno).HasColumnName("yazarno");

            entity.HasOne(d => d.YazarnoNavigation).WithMany()
                .HasForeignKey(d => d.Yazarno)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_kitap_yazar");
        });

        modelBuilder.Entity<Ogrenci>(entity =>
        {
            entity.HasKey(e => e.Ogrno);

            entity.ToTable("ogrenci");

            entity.Property(e => e.Ogrno).HasColumnName("ogrno");
            entity.Property(e => e.Ad)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ad");
            entity.Property(e => e.Cinsiyet)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("cinsiyet");
            entity.Property(e => e.Dtarih)
                .HasColumnType("datetime")
                .HasColumnName("dtarih");
            entity.Property(e => e.Puan).HasColumnName("puan");
            entity.Property(e => e.Sinif)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("sinif");
            entity.Property(e => e.Soyad)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("soyad");
        });

        modelBuilder.Entity<Siniflar>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("siniflar");

            entity.Property(e => e.Ad)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("ad");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
        });

        modelBuilder.Entity<Tur>(entity =>
        {
            entity.HasKey(e => e.Turno);

            entity.ToTable("tur");

            entity.Property(e => e.Turno).HasColumnName("turno");
            entity.Property(e => e.Ad)
                .HasMaxLength(30)
                .IsFixedLength()
                .HasColumnName("ad");
        });

        modelBuilder.Entity<Yazar>(entity =>
        {
            entity.HasKey(e => e.Yazarno);

            entity.ToTable("yazar");

            entity.Property(e => e.Yazarno)
                .ValueGeneratedNever()
                .HasColumnName("yazarno");
            entity.Property(e => e.Ad)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ad");
            entity.Property(e => e.Soyad)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("soyad");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
