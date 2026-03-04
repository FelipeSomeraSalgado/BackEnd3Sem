
using Filmes.WebAPI.DTO;
using Filmes.WebAPI.Interfaces;
using Filmes.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Xml.Linq;

namespace Filmes.WebAPI.Controllers;

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
       public IActionResult Login(LoginDTO loginDto)
        {
        try
        {
            Usuario usuarioBuscado = _usuarioRepository.BuscarPorEmailESenha(loginDto.Email!, loginDto.Senha!);

            if (usuarioBuscado == null)
            {
                return NotFound("Email ou Senha inválidos.");
            }

            //Caso encontre o usúario, prossegue para a criação do Token

            //1* - Definir as informações(Claims) que serão fornecidas no Token

            var claims = new[]
            {
                //formato da claim
                new Claim(JwtRegisteredClaimNames.Jti,
                  usuarioBuscado.IdUsuario!),
                new Claim(JwtRegisteredClaimNames.Email,
                  usuarioBuscado.Email!),

                //Existe a possibilidade de criar uma claim personalizada
                // new Claim("Claim Personalizada", "Valor da Claim")   
            };

            //2* - Definir a chave de acesso ao Token
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("filmes-chave-autenticacao-webapi-dev"));

            //3* - Definir as credenciais do Token (Header)
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //4* - Gerar o Token
            var token = new JwtSecurityToken(
                issuer: "api_filmes", //Emissor do Token
                audience: "api_filmes", //Destinatário do Token
                claims: claims, //Dados definidos nas Claims
                expires: DateTime.Now.AddMinutes(5), //Tempo de expiração do Token
                signingCredentials: creds //Credenciais do Token
            );

            //5* - Retornar o Token criado
            return Ok(new
                {
                token = new JwtSecurityTokenHandler().WriteToken(token)
                });
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
