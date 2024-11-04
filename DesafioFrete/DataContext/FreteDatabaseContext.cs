using System;
using System.Collections.Generic;
using DesafioFrete.Models;
using Microsoft.EntityFrameworkCore;

namespace DataContext;

public partial class FreteDatabaseContext : DbContext
{
    private readonly IConfiguration _configuration;
    public FreteDatabaseContext()
    {
    }

    public FreteDatabaseContext(DbContextOptions<FreteDatabaseContext> options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
    }

    public virtual DbSet<Frete> Fretes { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Veiculo> Veiculos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Frete>(entity =>
        {
            entity.HasKey(e => e.FreteId).HasName("PK__Frete__EF3EDF75D485CAA9");

            entity.ToTable("Frete");

            entity.Property(e => e.FreteId).HasColumnName("FreteID");
            entity.Property(e => e.CidadeDestino)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CidadeOrigem)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");
            entity.Property(e => e.ValorFrete).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ValorTaxa).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Fretes)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Frete__UsuarioID__3D5E1FD2");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioId).HasName("PK__Usuario__2B3DE7981C3E0335");

            entity.ToTable("Usuario");

            entity.HasIndex(e => e.Email, "UQ__Usuario__A9D105343C508D69").IsUnique();

            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Nome)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Senha)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Telefone)
                .HasMaxLength(14)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Veiculo>(entity =>
        {
            entity.HasKey(e => e.VeiculoId).HasName("PK__Veiculo__A5253AD843180D57");

            entity.ToTable("Veiculo");

            entity.HasIndex(e => e.Renavam, "UQ__Veiculo__5BAEE8D8CBA3424D").IsUnique();

            entity.Property(e => e.VeiculoId).HasColumnName("VeiculoID");
            entity.Property(e => e.Placa)
                .HasMaxLength(7)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Renavam)
                .HasMaxLength(11)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("RENAVAM");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
