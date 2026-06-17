using Filmes.API.DTO;
using FIlmes.API.Interfaces;
using FIlmes.API.Models;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;

namespace Filmes.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class FilmeController : ControllerBase
    {
        private readonly IFilmeRepository _filmeRepository;

        public FilmeController(IFilmeRepository filmeRepository)
        {
            _filmeRepository = filmeRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_filmeRepository.Listar());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] FilmeDTO filme)
        {

            if (String.IsNullOrWhiteSpace(filme.Titulo) || filme.IdGenero == 0)
                return BadRequest("É obrigatório que o filme tenha Nome e Gênero");

            Filme novoFilme = new Filme();

            if (filme.Imagem != null && filme.Imagem.Length != 0)
            {
                var extensao = Path.GetExtension(filme.Imagem.FileName);
                var nomeArquivo = $"{Guid.NewGuid()}{extensao}";

                var pastaRelativa = "wwwroot/imagens";
                var caminhoPasta = Path.Combine(Directory.GetCurrentDirectory(), pastaRelativa);

                //Garante que a pasta exista
                if (!Directory.Exists(caminhoPasta))
                    Directory.CreateDirectory(caminhoPasta);

                var caminhoCompleto = Path.Combine(caminhoPasta, nomeArquivo);

                using (var stream = new FileStream(caminhoCompleto, FileMode.Create))
                {
                    await filme.Imagem.CopyToAsync(stream);
                }

                novoFilme.Imagem = nomeArquivo;
            }

            novoFilme.IdGenero = filme.IdGenero;
            novoFilme.Titulo = filme.Titulo;

            try
            {
                _filmeRepository.Cadastrar(novoFilme);
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
                return Ok(_filmeRepository.BuscarPorId(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, FilmeDTO? filmeAtualizado)
        {

            var filme = _filmeRepository.BuscarPorId(id);
            if (filme == null)
                return NotFound("Filme não encontrado.");

            if (!string.IsNullOrWhiteSpace(filmeAtualizado.Titulo))
                filme.Titulo = filmeAtualizado.Titulo;

            if (filme.IdGenero != filmeAtualizado.IdGenero && filmeAtualizado.IdGenero != 0)
                filme.IdGenero = filmeAtualizado.IdGenero;
            if (filmeAtualizado.Imagem != null && filmeAtualizado.Imagem.Length != 0)
            {
                 var pastaRelativa = "wwwroot/imagens";
                 var caminhoPasta = Path.Combine(Directory.GetCurrentDirectory(), pastaRelativa);

                // Deleta arquivo antigo
                if (!String.IsNullOrEmpty(filme.Imagem))
                {

                    var caminhoAntigo = Path.Combine(caminhoPasta, filme.Imagem);

                    if(System.IO.File.Exists(caminhoAntigo))
                        System.IO.File.Delete(caminhoAntigo);
                }

                // Salva nova imagem
                var extensao = Path.GetExtension(filmeAtualizado.Imagem.FileName);
                var nomeArquivo = $"{Guid.NewGuid()}{extensao}";

                if (!Directory.Exists(caminhoPasta))
                    Directory.CreateDirectory(caminhoPasta);

                var caminhoCompleto = Path.Combine(caminhoPasta, nomeArquivo);
                using (var stream = new FileStream(caminhoCompleto, FileMode.Create))
                {
                    await filmeAtualizado.Imagem.CopyToAsync(stream);
                }

                filme.Imagem = nomeArquivo;
            }

            try
            {
                _filmeRepository.AtualizarIdUrl(id, filme);

                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public IActionResult PutBody(Filme filme)
        {
            try
            {
                _filmeRepository.AtualizarIdCorpo(filme);

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
            var filme = _filmeRepository.BuscarPorId(id);

            var pastaRelativa = "wwwroot/imagens";
            var caminhoPasta = Path.Combine(Directory.GetCurrentDirectory(), pastaRelativa);

            // Deleta arquivo
            if (!String.IsNullOrEmpty(filme.Imagem))
            {

                var caminho = Path.Combine(caminhoPasta, filme.Imagem);

                if (System.IO.File.Exists(caminho))
                    System.IO.File.Delete(caminho);
            }

            try
            {
                _filmeRepository.Deletar(id);

                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
