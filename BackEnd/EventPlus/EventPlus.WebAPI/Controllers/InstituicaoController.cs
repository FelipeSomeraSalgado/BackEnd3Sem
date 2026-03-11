using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstituicaoController : ControllerBase
    {
        private IInstituicaoRepository _instituicaoRepository;

        public InstituicaoController(IInstituicaoRepository instituicaoRepository)
        {
            _instituicaoRepository = instituicaoRepository;
        }


        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                return Ok(_instituicaoRepository.Listar());
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult BuscarPorId(Guid id)
        {
            try
            {
                return Ok(_instituicaoRepository.BuscarPorId(id));
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }

        [HttpPost]
        public IActionResult Cadastrar(InstituicaoDTO instituicao)
        {
            try
            {
                var novoInstituicao = new Instituicao
                {
                    Endereco = instituicao.Endereco!,
                     NomeFantasia = instituicao.NomeFantasia!,
                      Cnpj = instituicao.Cnpj!
                };

                _instituicaoRepository.Cadastrar(novoInstituicao);
                return StatusCode(201, novoInstituicao);
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(Guid id, InstituicaoDTO instituicao)
        {
            try
            {
                var instituicaoAtualizado = new Instituicao
                {
                    Endereco = instituicao.Endereco!,
                    NomeFantasia = instituicao.NomeFantasia!,
                    Cnpj = instituicao.Cnpj!
                };

                _instituicaoRepository.Atualizar(id, instituicaoAtualizado);
                return StatusCode(204, instituicaoAtualizado);
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }

        [HttpDelete("{id}")]

        public IActionResult Delete(Guid id)
        {
            try
            {
                _instituicaoRepository.Deletar(id);
                return NoContent();
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }
    }
}

