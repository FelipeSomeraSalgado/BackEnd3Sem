using Filmes.WebAPI.BdContextFilme;
using Filmes.WebAPI.Interfaces;
using Filmes.WebAPI.Models;


namespace Filmes.WebAPI.Repositories;

public class FilmeRepository : IFilmeRepository
{

    private readonly FilmeContext _context;

    public FilmeRepository(FilmeContext context)
    {
        _context = context;
    }


    // Atualiza o filme pelo id e é chamado no controller
    public void AtualizarIdCorpo(Filme filmeAtualizado)
    {
        try
        {
            Filme filmeBuscado = _context.Filmes.Find(filmeAtualizado.IdFilme);

            if (filmeBuscado != null)
            {
                filmeBuscado.Titulo = filmeAtualizado.Titulo;
            }

            _context.Filmes.Update(filmeBuscado);
            _context.SaveChanges();

        }
        catch (Exception)
        {

            throw;
        }
    }

    public void AtualizarIdCorpo(Filme filmeAtualizado)
    {
        throw new NotImplementedException();
    }


    // Atualiza o filme pelo id e é chamado no controller
    public void AtualizarIdUrl(Guid Id, Filme filmeAtualizado)
    {
        try
        {
            Filme filmeBuscado = _context.Filmes.Find(Id.ToString())!;
            if (filmeBuscado != null)
            {
                filmeBuscado.Titulo = filmeAtualizado.Titulo;

                _context.Filmes.Update(filmeBuscado);
                _context.SaveChanges();
            }
        }
        catch (Exception)
        {

            throw;
        }
    }

    public void AtualizarIdUrl(Guid Id, Filme filmeAtualizado)
    {
        throw new NotImplementedException();
    }

    // Busca o filme pelo id e é chamado no controller
    public Filme BuscarPorId(Guid Id)
    {
        try
        {
            Filme filmeBucado = _context.Filmes.Find(Id.ToString())!;
            return filmeBucado;
        }
        catch (Exception)
        {
            throw;
        }
    }

    // Cadastra um novo filme e é chamado no controller
    public void Cadastrar(Filme novoFilme)
    {
        try
        {
            novoFilme.IdFilme = Guid.NewGuid().ToString();

            _context.Filmes.Add(novoFilme);
            _context.SaveChanges();
        }
        catch (Exception)
        {

            throw;
        }
    }

    public void Cadastrar(Filme novoFilme)
    {
        throw new NotImplementedException();
    }

    // Deleta um filme pelo id e é chamado no controller
    public void Deletar(Guid Id)
    {
        try
        {
            Filme filmeBuscado = _context.Filmes.Find(Id.ToString())!;

            if (filmeBuscado != null)
            {
                _context.Filmes.Remove(filmeBuscado);
            }
            _context.SaveChanges();
        }
        catch (Exception)
        {

            throw;
        }
    }

    // Lista todos os filmes e é chamado no controller
    public List<Filme> Listar()
    {
        try
        {
            List<Filme> ListaFilmes = _context.Filmes.ToList();

            return ListaFilmes;

        }
        catch (Exception)
        {

            throw;
        }
    }

    Filme IFilmeRepository.BuscarPorId(Guid Id)
    {
        throw new NotImplementedException();
    }

    List<Filme> IFilmeRepository.Listar()
    {
        throw new NotImplementedException();
    }
}
