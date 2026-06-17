using LivrosBD.Interfaces;
using LivrosBD.LivrosBDConnect;
using LivrosBD.Models;
using LivrosBD.Utils;

namespace LivrosBD.Repositories
{
    public class UsuarioRepositories : IUsuarioRepository
    {
        private readonly LivrosBDContext _context;

        public UsuarioRepositories(LivrosBDContext context)
        {
            _context = context;
        }

        public Usuario BuscarPorEmailESenha(string email, string Senha)
        {
            try
            {
                Usuario usuarioBuscado = _context.Usuarios.FirstOrDefault
                    (u => u.Email == email)!;

                if (usuarioBuscado != null)
                {
                    bool confere = Criptografia.CompararHash
                        (Senha, usuarioBuscado.Senha);

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

                if(usuarioBuscado != null)
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
