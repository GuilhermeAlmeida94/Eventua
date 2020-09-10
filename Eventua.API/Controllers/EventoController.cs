using System.Threading.Tasks;
using Eventua.Domain.Models;
using Eventua.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Eventua.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventoController : ControllerBase
    {
        private readonly IEventuaRepository _repository;

        public EventoController(IEventuaRepository repository)
        {
            this._repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var results = await _repository.GetAllEventoAsync(true);
                return Ok(results);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }
        }

        [HttpGet("eventoId")]
        public async Task<IActionResult> Get(int eventoId)
        {
            try
            {
                var results = await _repository.GetEventoAsyncById(eventoId, true);
                return Ok(results);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Evento evento)
        {
            try{
                _repository.Add(evento);

                if (await _repository.SaveChangesAsync())
                    return Created($"evento/{evento.Id}", evento);
            }
            catch (System.Exception){
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }

            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Put(int eventoId, Evento evento)
        {
            try{
                Evento eventoOld = await _repository.GetEventoAsyncById(eventoId, false);
                if (eventoOld == null)
                    return NotFound();

            _repository.Update(evento);

            if (await _repository.SaveChangesAsync())
                return Created($"evento/{evento.Id}", evento);
            }
            catch (System.Exception){
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }

            return BadRequest();
        }

        [HttpDelete]
         public async Task<IActionResult> Delete(int eventoId)
         {
             try{
                 Evento evento = await _repository.GetEventoAsyncById(eventoId, false);
                 if (evento == null)
                     return NotFound();

             _repository.Delete(evento);

             if (await _repository.SaveChangesAsync())
                 return Ok();
             }
             catch (System.Exception){
                 return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
             }

             return BadRequest();
         }
    }
}
