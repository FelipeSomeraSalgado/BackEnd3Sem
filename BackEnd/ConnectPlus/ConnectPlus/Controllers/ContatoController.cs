using ConnectPlus.DTO;
using ConnectPlus.Interfaces;
using ConnectPlus.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConnectPlus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContatoController : ControllerBase
    {
        private IContatoRepository _contatoRepository;

        public ContatoController(IContatoRepository contatoRepository)
        {
            _contatoRepository = contatoRepository;
        }


        [HttpGet("{id}")]
        public IActionResult BuscarPorId(Guid id)
        {
            try
            {
                var contato = _contatoRepository.BuscarPorId(id);

                if (contato == null)
                    return NotFound();

                return Ok(contato);
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }

        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                return Ok(_contatoRepository.Listar());
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] ContatoDTO novoContato)
        {
            if (string.IsNullOrWhiteSpace(novoContato.Nome))
            {
                return BadRequest("O campo Nome é obrigatório.");
            }

            Contato contato = new Contato();

            if (novoContato.Imagem != null && novoContato.Imagem.Length > 0)
            {
                var extensao = Path.GetExtension(novoContato.Imagem.FileName);
                var nomeArquivo = $"{Guid.NewGuid()}{extensao}";

                var pastaRelativa = "wwwroot/Imagens";
                var caminhoPasta = Path.Combine(Directory.GetCurrentDirectory(), pastaRelativa);

                if (!Directory.Exists(caminhoPasta))
                {
                    Directory.CreateDirectory(caminhoPasta);
                }

                var caminhoCompleto = Path.Combine(caminhoPasta, nomeArquivo);

                using (var stream = new FileStream(caminhoCompleto, FileMode.Create))
                {
                    await novoContato.Imagem.CopyToAsync(stream);
                }

                contato.Imagem = nomeArquivo;
            }

            contato.Nome = novoContato.Nome!;
            contato.FormaContato = novoContato.FormaContato!;
            contato.IdTipoContato = novoContato.IdTipoContato;

            try
            {
                _contatoRepository.Cadastrar(contato);
                return StatusCode(201);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(Guid id, Contato contato)
        {
            try
            {
                _contatoRepository.Atualizar(id, contato);
                return Ok("Contato atualizado com sucesso!");
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(Guid id)
        {
            try
            {
                _contatoRepository.Deletar(id);
                return Ok("Contato deletado com sucesso!");
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }
    }
}

