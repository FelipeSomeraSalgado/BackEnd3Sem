using LivrosBD.Models;

namespace LivrosBD.Interfaces
{
    public interface IUsuarioRepository
    {
        void Cadastrar(Usuario novoUsuario);

        Usuario BuscarPorId(int id);

        Usuario BuscarPorEmailESenha(string email, string senha);
    }
}
