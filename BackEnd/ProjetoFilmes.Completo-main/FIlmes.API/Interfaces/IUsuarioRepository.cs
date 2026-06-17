using FIlmes.API.Models;

namespace Filmes.API.Interfaces
{
    public interface IUsuarioRepository
    {
        void Cadastrar(Usuario novoUsuario);
        Usuario BuscarPorId(int id);
        Usuario BuscarPorEmailESenha(string email, string senha);
    }
}
