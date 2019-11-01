using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using api_comil.Models;
using api_comil.Repositorios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api_comil.Controllers

{   [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ComunidadeController : ControllerBase{
    

     ComunidadeRepositorio repositorio = new ComunidadeRepositorio();

    [HttpGet]
    public async Task<ActionResult<List<Comunidade>>> Get()
    {
        List<Comunidade> ListaDeComunidades = await repositorio.Get();
        if(ListaDeComunidades == null){
            return NotFound();
        }
        return ListaDeComunidades;
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<Comunidade>> Get(int id)
    {
        Comunidade comunidadeRetornada = await repositorio.Get(id);
        if(comunidadeRetornada == null){
            return NotFound();
        } 
        return comunidadeRetornada;
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Comunidade>> Put(int id,Comunidade comunidade)
    {
        if(id != comunidade.ComunidadeId) return BadRequest();
        if(comunidade.DeletedoEm != null) return NotFound();   
      
        try

        
        {
            await repositorio.Put(comunidade);
        }
        catch (DbUpdateConcurrencyException)
        {
            var comunidadeValida = repositorio.Get(id);
            if(comunidadeValida == null)
            {
                return NotFound();
            }
            else
            {
                throw;
            }

        }
        return NoContent();
    }
    [HttpDelete("{id}")]

   public async Task<ActionResult<Comunidade>> Delete(int id)
        { 
            var comunidadeRetornada = await repositorio.Get(id);

            if(comunidadeRetornada != null)
            {
                comunidadeRetornada.DeletedoEm = DateTime.Now;
                await repositorio.Delete(comunidadeRetornada);
               
            }else
            {
                BadRequest();               
            }
            return comunidadeRetornada;
        }
    }
}
   