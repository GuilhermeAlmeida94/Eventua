using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Eventua.API.DTOs;
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
        private readonly IMapper _mapper;

        public EventoController(IEventuaRepository repository, IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var eventos = await _repository.GetAllEventoAsync(true);

                var results = _mapper.Map<IEnumerable<EventoDTO>>(eventos);

                return Ok(results);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }
        }

        [HttpGet("{eventoId}")]
        public async Task<IActionResult> Get(int eventoId)
        {
            try
            {
                var evento = await _repository.GetEventoAsyncById(eventoId, true);

                var result =  _mapper.Map<EventoDTO>(evento);

                return Ok(result);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(EventoDTO model)
        {
            try{
                var evento = _mapper.Map<Evento>(model);

                _repository.Add(evento);

                if (await _repository.SaveChangesAsync()){
                    model = _mapper.Map<EventoDTO>(evento);
                    return Created($"evento/{model.Id}", model);
                }
            }
            catch (System.Exception){
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }

            return BadRequest();
        }

        [HttpPut("{eventoId}")]
        public async Task<IActionResult> Put(int eventoId, EventoDTO model)
        {
            try{
                Evento evento = await _repository.GetEventoAsyncById(eventoId, false);
                if (evento == null) return NotFound();

            _mapper.Map(model, evento);
            
            _repository.Update(evento);

            if (await _repository.SaveChangesAsync())
                    model = _mapper.Map<EventoDTO>(evento);
                    return Created($"evento/{model.Id}", model);
            }
            catch (System.Exception){
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }

            return BadRequest();
        }

        [HttpDelete("{eventoId}")]
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
