using System.Collections.Generic;
using System.Threading.Tasks;
using api_comil.Models;
using api_comil.Repositorios;
using Microsoft.AspNetCore.Mvc;

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
    }

}