using LivrosBD.Interfaces;
using LivrosBD.LivrosBDConnect;
using LivrosBD.Models;
using static System.Net.WebRequestMethods;

namespace LivrosBD.Repositories
{
    public class LivrosRepositories : ILivrosRepository
    {
        private readonly LivrosBDContext _context;

        public LivrosRepositories(LivrosBDContext context)
        {
            _context = context;
        }

        public void AtualizarIdCorpo(Livro livro)
        {
            try
            {
                Livro livroBuscado = _context.Livros.Find(livro.IdLivro)!;
                if (livroBuscado != null)
                {
                    livroBuscado.Titulo = livro.Titulo;
                    livroBuscado.Autor = livro.Autor;
                    livroBuscado.Ano = livro.Ano;
                    livroBuscado.IdGenero = livro.IdGenero;
                    livroBuscado.Imagem = livro.Imagem;


                }
                    _context.Livros.Update(livroBuscado!);

                    _context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void AtualizarIdUrl(int id, Livro livro)
        {
            try
            {
                Livro livroBuscado = _context.Livros.Find(id)!;
                if (livroBuscado != null)
                {
                    livroBuscado.Titulo = livro.Titulo;
                    livroBuscado.Autor = livro.Autor;
                    livroBuscado.Ano = livro.Ano;
                    livroBuscado.IdGenero = livro.IdGenero;
                    livroBuscado.Imagem = livro.Imagem;

                }
                    _context.Livros.Update(livroBuscado!);
                    _context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

       

        public Livro BuscarPorId(int id)
        {
            try
            {
                Livro livroBuscado = _context.Livros.Find(id)!;
                return livroBuscado;
            }
            catch (Exception)
            {

                throw;
            }
        }

      
        

        public void Cadastrar(Livro novoLivro)
        {
            try
            {
             

                _context.Livros.Add(novoLivro);

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
                Livro livroBuscado = _context.Livros.Find(id)!;
                if (livroBuscado != null)
                {
                    _context.Livros.Remove(livroBuscado);
                }
                _context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }


        public List<Livro> Listar()
        {
            try
            {
                List<Livro> ListaLivros = _context.Livros.ToList();

                return ListaLivros;

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
