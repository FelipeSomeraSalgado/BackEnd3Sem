using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LivrosBD.Models;

[Table("Livro")]
public partial class Livro
{
    [Key]
    public int IdLivro { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Imagem { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string Titulo { get; set; } = null!;

    public int? IdGenero { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string Autor { get; set; } = null!;

    public int Ano { get; set; }

    [ForeignKey("IdGenero")]
    [InverseProperty("Livros")]
    [JsonIgnore]
    public virtual Genero? IdGeneroNavigation { get; set; }
}
