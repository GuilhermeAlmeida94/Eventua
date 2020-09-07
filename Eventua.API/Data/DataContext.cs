using Eventua.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Eventua.API.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base (options) {}
        public DbSet<EventoModel> Eventos { get; set; }
    }
}