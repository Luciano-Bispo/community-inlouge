using System.Collections.Generic;
using System.Threading.Tasks;
using api_comil.Models;
using api_comil.Repositorios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api_comil.Controllers
{
    [ApiController]
    [Route("api/{controller}")]
    public class EventoController : ControllerBase
    {

        EventoRepositorio EventoRep = new EventoRepositorio();

        [HttpGet]
        public async Task<ActionResult<List<Evento>>> Get(){
         return await EventoRep.Get();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Evento>> Get(int id){
            return await EventoRep.Get(id);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Evento evento)
        {
        if(id != evento.EventoId) return BadRequest();
        if(evento.DeletedoEm != null) return NotFound();   
                

        try        
        {
            Evento eventoValido = EventoRep.Get(id).Result;
            if(eventoValido == null)
            {
            return NotFound();
            }
            else
            {  
            await EventoRep.Update(evento);
            }
            
        }
        catch (DbUpdateConcurrencyException)
        {
        throw;
        }
        return NoContent();

     
    }

    }

}