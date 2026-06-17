using System;
using System.Collections.Generic;
using LivrosBD.Models;
using Microsoft.EntityFrameworkCore;

namespace LivrosBD.LivrosBDConnect;

public partial class LivrosBDContext : DbContext
{
    public LivrosBDContext()
    {
    }

    public LivrosBDContext(DbContextOptions<LivrosBDContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Genero> Generos { get; set; }

    public virtual DbSet<Livro> Livros { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=LivrosBD;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Genero>(entity =>
        {
            entity.HasKey(e => e.IdGenero).HasName("PK__Genero__0F834988E5B04201");
        });

        modelBuilder.Entity<Livro>(entity =>
        {
            entity.HasKey(e => e.IdLivro).HasName("PK__Livro__3B69D85A0BFB6EDA");

            entity.HasOne(d => d.IdGeneroNavigation).WithMany(p => p.Livros).HasConstraintName("FK__Livro__IdGenero__5EBF139D");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__5B65BF97456AB2CC");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
