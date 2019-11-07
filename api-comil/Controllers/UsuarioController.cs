using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text.RegularExpressions;
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

            Usuario usuarioEmail = await usuariorep.VerificarEmail(usuario.Email);
            Regex expressaoRegex = new Regex(@"\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}");

            if(usuarioEmail != null){
            
                return BadRequest("Esse email já está cadastrado.");
            }
            if(!ModelState.IsValid && expressaoRegex.IsMatch(usuario.Email))
            {
                return BadRequest();
            }
        
        if (ValidaEnderecoEmail(usuario.Email)){

            return await usuariorep.Post(usuario);
        }else{
            return BadRequest();
        }
            
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

        public bool ValidaEnderecoEmail(string enderecoEmail)
        {
            try
            {
                //define a expressão regular para validar o email
                string texto_Validar = enderecoEmail;
                Regex expressaoRegex = new Regex(@"\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}");

                // testa o email com a expressão
                if (expressaoRegex.IsMatch(texto_Validar))
                {
                    // o email é valido
                    return true;
                }
                else
                {
                    // o email é inválido
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
   
   
    }
}