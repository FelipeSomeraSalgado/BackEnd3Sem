using System.ComponentModel.DataAnnotations;

namespace Filmes.WebAPI.DTO;

public class LoginDTO
{
    [Required(ErrorMessage = "O campo Email é obrigatório! ")]
    public string Email { get; set; }

    [Required(ErrorMessage = "O campo Senha é obrigatória! ")]
    public string Senha { get; set; }
}
