using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FIlmes.API.Models;

[Table("Filme")]
public partial class Filme
{
    [Key]
    public int IdFilme { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Imagem { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string Titulo { get; set; } = null!;

    public int? IdGenero { get; set; }

    [ForeignKey("IdGenero")]
    [InverseProperty("Filmes")]
    public virtual Genero? IdGeneroNavigation { get; set; }
}
