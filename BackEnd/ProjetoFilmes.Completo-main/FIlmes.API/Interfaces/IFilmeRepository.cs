using FIlmes.API.Models;

namespace FIlmes.API.Interfaces
{
    public interface IFilmeRepository
    {
        void Cadastrar(Filme novoFilme);
        List<Filme> Listar();
        void AtualizarIdCorpo(Filme filme);
        void AtualizarIdUrl(int id, Filme filme);
        void Deletar(int id);
        Filme BuscarPorId(int id);
    }
}
