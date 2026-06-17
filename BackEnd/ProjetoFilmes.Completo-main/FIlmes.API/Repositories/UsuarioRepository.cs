using Filmes.API.Interfaces;
using Filmes.API.Utils;
using FIlmes.API.BdContextFilmes;
using FIlmes.API.Models;

namespace Filmes.API.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly FilmesContext _context;

        public UsuarioRepository(FilmesContext context)
        {
            _context = context;
        }

        public Usuario BuscarPorEmailESenha(string email, string senha)
        {
            try
            {
                Usuario usuarioBuscado = _context.Usuarios.FirstOrDefault(u => u.Email == email)!;

                if (usuarioBuscado != null)
                {
                    bool confere = Criptografia.CompararHash(senha, usuarioBuscado.Senha!);

                    if (confere)
                    {
                        return usuarioBuscado;
                    }
                }
                return null!;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Usuario BuscarPorId(int id)
        {
            try
            {
                Usuario usuarioBuscado = _context.Usuarios.Find(id)!;

                if (usuarioBuscado != null)
                {
                    return usuarioBuscado;

                }
                return null!;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Cadastrar(Usuario novoUsuario)
        {
            try
            {

                novoUsuario.Senha = Criptografia.GerarHash(novoUsuario.Senha!);


                _context.Usuarios.Add(novoUsuario);


                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
