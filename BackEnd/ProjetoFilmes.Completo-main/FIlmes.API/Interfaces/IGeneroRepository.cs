using FIlmes.API.Models;

namespace FIlmes.API.Interfaces
{
    public interface IGeneroRepository
    {
        void Cadastrar(Genero novoGenero);
        List<Genero> Listar();
        void AtualizarIdCorpo(Genero genero);
        void AtualizarIdUrl(int id, Genero genero);
        void Deletar(int id);
        Genero BuscarPorId(int id);
    }
}
