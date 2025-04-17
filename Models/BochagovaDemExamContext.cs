using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Module2.Models;

public partial class BochagovaDemExamContext : DbContext
{
    public BochagovaDemExamContext()
    {
    }

    public BochagovaDemExamContext(DbContextOptions<BochagovaDemExamContext> options)
        : base(options)
    {
    }

    public virtual DbSet<MaterialType> MaterialTypes { get; set; }

    public virtual DbSet<Partner> Partners { get; set; }

    public virtual DbSet<PartnerProduct> PartnerProducts { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductType> ProductTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=hqvla3302s01\\KITP;Initial Catalog=Bochagova_DemExam;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MaterialType>(entity =>
        {
            entity.HasKey(e => e.IdMaterialType);

            entity.ToTable("Material_Type");

            entity.Property(e => e.IdMaterialType).HasColumnName("ID_Material_Type");
            entity.Property(e => e.PercentDefect)
                .HasColumnType("decimal(10, 4)")
                .HasColumnName("Percent_Defect");
            entity.Property(e => e.TypeMaterial)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Type_Material");
        });

        modelBuilder.Entity<Partner>(entity =>
        {
            entity.HasKey(e => e.IdPartner).HasName("PK_Partners");

            entity.ToTable("Partner");

            entity.Property(e => e.IdPartner).HasColumnName("ID_Partner");
            entity.Property(e => e.CityPartner)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("City_Partner");
            entity.Property(e => e.EmailPartner)
                .HasMaxLength(100)
                .HasColumnName("Email_Partner");
            entity.Property(e => e.FirstNameDirectorPartner)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("FirstName_Director_Partner");
            entity.Property(e => e.HousePartner)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("House_Partner");
            entity.Property(e => e.IndexPartner).HasColumnName("Index_Partner");
            entity.Property(e => e.InnPartner).HasColumnName("INN_Partner");
            entity.Property(e => e.LastNameDirectorPartner)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("LastName_Director_Partner");
            entity.Property(e => e.NamePartner)
                .IsUnicode(false)
                .HasColumnName("Name_Partner");
            entity.Property(e => e.PatronymicDirectorPartner)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Patronymic_Director_Partner");
            entity.Property(e => e.PhonePartner)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Phone_Partner");
            entity.Property(e => e.RatingPartner).HasColumnName("Rating_Partner");
            entity.Property(e => e.RegionPartner)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("Region_Partner");
            entity.Property(e => e.StreetPartner)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Street_Partner");
            entity.Property(e => e.TypePartner)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Type_Partner");
        });

        modelBuilder.Entity<PartnerProduct>(entity =>
        {
            entity.HasKey(e => e.IdPartnerProduct).HasName("PK_Partner_Products");

            entity.ToTable("Partner_Product");

            entity.Property(e => e.IdPartnerProduct).HasColumnName("ID_Partner_Product");
            entity.Property(e => e.CountProduct).HasColumnName("Count_Product");
            entity.Property(e => e.DateSale).HasColumnName("Date_Sale");
            entity.Property(e => e.IdPartner).HasColumnName("ID_Partner");
            entity.Property(e => e.IdProduct).HasColumnName("ID_Product");

            entity.HasOne(d => d.IdPartnerNavigation).WithMany(p => p.PartnerProducts)
                .HasForeignKey(d => d.IdPartner)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Partner_Products_Partners");

            entity.HasOne(d => d.IdProductNavigation).WithMany(p => p.PartnerProducts)
                .HasForeignKey(d => d.IdProduct)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Partner_Products_Products");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.IdProduct).HasName("PK_Products");

            entity.ToTable("Product");

            entity.Property(e => e.IdProduct).HasColumnName("ID_Product");
            entity.Property(e => e.ArticleProduct).HasColumnName("Article_Product");
            entity.Property(e => e.IdProductType).HasColumnName("ID_Product_Type");
            entity.Property(e => e.MinimalCostForAPartner)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("Minimal_Cost_For_A_Partner");
            entity.Property(e => e.NameProduct)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("Name_Product");

            entity.HasOne(d => d.IdProductTypeNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.IdProductType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Products_Product_Type");
        });

        modelBuilder.Entity<ProductType>(entity =>
        {
            entity.HasKey(e => e.IdProductType);

            entity.ToTable("Product_Type");

            entity.Property(e => e.IdProductType).HasColumnName("ID_Product_Type");
            entity.Property(e => e.CoefficentType)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("Coefficent_Type");
            entity.Property(e => e.NameType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Name_Type");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
