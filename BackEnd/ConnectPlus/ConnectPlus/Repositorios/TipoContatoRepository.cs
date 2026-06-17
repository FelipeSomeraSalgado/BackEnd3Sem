        using ConnectPlus.BdContextConnect;
using ConnectPlus.Interfaces;
using ConnectPlus.Models;

namespace ConnectPlus.Repositorios;

public class TipoContatoRepository : ITipoContatoRepository
{
    private readonly ConnectContext _context;


    public TipoContatoRepository(ConnectContext context)
    {
        _context = context;
    }

    public void Atualizar(Guid id, TipoContato TipoContato)
    {
        var tipoBuscado = _context.TipoContatos.Find(id);

        if (tipoBuscado != null)
        {
            tipoBuscado.Titulo = TipoContato.Titulo;

            _context.SaveChanges();
        }
    }

    public TipoContato BuscarPorId(Guid IdTipoContato)
    {
        return _context.TipoContatos.Find(IdTipoContato)!;
    }


    public void Cadastrar(TipoContato TipoContato)
    {
        _context.TipoContatos.Add(TipoContato);
        _context.SaveChanges();
    }

    public List<TipoContato> Listar()
    {
        return _context.TipoContatos.ToList();
    }

    public void Deletar(Guid id)
    {
        var tipoBuscado = _context.TipoContatos.Find(id);
        if (tipoBuscado != null)
        {
            _context.TipoContatos.Remove(tipoBuscado);
            _context.SaveChanges();
        }
    }
}
