using ConnectPlus.Models;

namespace ConnectPlus.Interfaces;

public interface IContatoRepository
{

    void Cadastrar(Contato contato);
    void Atualizar(Guid id, Contato contato);
    void Deletar(Guid id);
    List<Contato> Listar();
    Contato BuscarPorId(Guid IdContato);
}
