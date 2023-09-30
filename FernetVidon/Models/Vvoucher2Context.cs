using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FernetVidon.Models;

public partial class Vvoucher2Context : DbContext
{
    public Vvoucher2Context()
    {
    }

    public Vvoucher2Context(DbContextOptions<Vvoucher2Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Botellas> Botellas { get; set; }



    public virtual DbSet<Cliente> Clientes { get; set; }



    public virtual DbSet<Sucursales> Sucursales { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("naachocaceres");

        modelBuilder.Entity<Botellas>(entity =>
        {
            entity.HasKey(e => e.IdBotella).HasName("PK__Botellas__574D289E71013D94");

            entity.ToTable("Botellas", "Clientes");

            entity.Property(e => e.IdBotella).HasColumnName("idBotella");
            entity.Property(e => e.Estado)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("estado");
            entity.Property(e => e.FechaGuardado)
                .HasColumnType("date")
                .HasColumnName("fechaGuardado");
            entity.Property(e => e.FechaVencimiento)
                .HasColumnType("date")
                .HasColumnName("fechaVencimiento");
            entity.Property(e => e.IdCliente).HasColumnName("idCliente");
            entity.Property(e => e.IdSucursal).HasColumnName("idSucursal");
            entity.Property(e => e.QuienGuardoMozo)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("quienGuardoMozo");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Botellas)
                .HasForeignKey(d => d.IdCliente)
                .HasConstraintName("FK__Botellas__idClie__0B91BA14");

            entity.HasOne(d => d.IdSucursalNavigation).WithMany(p => p.Botellas)
                .HasForeignKey(d => d.IdSucursal)
                .HasConstraintName("FK__Botellas__idSucu__0C85DE4D");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente).HasName("PK__Cliente__885457EEAE222EB2");

            entity.ToTable("Cliente", "Clientes");

            entity.Property(e => e.IdCliente).HasColumnName("idCliente");
            entity.Property(e => e.Apellido)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("apellido");
            entity.Property(e => e.FechaNacimiento)
                .HasColumnType("date")
                .HasColumnName("fechaNacimiento");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.NumeroTelefono)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("numeroTelefono");
        });

        modelBuilder.Entity<Sucursales>(entity =>
        {
            entity.HasKey(e => e.IdSucursal);

            entity.ToTable("SUCURSALES");

            entity.Property(e => e.IdSucursal).HasColumnName("idSucursal");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
