using Filmes.WebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Filmes.WebAPI.Models;

[Table("Filme")]
public partial class Filme
{
    [Key]
    [StringLength(40)]
    [Unicode(false)]
    public string IdFilme { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string Titulo { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string? Imagem { get; set; }

    [StringLength(40)]
    [Unicode(false)]
    public string? IdGenero { get; set; }

    [JsonIgnore]
    [ForeignKey("IdGenero")]
    [InverseProperty("Filmes")]
    public virtual Genero? IdGeneroNavigation { get; set; }
}