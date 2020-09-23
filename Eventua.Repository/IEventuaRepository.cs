using System.Threading.Tasks;
using Eventua.Domain.Models;

namespace Eventua.Repository
{
    public interface IEventuaRepository
    {
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        void DeleteRange<T>(T[] entity) where T : class;
        Task<bool> SaveChangesAsync();

        Task<Evento[]> GetAllEventoAsync(bool includePalestrante);
        Task<Evento[]> GetAllEventoAsyncByTema(string tema, bool includePalestrante);
        Task<Evento> GetEventoAsyncById(int eventoId, bool includePalestrante);

        Task<Palestrante[]> GetAllPalestranteAsync(bool includeEvento);
        Task<Palestrante[]> GetAllPalestranteAsyncByTema(string nome, bool includeEvento);
        Task<Palestrante> GetPalestranteAsyncById(int palestranteId, bool includeEvento);
    }
}