using ConnectPlus.BdContextConnect;
using ConnectPlus.Interfaces;
using ConnectPlus.Models;

namespace ConnectPlus.Repositorios;

public class ContatoRepository : IContatoRepository
{
    private readonly ConnectContext _context;

    public ContatoRepository(ConnectContext context)
    {
        _context = context;
    }

    public void Atualizar(Guid id, Contato contato)
    {
        var contatoBuscado = _context.Contatos.Find(id);

        if (contatoBuscado != null)
        {
            contatoBuscado.Nome = contato.Nome;
            contatoBuscado.FormaContato = contato.FormaContato;
            contatoBuscado.Imagem = contato.Imagem;
            contatoBuscado.IdTipoContato = contato.IdTipoContato; 

            _context.SaveChanges();
        }
    }

    public Contato BuscarPorId(Guid IdContato)
    {
        return _context.Contatos.Find(IdContato)!;
    }

    public void Cadastrar(Contato contato)
        {
            _context.Contatos.Add(contato);
            _context.SaveChanges();
    }

    public List<Contato> Listar()
    {
        return _context.Contatos.ToList();
    }

    public void Deletar(Guid id)
    {
        var ContatoBuscado = _context.Contatos.Find(id);

        if (ContatoBuscado != null)
        {
            _context.Contatos.Remove(ContatoBuscado);
            _context.SaveChanges();
        }
    }
}
