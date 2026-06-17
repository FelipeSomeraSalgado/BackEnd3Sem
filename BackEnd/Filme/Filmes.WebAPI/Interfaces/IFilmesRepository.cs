using Filmes.WebAPI.Models;


namespace Filmes.WebAPI.Interfaces;

public interface IFilmeRepository
{
    Filme BuscarPorId(Guid Id);
    List<Filme> Listar();
    void Cadastrar(Filme novoFilme);
    void Deletar(Guid Id);
    void AtualizarIdCorpo(Filme filmeAtualizado);
    void AtualizarIdUrl(Guid Id, Filme filmeAtualizado);

}
