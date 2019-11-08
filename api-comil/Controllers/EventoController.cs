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


        /// <summary>
        /// Método de busca de um evento através de um Id
        /// </summary>
        /// <param name="id">Id do evento</param>
        /// <returns>Comunidade correspondente ao Id</returns>
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


        /// <summary>
        /// Método onde um administradoe aceita um evento
        /// </summary>
        /// <param name="idEvento">Id do evento</param>
        /// <param name="idResp"> Id do resposável pelo evento</param>
        /// <returns>Evento aprovado</returns>
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


        /// <summary>
        /// Método para atualização de dados de um evento
        /// </summary>
        /// <param name="id">Id do evento</param>
        /// <param name="evento">Objeto evento</param>
        /// <returns>Evento com dados atualizados</returns>

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



        /// <summary>
        /// Método onde um administrador recusa um evento
        /// </summary>
        /// <param name="idEvento">Id do evento</param>
        /// <param name="idResp">Id do responsável pelo evento</param>
        /// <returns>Evento rejeitado</returns>
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

        /// <summary>
        /// Método de busca de eventos do mês correspondente
        /// </summary>
        /// <returns>eventos do mês</returns>
        [HttpGet("Mes")]
         public async Task<ActionResult<List<Evento>>> Mouth(){
            return await EventoRep.Mounth();
         }


            ///adm
           /// <summary>
           /// Método de busca dos eventos aprovados pelo respectivo administrador
           /// </summary>
           /// <param name="id">Id do administrador</param>
           /// <returns>Eventos que o respectivo administrador aceitou</returns>
          [HttpGet("Aprovados/{id}")]
         public async Task<ActionResult<List<ResponsavelEventoTw>>> MyEventsAccept(int id){
            return await EventoRep.MyEventsAccept(id);
         }


           //ADM
           /// <summary>
           /// Método dos eventos recusados pelo respectivo administrador
           /// </summary>
           /// <param name="id">Id do administrador</param>
           /// <returns>Eventos recusados do respectivo administrador</returns>
          [HttpGet("Recusados/{id}")]
         public async Task<ActionResult<List<ResponsavelEventoTw>>> MyEventsReject(int id){
             
                return await EventoRep.MyEventsReject(id);
         }

          //ADM
          /// <summary>
          /// Método de busca dos eventos que estão pendentes
          /// </summary>
          /// <param name="id">Id do evento</param>
          /// <returns>Eventos pendentes</returns>
          [HttpGet("Pendentes/{id}")]
         public async Task<ActionResult<List<ResponsavelEventoTw>>> PendingMounth(int id){
             
                return await EventoRep.PendingMounth(id);
         }

            /// <summary>
            /// Método de busca de evento através de uma categoria expecífica
            /// </summary>
            /// <param name="id">Id da categoria</param>
            /// <returns>Eventos correspondentes à categoria</returns>
          [HttpGet("Categoria/{id}")]
         public async Task<ActionResult<List<Evento>>> EventByCategory(int id){
             
                return await EventoRep.EventByCategory(id);
         }

        /// <summary>
        /// Método de eventos pendentes do usuário
        /// </summary>
        /// <param name="id">Id do evento</param>
        /// <returns>Retorna os eventos pendenetes do usuário</returns>
        [HttpGet("PendentesUsuario/{id}")]
         public async Task<ActionResult<List<ResponsavelEventoTw>>> PendingUser(int id){
             
                return await EventoRep.PendingUser(id);
         }
        /// <summary>
        /// Método de busca dos eventos já realizados pelo usuário
        /// </summary>
        /// <param name="id">Id do evento</param>
        /// <returns>Eventos realizados pelo usuário</returns>
        [HttpGet("RealizadosUsuario/{id}")]
        public async Task<ActionResult<List<ResponsavelEventoTw>>> RealizeUser(int id){
             
                return await EventoRep.RealizeUser(id);
         }

         /// <summary>
        /// Método de busca dos eventos do usuário que foram aprovados
        /// </summary>
        /// <param name="id">Id do evento</param>
        /// <returns>Eventos do usuário que estão aprovados</returns>
        [HttpGet("AprovadosUsuario/{id}")]
        public async Task<ActionResult<List<ResponsavelEventoTw>>> ApprovedUser(int id){
             
                return await EventoRep.ApprovedUser(id);
         }

         //em vez de receber o id pega do token 
    }
}