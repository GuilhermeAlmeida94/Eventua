using Eventua.Domain.Models;
using Microsoft.EntityFrameworkCore;
 
 namespace Eventua.Repository
{
     public class EventuaContext : DbContext
     {
         public EventuaContext(DbContextOptions<EventuaContext> options) : base (options) {}
         public DbSet<Evento> Eventos { get; set; }
         public DbSet<Palestrante> Palestrantes { get; set; }
         public DbSet<PalestranteEvento> PalestranteEventos { get; set; }
         public DbSet<Lote> Lotes { get; set; }
         public DbSet<RedeSocial> RedeSociais { get; set; }

         protected override void OnModelCreating(ModelBuilder modelBuilder)
         {
             modelBuilder.Entity<PalestranteEvento>()
                 .HasKey(pe => new { pe.EventoId, pe.PalestranteId });
         }
     }
}