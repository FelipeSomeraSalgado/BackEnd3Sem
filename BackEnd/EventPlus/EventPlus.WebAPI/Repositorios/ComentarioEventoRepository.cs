using EventPlus.WebAPI.BdContextEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace EventPlus.WebAPI.Repositorios;

public class ComentarioEventoRepository : IComentarioEventoRepository
{
  private readonly EventContext _context;

    public ComentarioEventoRepository(EventContext context)
    {
        _context = context;
    }

    
    public void Atualizar(Guid id, ComentarioEvento comentarioEvento)
    {
        var comentarioEventoBuscado = _context.ComentarioEventos.Find(id);
        if (comentarioEventoBuscado != null)
        {
            comentarioEventoBuscado.Descricao = comentarioEvento.Descricao;
            
            _context.SaveChanges();
        }
    }

    public ComentarioEvento BuscarPorIdUsuario(Guid idUsuario, Guid idEvento)
    {
        return _context.ComentarioEventos.FirstOrDefault(c => c.IdUsuario == idUsuario && c.IdEvento == idEvento);
    }

    public void Cadastrar(ComentarioEvento comentarioEvento)
    {
        _context.ComentarioEventos.Add(comentarioEvento);
        _context.SaveChanges();
    }

    public void Deletar(Guid idcomentarioEvento)
    {
        var comentarioEventoBuscado = _context.ComentarioEventos.Find(idcomentarioEvento);

        if (comentarioEventoBuscado != null)
        {
            _context.ComentarioEventos.Remove(comentarioEventoBuscado);
            _context.SaveChanges();
        }
    }

    public List<ComentarioEvento> Listar(Guid IdEvento)
    {
        return _context.ComentarioEventos.ToList();
    }

    public List<ComentarioEvento> ListarSomenteExibe(Guid idEvento)
    {
        return _context.ComentarioEventos.Where(c => c.IdEvento == idEvento && c.Exibe).ToList();
       
    }
}
