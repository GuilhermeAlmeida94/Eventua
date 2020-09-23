using System.Linq;
using System.Threading.Tasks;
using Eventua.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Eventua.Repository
{
    public class EventuaRepository : IEventuaRepository
    {
        private EventuaContext _context;

        public EventuaRepository(EventuaContext context)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public void DeleteRange<T>(T[] entity) where T : class
        {
            _context.RemoveRange(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Evento[]> GetAllEventoAsync(bool includePalestrante = false)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include(e => e.RedesSociais)
                .Include(e => e.Lotes);

            if (includePalestrante)
                query = query
                    .Include(e => e.PalestranteEventos)
                    .ThenInclude(pe => pe.Palestrante);

            query = query.OrderBy(e => e.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Evento> GetEventoAsyncById(int eventoId, bool includePalestrante = false)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include(e => e.RedesSociais)
                .Include(e => e.Lotes);

            if (includePalestrante)
                query = query
                    .Include(e => e.PalestranteEventos)
                    .ThenInclude(pe => pe.Palestrante);

            query = query
                .Where(e => e.Id == eventoId)
                .OrderBy(e => e.Id);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Evento[]> GetAllEventoAsyncByTema(string tema, bool includePalestrante = false)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include(e => e.RedesSociais)
                .Include(e => e.Lotes);

            if (includePalestrante)
                query = query
                    .Include(e => e.PalestranteEventos)
                    .ThenInclude(pe => pe.Palestrante);

            query = query
                .Where(e => e.Tema.ToLower().Contains(tema.ToLower()))
                .OrderByDescending(e => e.DataEvento);

            return await query.ToArrayAsync();
        }

        public async Task<Palestrante[]> GetAllPalestranteAsync(bool includeEvento = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
                .Include(p => p.RedesSociais);

            if (includeEvento)
                query = query
                    .Include(p => p.PalestranteEventos)
                    .ThenInclude(pe => pe.Evento);

            query = query.OrderBy(p => p.Nome);

            return await query.ToArrayAsync();
        }

        public async Task<Palestrante[]> GetAllPalestranteAsyncByTema(string nome, bool includeEvento = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
                .Include(p => p.RedesSociais);

            if (includeEvento)
                query = query
                    .Include(p => p.PalestranteEventos)
                    .ThenInclude(pe => pe.Evento);

            query = query
                .Where(e => e.Nome.ToLower().Contains(nome.ToLower()))
                .OrderBy(p => p.Nome);

            return await query.ToArrayAsync();
        }

        public async Task<Palestrante> GetPalestranteAsyncById(int palestranteId, bool includeEvento= false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
                .Include(p => p.RedesSociais);

            if (includeEvento)
                query = query
                    .Include(p => p.PalestranteEventos)
                    .ThenInclude(pe => pe.Evento);

            query = query
                .Where(e => e.Id == palestranteId)
                .OrderBy(p => p.Nome);

            return await query.FirstOrDefaultAsync();
        }
    }
}