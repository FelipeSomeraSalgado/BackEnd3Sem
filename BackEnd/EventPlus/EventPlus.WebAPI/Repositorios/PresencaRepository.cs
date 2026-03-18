
using EventPlus.WebAPI.BdContextEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.WebAPI.Repositorios;

public class PresencaRepository : IPresencaRepository
{
    private readonly EventContext _context;
    public PresencaRepository(EventContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Método que alterna a situação da presença
    /// </summary>
    /// <param name="id">id da presenca a ser alterada</param>
    public void Atualizar(Guid id)
    {
        var presencaBuscada = _context.Presencas.Find(id);

        if (presencaBuscada != null)
        {
            presencaBuscada.Situacao = !presencaBuscada.Situacao;

            _context.SaveChanges();
        }
    }

    /// <summary>
    /// Método que busca uma presença por id
    /// </summary>
    /// <param name="id">id da presença a ser buscada</param>
    /// <returns>presença buscada</returns>
    public Presenca BuscarPorId(Guid id)
    {
        return _context.Presencas
            .Include(p => p.IdEventoNavigation)
               .ThenInclude(e => e!.IdInstituicaoNavigation)
            .FirstOrDefault(p => p.IdPresenca == id)!;
    }

    public void Deletar(Guid id)
    {
        var PresencaBuscado = _context.Presencas.Find(id);

        if (PresencaBuscado != null)
        {
            _context.Presencas.Remove(PresencaBuscado);
            _context.SaveChanges();
        }
    }

    public void Inscrever(Presenca presenca)
    {
        _context.Presencas.Add(presenca);
        _context.SaveChanges();
    }

    public List<Presenca> Listar()
    {
        return _context.Presencas.ToList();
    }

    /// <summary>
    /// Método que lista as presenças de um usuário especifico
    /// </summary>
    /// <param name="IdUsuario">id do usuário para filtragem</param>
    /// <returns>Lista de presenças de um usuário</returns>
    public List<Presenca> ListarMinhas(Guid IdUsuario)
    {
       return _context.Presencas
            .Include(p => p.IdEventoNavigation)
               .ThenInclude(e => e!.IdInstituicaoNavigation)
            .Where(p => p.IdUsuario == IdUsuario)
            .ToList();
    }
}
