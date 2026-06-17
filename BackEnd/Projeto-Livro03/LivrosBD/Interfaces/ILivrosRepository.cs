
using LivrosBD.Models;

namespace LivrosBD.Interfaces
{
    public interface ILivrosRepository
    {
        Livro BuscarPorId(int id);

        List<Livro> Listar();

        void Cadastrar(Livro novoLivro);

        void AtualizarIdCorpo(Livro livroAtualizado);

        void AtualizarIdUrl(int id, Livro livroAtualizado);

        void Deletar(int id);
    }
}
