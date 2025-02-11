using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace stage.Models;

public partial class LabContext : DbContext
{
    protected readonly IConfiguration Configuration;
    public LabContext()
    {
    }

    public LabContext(DbContextOptions<LabContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Banque> Banques { get; set; }
    public virtual DbSet<Paillasse> Paillasses { get; set; }

    public virtual DbSet<Tube> Tube { get; set; }

    public virtual DbSet<TypeTube> Type_Tube { get; set; }
    

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\SQL2022;Database=stage;user id=sa;Password=123;Persist Security Info=False;MultipleActiveResultSets=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Banque>(entity =>
        {
            entity.HasKey(e => e.Matban);

            entity.ToTable("Banque");

            entity.Property(e => e.Matban)
                .HasMaxLength(10)
                .HasColumnName("matban");
            entity.Property(e => e.Desban)
                .HasMaxLength(50)
                .HasColumnName("desban");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
        });
        modelBuilder.Entity<Paillasse>(entity =>
        {
            entity.HasKey(e => e.CodePaillasse);

            entity.ToTable("Paillasse");

            entity.Property(e => e.CodePaillasse)
                .HasMaxLength(10)
                .HasColumnName("Code_Paillasse");
        });

        modelBuilder.Entity<Tube>(entity =>
        {
            entity.HasKey(e => e.codeTube);

            entity.ToTable("Tube");

            entity.Property(e => e.codeTube)
                .HasMaxLength(10)
                .HasColumnName("codeTube");
            entity.Property(e => e.LiblletTube)
                .HasMaxLength(50)
                .HasColumnName("LiblletTube");
            entity.Property(e => e.nom_image)
                .HasMaxLength(50)
                .HasColumnName("nom_image");
            entity.Property(e => e.code_Type)
                .HasMaxLength(50)
                .HasColumnName("code_Type");
        });

        modelBuilder.Entity<TypeTube>(entity =>
        {
            entity.HasKey(e => e.CodeType).HasName("PK_type_Tubeeeee");

            entity.ToTable("Type_Tube");

            entity.Property(e => e.CodeType).HasColumnName("code_Type");
            entity.Property(e => e.Couleur).HasColumnName("couleur");
            entity.Property(e => e.DateCreate)
                .HasColumnType("datetime")
                .HasColumnName("date_create");
            entity.Property(e => e.LiblletType)
                .HasMaxLength(400)
                .HasColumnName("Libllet_Type");
            entity.Property(e => e.NomImage)
                .HasMaxLength(4)
                .HasColumnName("nom_image");
            entity.Property(e => e.UserCreate)
                .HasMaxLength(50)
                .HasColumnName("user_create");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
