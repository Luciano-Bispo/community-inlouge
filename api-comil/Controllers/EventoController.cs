using System.Collections.Generic;
using System.Threading.Tasks;
using api_comil.Models;
using api_comil.Repositorios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api_comil.Controllers {
    [ApiController]
    [Route ("api/{controller}")]
    public class EventoController : ControllerBase {

        EventoRepositorio EventoRep = new EventoRepositorio();
        UsuarioRepositorio UsuarioRep = new UsuarioRepositorio();
        communityInLoungeContext db = new communityInLoungeContext();

        [HttpGet]
        public async Task<ActionResult<List<Evento>>> Get () {
            return await EventoRep.Get ();
        }

        /// <summary>
        /// Método de cadastrar Evento 
        /// </summary>
        /// <returns>Retorna evento cadastrado</returns>

        [HttpPost]
        public async Task<ActionResult<Evento>> Post (Evento evento) {

            try {
                if (!ModelState.IsValid) {

                    return NotFound();

                } else {
                    var eRtornado = await EventoRep.Post(evento);
                    if (eRtornado == null)
                    {
                        return NotFound();
                    }else{
                        return eRtornado;
                    }
                }
            } catch (System.Exception) {

                throw;
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Evento>> Get(int id){
           try
           {
            var evento = await EventoRep.Get(id);

            if (evento == null){
                return NotFound();
            } 
            
            return evento;
               
           }
           catch (System.Exception)
           {
               
               throw;
           }
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost("aceppt/{idEvento}/{idResp}")]
        public async Task<ActionResult<Evento>> Accept(int idEvento, int idResp){
            
           var responsavel = await db.Usuario.FindAsync(idResp);
          //  var responsavel = await UsuarioRep.Get(idResp);
           
           var evento = await db.Evento.FindAsync(idEvento);
          //  var evento = await EventoRep.Get(idEvento);

           if (responsavel == null && evento == null){

                return BadRequest();
           }else{

                return await EventoRep.Accept(evento, responsavel.UsuarioId);
           }
            
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


       [Authorize(Roles = "Administrador")]
       [HttpPost("rejetc/{idEvento}/{idResp}")]
        public async Task<ActionResult<Evento>> Reject(int idEvento, int idResp){

            var responsavel = await db.Usuario.FindAsync(idResp);
         // var responsavel = await UsuarioRep.Get(idResp);
           
           var evento = await db.Evento.FindAsync(idEvento);
         // var evento = await EventoRep.Get(idEvento);

           if (responsavel == null && evento == null){

                return BadRequest();
           }else{

                return await EventoRep.Reject(evento, responsavel.UsuarioId);
           }

           }
            


            
    }
}