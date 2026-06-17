using LivrosBD.Interfaces;
using LivrosBD.LivrosBDConnect;
using LivrosBD.Models;

namespace LivrosBD.Repositories
{
    public class GeneroRepositories : IGeneroRepository
    {
        private readonly LivrosBDContext _context;

        public GeneroRepositories(LivrosBDContext context)
        {
            _context = context;
        }

        public void AtualizarIdCorpo(Genero genero)
        {
            try
            {
                Genero generoBuscado = _context.Generos.Find
                    (genero.IdGenero)!;

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
                Genero generoBuscado = _context.Generos.Find
                     (id)!;
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
                Genero generoBuscado = _context.Generos.Find(id)!;
                if (generoBuscado != null)
                {
                    _context.Generos.Remove(generoBuscado);
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
                List<Genero> ListGeneros = _context.Generos.ToList();

                return ListGeneros;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
