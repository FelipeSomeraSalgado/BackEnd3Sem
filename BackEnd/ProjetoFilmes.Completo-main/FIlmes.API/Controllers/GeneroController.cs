using Filmes.API.DTO;
using FIlmes.API.Interfaces;
using FIlmes.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Filmes.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class GeneroController : ControllerBase
    {
        private readonly IGeneroRepository _generoRepository;

        public GeneroController(IGeneroRepository generoRepository)
        {
            _generoRepository = generoRepository;
        }

        [HttpGet]
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
        public IActionResult Post(GeneroDTO genero)
        {
            try
            {
                var novoGenero = new Genero
                {
                    Nome = genero.Nome
                };

                _generoRepository.Cadastrar(novoGenero);
                return StatusCode(201);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("BuscarPorId/{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                return Ok(_generoRepository.BuscarPorId(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, GeneroDTO genero)
        {
            try
            {
                var generoAtualizado = new Genero
                {
                    IdGenero = id,
                    Nome = genero.Nome,
                };

                _generoRepository.AtualizarIdUrl(id, generoAtualizado);

                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut()]
        public IActionResult PutBody(Genero genero)
        {
            try
            {
                _generoRepository.AtualizarIdCorpo(genero);

                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
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
}
