using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using api_comil.Models;
using api_comil.Repositorios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api_comil.Controllers

{   [Route("api/[controller]")]
    [ApiController]
    public class ComunidadeController : ControllerBase{
    
    /// <summary>
    /// Método resposável por fazer busca de todas as comunidade no banco de dados
    /// </summary>
    /// <returns>Todas as comunidades cadastradas no banco de dados</returns>
    ComunidadeRepositorio repositorio = new ComunidadeRepositorio();
    
    [AllowAnonymous]
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
    [Authorize]   
    [HttpGet("{id}")]
    public async Task<ActionResult<Comunidade>> Get(int id)
    {
        Comunidade comunidadeRetornada = await repositorio.Get(id);
        
            if(comunidadeRetornada != null){

                return comunidadeRetornada;            
            }else{
                return NotFound();
            }
    }


    /// <summary>
    /// Método resposável por fazer alteração na comunidade encontrada através do ID ou nome de uma comunidade 
    /// </summary>
    /// <param name="id">Recebe ID da comunidade</param>
    /// <param name="comunidade">Recebe o objeto comunidade</param>
    /// <returns>Comunidade alterada</returns>
    [Authorize]
    [HttpPut("{id}")]
    public async Task<ActionResult<Comunidade>> Put(int id,Comunidade comunidade)
    {

         try
        {
            var usuarioId = HttpContext.User.Claims.First(c => c.Type == JwtRegisteredClaimNames.Jti).Value;
            
            if(comunidade.ResponsavelUsuarioId != int.Parse(usuarioId)) return Forbid();
            if(comunidade.DeletedoEm != null) return NotFound();   

            await repositorio.Put(comunidade);
         
        }catch (DbUpdateConcurrencyException){
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
    [Authorize]   
    [HttpDelete("{id}")]

   public async Task<ActionResult<Comunidade>> Delete(int id)
        { 

            try
            {
                var comunidadeRetornada = await repositorio.Get(id);
                var usuarioId = HttpContext.User.Claims.First(c => c.Type == JwtRegisteredClaimNames.Jti).Value;
                
                if(comunidadeRetornada.ResponsavelUsuarioId != int.Parse(usuarioId)) return Forbid();
                if(comunidadeRetornada.DeletedoEm != null) return NotFound();   
                
                if(comunidadeRetornada != null)
                {
                    comunidadeRetornada.DeletedoEm = DateTime.Now;
                    await repositorio.Delete(comunidadeRetornada);
                    return comunidadeRetornada;     
                
                }else{
                    return NotFound();               
                }
            }
            catch (System.Exception)
            {
                
                throw;
            }
           
        }


    /// <summary>
    /// Método responsável por cadastrar uma comunidade
    /// </summary>
    /// <param name="comunidade">Objeto comunidade</param>
    /// <returns>Comunidade cadastrada</returns>
    [Authorize]
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
   