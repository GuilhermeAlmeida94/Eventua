using System.Threading.Tasks;
using Eventua.API.Data;
using Eventua.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Eventua.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventoController : ControllerBase
    {
        private readonly DataContext _context;

        public EventoController(DataContext context)
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
        public async Task Post([FromBody] EventoModel evento)
        {
            _context.Eventos.Add(evento);
        await _context.SaveChangesAsync();
        }
    }
}
