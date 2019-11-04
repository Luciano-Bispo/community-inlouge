using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using api_comil.Models;
using api_comil.Repositorios;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api_comil.Controllers

{   [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    
    public class ComunidadeController : ControllerBase{
    
    /// <summary>
    /// Método resposável por fazer busca de todas as comunidade no banco de dados
    /// </summary>
    /// <returns>Todas as comunidades cadastradas no banco de dados</returns>
     ComunidadeRepositorio repositorio = new ComunidadeRepositorio();
    [EnableCors]
    [HttpGet]
    public async Task<ActionResult<List<Comunidade>>> Get()
    {
        List<Comunidade> ListaDeComunidades = await repositorio.Get();
        if(ListaDeComunidades == null){
            return NotFound();
        }
        return ListaDeComunidades;
    }
    /// <summary>
    /// Método resposável por fazer busca de uma comunidade expecífica através do parâmetro - ID
    /// </summary>
    /// <param name="id">Recebe i ID da comunidade</param>
    /// <returns>Comundade correspondente ao ID digitado</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<Comunidade>> Get(int id)
    {
        Comunidade comunidadeRetornada = await repositorio.Get(id);
        if(comunidadeRetornada == null){
            return NotFound();
        } 
        return comunidadeRetornada;
    }
    /// <summary>
    /// Método resposável por fazer alteração na comunidade encontrada através do ID ou nome de uma comunidade 
    /// </summary>
    /// <param name="id">Recebe ID da comunidade</param>
    /// <param name="comunidade">Recebe o objeto comunidade</param>
    /// <returns>Comunidade alterada</returns>
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
    /// <summary>
    /// Método resposável por deletar uma comunidade, mudando seu status deixando-a desabilitada
    /// </summary>
    /// <param name="id">Id da comunidade</param>
    /// <returns>Comunidade que foi desativada do sistema</returns>
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
        /// <summary>
        /// Método responsável por cadastrar uma comunidade
        /// </summary>
        /// <param name="comunidade">Objeto comunidade</param>
        /// <returns>Comunidade cadastrada</returns>

    [HttpPost]
    public async Task<ActionResult<Comunidade>> Post(Comunidade comunidade)
    {       
        try
           {
               if (!ModelState.IsValid)
                {
                    await repositorio.Post(comunidade);
                }
           }
           catch (System.Exception)
           {
               throw;
           }

            return comunidade;
    }
    }
}
   