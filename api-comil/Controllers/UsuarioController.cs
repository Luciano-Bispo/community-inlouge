using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using api_comil.Models;
using api_comil.Repositorios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api_comil.Controllers
{
    [ApiController]
    [Route("api/{controller}")]
    public class UsuarioController : ControllerBase
    {
        UsuarioRepositorio usuariorep = new UsuarioRepositorio();

        [Authorize]
         [HttpGet("{id}")]
         public async Task<ActionResult<Usuario>> Get(int id){

             Usuario userReturn = await usuariorep.Get(id);
            if(userReturn == null){
                return NotFound();
            }
            return userReturn;
        }

        [AllowAnonymous]
        [HttpPost]
         public async Task<ActionResult<Usuario>> Post(Usuario usuario){
             
            if(!ModelState.IsValid){
                return BadRequest();
            }

            return await usuariorep.Post(usuario);
        }

        [Authorize]
         [HttpDelete("{id}")]
         public async Task<ActionResult<Usuario>> Delete(int id){
            var user = await usuariorep.Get(id);
            if(user != null)
            { 
                user.DeletedoEm = DateTime.Now;
                await usuariorep.Delete(user);
            } else 
            {
                BadRequest();
            }

            return user;
        }

        [Authorize]
         [HttpPut]
         public async Task<ActionResult<Usuario>> Put(Usuario usuario){
             
            if(!ModelState.IsValid){
                return BadRequest();
            }

            return await usuariorep.Put(usuario);
        }
    }
}