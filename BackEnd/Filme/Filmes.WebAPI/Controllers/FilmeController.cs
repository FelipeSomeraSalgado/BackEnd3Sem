using Filmes.WebAPI.DTO;
using Filmes.WebAPI.Interfaces;
using Filmes.WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Filmes.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FilmeController : ControllerBase
{
    private readonly IFilmeRepository _filmeRepository;
    public FilmeController(IFilmeRepository filmeRepository)
    {
        _filmeRepository = filmeRepository;
    }

    //[Authorize]   
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

    [HttpGet("{id}")]
    public IActionResult GetById(Guid Id)
    {
        try
        {

            return Ok(_filmeRepository.BuscarPorId(Id));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }


    [HttpPost]
    public async Task<IActionResult> Post([FromForm]FilmeDTO novoFilme)
    {
        if (String.IsNullOrWhiteSpace(novoFilme.Titulo) && novoFilme.IdGenero != null)
        {
            return BadRequest("É Obrigatório que o filme tenha Nome e Gênero");
        }
        Filme filme = new Filme();

        if (novoFilme.Imagem != null && novoFilme.Imagem.Length > 0)
        {
            var extensao = Path.GetExtension(novoFilme.Imagem.FileName);
            var nomeArquivo = $"{Guid.NewGuid()}{extensao}";

            var pastaRelativa = "wwwroot/Imagens";
            var caminhoPasta = Path.Combine(Directory.GetCurrentDirectory(), pastaRelativa);

            //Garantir que a pasta existe 
            if (!Directory.Exists(caminhoPasta))
            {
                Directory.CreateDirectory(caminhoPasta);
            }
            var caminhoCompleto = Path.Combine(caminhoPasta, nomeArquivo);

            using (var stream = new FileStream(caminhoCompleto, FileMode.Create))
            {
                await novoFilme.Imagem.CopyToAsync(stream);
            }

            filme.Imagem = nomeArquivo;
        }

        filme.IdGenero = novoFilme.IdGenero.ToString();
        filme.Titulo = novoFilme.Titulo!    ;

        try
        {
            _filmeRepository.Cadastrar(filme);
            return StatusCode(201);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("{id}")]

    public async Task<IActionResult> Put(Guid id, FilmeDTO filme)
    {
        var filmeBuscado = _filmeRepository.BuscarPorId(id);
        if (filmeBuscado == null)
            return NotFound("Filme não encontrado! ");

        if (!String.IsNullOrWhiteSpace(filme.Titulo))
            filmeBuscado.Titulo = filme.Titulo;
        if (filme.IdGenero != null && filme.IdGenero.ToString() != filmeBuscado.IdGenero)
            filmeBuscado.IdGenero = filme.IdGenero.ToString();
        if (filme.Imagem != null && filme.Imagem.Length != 0)
        {
            var pastaRelativa = "wwwroot/imagens";
            var caminhoPasta = Path.Combine(Directory.GetCurrentDirectory(), pastaRelativa);

            //Deleta arquivo antigo
            if(!String.IsNullOrEmpty(filmeBuscado.Imagem))
            {                 
                var caminhoAntiga = Path.Combine(caminhoPasta, filmeBuscado.Imagem);
                if (System.IO.File.Exists(caminhoAntiga))
                    System.IO.File.Delete(caminhoAntiga);
            }
            //Salva a nova imagem
            var extensao = Path.GetExtension(filme.Imagem.FileName);
            var nomeArquivo = $"{Guid.NewGuid()}{extensao}";

            if (!Directory.Exists(caminhoPasta))
                Directory.CreateDirectory(caminhoPasta);

            var caminhoCompleto = Path.Combine(caminhoPasta, nomeArquivo);
            using (var stream = new FileStream(caminhoCompleto, FileMode.Create))
            {
                await filme.Imagem.CopyToAsync(stream);
            }

            filmeBuscado.Imagem = nomeArquivo;

        }


        try
        {
            _filmeRepository.AtualizarIdUrl(id, filmeBuscado);
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
    public IActionResult Delete(Guid id)
    {
        var filmeBuscado = _filmeRepository.BuscarPorId(id);
        if(filmeBuscado == null)
            return NotFound("Filme não encontrado!");

        var pastaRelativa = "wwwroot/imagens";
        var caminhoPasta = Path.Combine(Directory.GetCurrentDirectory(), pastaRelativa);

        // Deleta arquivo
        if (!String.IsNullOrEmpty(filmeBuscado.Imagem))
        {
            var caminho = Path.Combine(caminhoPasta, filmeBuscado.Imagem);
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
