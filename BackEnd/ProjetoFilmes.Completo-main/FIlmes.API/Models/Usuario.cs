using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FIlmes.API.Models;

[Table("Usuario")]
[Index("Email", Name = "UQ__Usuario__A9D10534EFCAAE4D", IsUnique = true)]
public partial class Usuario
{
    [Key]
    public int IdUsuario { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string Nome { get; set; } = null!;

    [StringLength(256)]
    [Unicode(false)]
    public string Email { get; set; } = null!;

    [StringLength(60)]
    [Unicode(false)]
    public string Senha { get; set; } = null!;
}
