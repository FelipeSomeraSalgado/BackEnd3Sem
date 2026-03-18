using System.ComponentModel.DataAnnotations;

namespace EventPlus.WebAPI.DTO;

public class LoginDTO : TipoUsuarioDTO
{
    [Required(ErrorMessage = "O campo Email é obrigatório! ")]
    public string Email { get; set; }

    [Required(ErrorMessage = "O campo Senha é obrigatória! ")]
    public string Senha { get; set; }

    [Required(ErrorMessage = "Informe seu tipo de usuário ")]
    public string Titulo { get; set; }
}
