using FIlmes.API.BdContextFilmes;
using FIlmes.API.Interfaces;
using FIlmes.API.Models;

namespace Filmes.API.Repositories
{
    public class FilmeRepository : IFilmeRepository
    {
        private readonly FilmesContext _context;
        public FilmeRepository(FilmesContext context)
        {
            _context = context;
        }
        public void AtualizarIdCorpo(Filme filme)
        {
            try
            {
                Filme filmeBuscado = _context.Filmes.Find(filme.IdFilme)!;

                if (filmeBuscado != null)
                {
                    filmeBuscado.Titulo = filme.Titulo;
                    filmeBuscado.IdGenero = filme.IdGenero;
                }
                _context.Filmes.Update(filmeBuscado!);

                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void AtualizarIdUrl(int id, Filme filme)
        {
            try
            {
                Filme filmeBuscado = _context.Filmes.Find(id)!;

                if (filmeBuscado != null)
                {
                    filmeBuscado.Titulo = filme.Titulo;
                    filmeBuscado.IdGenero = filme.IdGenero;
                }

                _context.Filmes.Update(filmeBuscado!);

                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Filme BuscarPorId(int id)
        {
            try
            {
                Filme filmeBuscado = _context.Filmes.Find(id)!;

                return filmeBuscado;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Cadastrar(Filme novoFilme)
        {
            try
            {
                _context.Filmes.Add(novoFilme);

                _context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Deletar(int id)
        {
            try
            {
                Filme filmeBuscado = _context.Filmes.Find(id)!;

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

        public List<Filme> Listar()
        {
            try
            {
                List<Filme> listaFilmes = _context.Filmes.ToList();

                return listaFilmes;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
