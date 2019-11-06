using System.Collections.Generic;
using System.Threading.Tasks;
using api_comil.Models;
using api_comil.Repositorios;
using Microsoft.AspNetCore.Mvc;

namespace api_comil.Controllers {
    [ApiController]
    [Route ("api/{controller}")]
    public class EventoController : ControllerBase {

        EventoRepositorio EventoRep = new EventoRepositorio ();

        [HttpGet]
        public async Task<ActionResult<List<Evento>>> Get () {
            return await EventoRep.Get ();
        }

        [HttpGet ("{id}")]
        public async Task<ActionResult<Evento>> Get (int id) {
            return await EventoRep.Get (id);
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
    }

}