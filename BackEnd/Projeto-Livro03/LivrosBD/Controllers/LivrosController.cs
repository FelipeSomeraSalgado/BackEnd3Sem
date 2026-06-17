using LivrosBD.DTO;
using LivrosBD.Interfaces;
using LivrosBD.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Net.WebRequestMethods;

namespace LivrosBD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivrosController : ControllerBase
    {
        private readonly ILivrosRepository _livrosRepository;

        public LivrosController(ILivrosRepository livrosRepository)
        {
            _livrosRepository = livrosRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_livrosRepository.Listar());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] LivrosDTO livros)
        {

            if (String.IsNullOrWhiteSpace(livros.Titulo) || livros.IdGenero == 0)
                return BadRequest("É obrigatório que o livro tenha Título e Gênero");

            Livro novoLivro = new Livro();

            if (livros.Imagem != null && livros.Imagem.Length != 0)
            {
                var extensao = Path.GetExtension(livros.Imagem.FileName);
                var nomeArquivo = $"{Guid.NewGuid()}{extensao}";

                var pastaRelativa = "wwwroot/imagens";
                var caminhoPasta = Path.Combine(Directory.GetCurrentDirectory(), pastaRelativa);

                //Garante que a pasta exista
                if (!Directory.Exists(caminhoPasta))
                    Directory.CreateDirectory(caminhoPasta);

                var caminhoCompleto = Path.Combine(caminhoPasta, nomeArquivo);

                using (var stream = new FileStream(caminhoCompleto, FileMode.Create))
                {
                    await livros.Imagem.CopyToAsync(stream);
                }

                novoLivro.Imagem = nomeArquivo;
            }

            novoLivro.Titulo = livros.Titulo;
            novoLivro.Autor = livros.Autor;
            novoLivro.Ano = livros.Ano.Value;
            novoLivro.IdGenero = livros.IdGenero;

            try
            {
                _livrosRepository.Cadastrar(novoLivro);
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return BadRequest(
        ex.InnerException?.Message ??
        ex.Message
    );
            }
        }

        [HttpGet("BuscarPorId/{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                return Ok(_livrosRepository.BuscarPorId(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, LivrosDTO? livrosAtualizados)
        {

            var livro = _livrosRepository.BuscarPorId(id);
            if (livro == null)
                return NotFound("Livro não encontrado.");

            if (!string.IsNullOrWhiteSpace(livrosAtualizados.Titulo))
                livro.Titulo = livrosAtualizados.Titulo;

            if (!string.IsNullOrWhiteSpace(livrosAtualizados.Autor))
                livro.Autor = livrosAtualizados.Autor;

            if (livrosAtualizados.Ano.HasValue)
                livro.Ano = livrosAtualizados.Ano.Value;

            if (livro.IdGenero != livrosAtualizados.IdGenero &&
                livrosAtualizados.IdGenero != 0)
            {
                livro.IdGenero = livrosAtualizados.IdGenero;
            }


            if (livrosAtualizados.Imagem != null && livrosAtualizados.Imagem.Length != 0)
            {
                var pastaRelativa = "wwwroot/imagens";
                var caminhoPasta = Path.Combine(Directory.GetCurrentDirectory(), pastaRelativa);

                // Deleta arquivo antigo
                if (!String.IsNullOrEmpty(livro.Imagem))
                {

                    var caminhoAntigo = Path.Combine(caminhoPasta, livro.Imagem);

                    if (System.IO.File.Exists(caminhoAntigo))
                        System.IO.File.Delete(caminhoAntigo);
                }

                // Salva nova imagem
                var extensao = Path.GetExtension(livrosAtualizados.Imagem.FileName);
                var nomeArquivo = $"{Guid.NewGuid()}{extensao}";

                if (!Directory.Exists(caminhoPasta))
                    Directory.CreateDirectory(caminhoPasta);

                var caminhoCompleto = Path.Combine(caminhoPasta, nomeArquivo);
                using (var stream = new FileStream(caminhoCompleto, FileMode.Create))
                {
                    await livrosAtualizados.Imagem.CopyToAsync(stream);
                }

                livro.Imagem = nomeArquivo;
            }

            try
            {
                _livrosRepository.AtualizarIdUrl(id, livro);

                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public IActionResult PutBody(Livro livro)
        {
            try
            {
                _livrosRepository.AtualizarIdCorpo(livro);

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
            var livro = _livrosRepository.BuscarPorId(id);

            var pastaRelativa = "wwwroot/imagens";
            var caminhoPasta = Path.Combine(Directory.GetCurrentDirectory(), pastaRelativa);

            // Deleta arquivo
            if (!String.IsNullOrEmpty(livro.Imagem))
            {

                var caminho = Path.Combine(caminhoPasta, livro.Imagem);

                if (System.IO.File.Exists(caminho))
                    System.IO.File.Delete(caminho);
            }

            try
            {
                _livrosRepository.Deletar(id);

                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
