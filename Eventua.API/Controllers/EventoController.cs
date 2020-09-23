using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
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

        [HttpPost("upload")]
        public async Task<IActionResult> upload()
        {
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("Resources","Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (file.Length > 0)
                {
                    var filename = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName;
                    var fullPath = Path.Combine(pathToSave, filename.Replace("\"", " ").Trim());

                    using(var stream  = new FileStream(fullPath, FileMode.Create)){
                        await file.CopyToAsync(stream);
                    }

                    return Ok();
                }
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }

            return BadRequest("Erro ao tentar realizar upload.");
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
                var idLotes = new List<int>();
                model.Lotes.ForEach(lote => idLotes.Add(lote.Id));

                var idRedesSociais = new List<int>();
                model.RedesSociais.ForEach(redeSocial => idRedesSociais.Add(redeSocial.Id));

                Evento evento = await _repository.GetEventoAsyncById(eventoId, false);
                if (evento == null) return NotFound();

                var lotesFaltantes = evento.Lotes
                    .Where(lote => !idLotes.Contains(lote.Id)).ToArray();
                if (lotesFaltantes.Count() > 0)
                    _repository.DeleteRange(lotesFaltantes);

                var redesSociaisFaltantes = evento.RedesSociais
                    .Where(redeSocial => !idRedesSociais.Contains(redeSocial.Id)).ToArray();
                if (redesSociaisFaltantes.Count() > 0)
                    _repository.DeleteRange(redesSociaisFaltantes);

                _mapper.Map(model, evento);
                
                _repository.Update(evento);

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
