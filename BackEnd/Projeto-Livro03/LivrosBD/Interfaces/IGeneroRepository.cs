using LivrosBD.Models;

namespace LivrosBD.Interfaces
{
    public interface IGeneroRepository
    {
        Genero BuscarPorId(int id);

        List<Genero> Listar();

        void Cadastrar(Genero novoGenero);

        void Deletar(int id);

        void AtualizarIdCorpo(Genero generoAtualizado);

        void AtualizarIdUrl(int id, Genero generoAtualizado);
    }
}
