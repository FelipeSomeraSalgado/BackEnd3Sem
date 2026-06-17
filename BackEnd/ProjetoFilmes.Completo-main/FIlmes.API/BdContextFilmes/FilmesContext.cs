using System;
using System.Collections.Generic;
using FIlmes.API.Models;
using Microsoft.EntityFrameworkCore;

namespace FIlmes.API.BdContextFilmes;

public partial class FilmesContext : DbContext
{
    public FilmesContext()
    {
    }

    public FilmesContext(DbContextOptions<FilmesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Filme> Filmes { get; set; }

    public virtual DbSet<Genero> Generos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=FilmesBD;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Filme>(entity =>
        {
            entity.HasKey(e => e.IdFilme).HasName("PK__Filme__6E8F2A76FCC1C6A3");

            entity.HasOne(d => d.IdGeneroNavigation).WithMany(p => p.Filmes).HasConstraintName("FK__Filme__IdGenero__398D8EEE");
        });

        modelBuilder.Entity<Genero>(entity =>
        {
            entity.HasKey(e => e.IdGenero).HasName("PK__Genero__0F8349889C8EE97D");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__5B65BF97AFAB4A60");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
