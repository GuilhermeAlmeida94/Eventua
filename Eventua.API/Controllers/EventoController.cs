using System.Threading.Tasks;
using Eventua.Domain.Models;
using Eventua.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Eventua.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventoController : ControllerBase
    {
        private readonly EventuaContext _context;

        public EventoController(EventuaContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var results = await _context.Eventos.ToListAsync();
                return Ok(results);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }
        }

        [HttpPost]
        public async Task Post([FromBody] Evento evento)
        {
            _context.Eventos.Add(evento);
        await _context.SaveChangesAsync();
        }

        [HttpPut]
        public async Task Put([FromBody] Evento evento)
        {
            _context.Eventos.Update(evento);
        await _context.SaveChangesAsync();
        }
    }
}
