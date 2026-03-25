using ConnectPlus.Models;

namespace ConnectPlus.Interfaces;

public interface ITipoContatoRepository
{
    void Cadastrar(TipoContato TipoContato);
    void Atualizar(Guid id, TipoContato TipoContato);
    void Deletar(Guid id);
    List<TipoContato> Listar();
    TipoContato BuscarPorId(Guid IdTipoContato);
}
