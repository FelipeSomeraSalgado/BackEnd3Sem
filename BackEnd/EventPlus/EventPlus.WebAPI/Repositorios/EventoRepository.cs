using EventPlus.WebAPI.BdContextEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.WebAPI.Repositorios;

public class EventoRepository : IEventoRepository
{
    private readonly EventContext _context;

    public EventoRepository(EventContext context)
    {
        _context = context; 
    }
    public void Atualizar(Guid id, Evento evento)
    {
        var EventoBuscado = _context.Eventos.Find(id);

        if (EventoBuscado != null)
        {
           EventoBuscado.Descricao = evento.Descricao;

            //O SaveChanges() detecta a mudança na propriedade "Titulo" automaticamente
            _context.SaveChanges();
        }
    }
    

    public Evento BuscarPorId(Guid id)
    {
        return _context.Eventos.Find(id)!;
    }

    public void Cadastrar(Evento evento)
    {
        _context.Eventos.Add(evento);
        _context.SaveChanges();
    }

    public void Deletar(Guid id)
    {
        var EventoBuscado = _context.Eventos.Find(id);

        if (EventoBuscado != null)
        {
            _context.Eventos.Remove(EventoBuscado);
            _context.SaveChanges();
        }
    }

    public List<Evento> Listar()
    {
        return _context.Eventos
            .OrderBy(evento => evento.IdEvento)
            .ToList(); 
    }
    /// <summary>
    /// Metodo que lista eventos filtrando pelas prensenças do usuário
    /// </summary>
    /// <param name="IdUsuario">Id do usuário para filtragem</param>
    /// <returns>Lista de eventos filtrados por usuário</returns>
    public List<Evento> ListarPorId(Guid IdUsuario)
    {
        return _context.Eventos
            .Include(e => e.Presencas)
            .Include(e => e.IdTipoEventoNavigation)
            .Include(e => e.IdInstituicaoNavigation)
            .Where(e => e.Presencas.Any(p => p.IdUsuario == IdUsuario && p.Situacao))
            .ToList();
    }

    /// <summary>
    /// Método que busca os próximos evento que irão acontecer
    /// </summary>
    /// <returns>Lista de próximos eventos</returns>
    public List<Evento> ListarProximos()
    {     
         return _context.Eventos
             .Include(e => e.IdTipoEventoNavigation)
             .Include(e => e.IdInstituicaoNavigation)
             .Where(e => e.DataEvento >= DateTime.Now)
             .OrderBy(e => e.DataEvento)
             .ToList();
    }
}
