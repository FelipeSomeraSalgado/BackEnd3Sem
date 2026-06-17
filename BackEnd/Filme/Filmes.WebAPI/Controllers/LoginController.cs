using Filmes.WebAPI.DTO;
using Filmes.WebAPI.Interfaces;
using Filmes.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace FilmesMoura1.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly IUsuarioRepository _usuarioRepository;
    public LoginController(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    [HttpPost]
    public IActionResult Login(LoginDTO loginDTO)
    {
        try
        {
            Usuario usuarioBuscado = _usuarioRepository.BuscarPorEmailESenha(loginDTO.Email!, loginDTO.Senha!);

            if (usuarioBuscado == null)
            {
                return NotFound("Email ou senha inválidos");
            }

            //caso encontre o usuário, prossegue para a criacao do token
            //primeiro - definir as informacoes(claims) que serao fornecidos no token
            var claims = new[]
            {
                //formato da claim
                new Claim(JwtRegisteredClaimNames.Jti, usuarioBuscado.IdUsuario),

                new Claim(JwtRegisteredClaimNames.Email, usuarioBuscado.Email!)

                //existe a possibilidade de criar uma claim personalizada
                //new Claim("Claim Personalizada", "Valor da Claim")

            };

            //segundo - definir a chave de acesso ao token
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("filmes-chave-autenticacao-webapi-dev"));

            //terceiro - definir as credenciais do token
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //quarto - gerar o token
            var token = new JwtSecurityToken
                (
                    //emissor do token
                    issuer: "api_filmes",
                    //destinatário do token
                    audience: "api_filmes",
                    //dados definidos nas claims
                    claims: claims,
                    //tempo de expiração do token
                    expires: DateTime.Now.AddMinutes(5),
                    //credenciais do token
                    signingCredentials: creds
                );

            //quinto - retornar o token criado
            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token)
            });

        }
        catch (Exception erro)
        {

            return BadRequest(erro.Message);
        }
    }

}
