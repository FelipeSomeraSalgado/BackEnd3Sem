using Filmes.WebAPI.Interfaces;
using Filmes.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FilmesMoura1.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GeneroController : ControllerBase
{
    private readonly IGeneroRepository _generoRepository;

    public GeneroController(IGeneroRepository generoRepository)
    {
        _generoRepository = generoRepository;
    }

    [HttpGet("{id}")]

    // Busca o gênero pelo id
    public IActionResult GetById(Guid id)
    {
        try
        {
            return Ok(_generoRepository.BuscarPorId(id));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]

    // Busca todos os gêneros
    public IActionResult Get()
    {
        try
        {
            return Ok(_generoRepository.Listar());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]

    // Cadastra um novo gênero
    public IActionResult Post(Genero novoGenero)
    {
        try
        {
            _generoRepository.Cadastrar(novoGenero);
            return StatusCode(201);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]

    // Atualiza um gênero existente
    public IActionResult Put(Guid id, Genero generoAtualizado)
    {
        try
        {
            _generoRepository.AtualizarIdUrl(id, generoAtualizado);
            return NoContent();
        }
        catch (Exception e)
        {

            return BadRequest(e.Message);
        }
    }

    [HttpPut]

    // Atualiza um gênero existente usando o id
    public IActionResult Put(Genero generoAtualizado)
    {
        try
        {
            _generoRepository.AtualizarIdCorpo(generoAtualizado);
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{id}")]

    // Deleta um gênero existente
    public IActionResult Delete(Guid id)
    {
        try
        {
            _generoRepository.Deletar(id);
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

}
