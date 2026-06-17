using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LivrosBD.Models;

[Table("Genero")]
public partial class Genero
{
    [Key]
    public int IdGenero { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string Nome { get; set; } = null!;

    [JsonIgnore]
    [InverseProperty("IdGeneroNavigation")]
    public virtual ICollection<Livro> Livros { get; set; } = new List<Livro>();
}
