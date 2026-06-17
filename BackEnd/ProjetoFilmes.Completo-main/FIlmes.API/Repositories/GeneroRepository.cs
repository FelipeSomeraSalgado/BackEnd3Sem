using FIlmes.API.BdContextFilmes;
using FIlmes.API.Interfaces;
using FIlmes.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Filmes.API.Repositories
{
    public class GeneroRepository : IGeneroRepository
    {
        private readonly FilmesContext _context;
        public GeneroRepository(FilmesContext context)
        {
            _context = context;
        }

        public void AtualizarIdCorpo(Genero genero)
        {
            try
            {
                Genero generoBuscado = _context.Generos.Find(genero.IdGenero)!;

                if (generoBuscado != null)
                {
                    generoBuscado.Nome = genero.Nome;
                }

                _context.Generos.Update(generoBuscado!);

                _context.SaveChanges();

            }
            catch (Exception)
            {
                throw;
            }
        }

        public void AtualizarIdUrl(int id, Genero genero)
        {
            try
            {
                Genero generoBuscado = _context.Generos.Find(id)!;

                if (generoBuscado != null)
                {
                    generoBuscado.Nome = genero.Nome;
                }

                _context.Generos.Update(generoBuscado!);

                _context.SaveChanges();

            }
            catch (Exception)
            {
                throw;
            }
        }

        public Genero BuscarPorId(int id)
        {
            try
            {
                Genero generoBuscado = _context.Generos.Find(id)!;

                return generoBuscado;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Cadastrar(Genero novoGenero)
        {
            try
            {
                _context.Generos.Add(novoGenero);

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
                Genero generoBucado = _context.Generos.Find(id)!;

                if (generoBucado != null)
                {
                    _context.Generos.Remove(generoBucado);
                }
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Genero> Listar()
        {
            try
            {
                List<Genero> listaGeneros = _context.Generos.ToList();

                return listaGeneros;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
