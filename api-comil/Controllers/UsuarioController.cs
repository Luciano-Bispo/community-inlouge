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
        /// <summary>
        /// Método de busca de usuário através do Id
        /// </summary>
        /// <param name="id">Id do usuário</param>
        /// <returns>Usuário correspondente ao Id</returns>
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> Get(int id){
        

             Usuario userReturn = await usuariorep.Get(id);
            if(userReturn == null){
                return NotFound();
            }
            return userReturn;
        }
        /// <summary>
        ///  Método de cadastro de usuário
        /// </summary>
        /// <param name="usuario">Objeto Usuário</param>
        /// <returns>Usuario cadastrado</returns>
        [AllowAnonymous]
        [HttpPost]
         public async Task<ActionResult<Usuario>> Post(Usuario usuario){

            var valid = ValidaEnderecoEmail(usuario.Email);
            var exist = usuariorep.ExistEmail(usuario.Email);
            
            if( exist != null){
            
                return BadRequest("Esse email já está cadastrado.");
            }

            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
        
            if (valid){
                return await usuariorep.Post(usuario);
            }else{
                return BadRequest();
            }
            
        }
        /// <summary>
        /// Método para deletar um usuário
        /// </summary>
        /// <param name="id">Id do usuário</param>
        /// <returns>Retorna usuário deletado</returns>
        [Authorize]
         [HttpDelete("{id}")]
         public async Task<ActionResult<Usuario>> Delete(int id){
            
            var user = await usuariorep.Get(id);
            var usuarioId = HttpContext.User.Claims.First(c => c.Type == JwtRegisteredClaimNames.Jti).Value;
                    
            if(user.UsuarioId != int.Parse(usuarioId)) return Forbid();
            
            if(user != null)
            { 
                user.DeletedoEm = DateTime.Now;
                await usuariorep.Delete(user);
                return user;

            } else {
               return BadRequest();
            }
        }

        /// <summary>
        /// Método para atualização de dados do usuário
        /// </summary>
        /// <param name="usuario">Objeto Usuário</param>
        /// <returns>Usuário alterado</returns>
        [Authorize]
         [HttpPut]
         public async Task<ActionResult<Usuario>> Put(Usuario usuario){
            
            var usuarioId = HttpContext.User.Claims.First(c => c.Type == JwtRegisteredClaimNames.Jti).Value;

            if(usuario.UsuarioId != int.Parse(usuarioId)) return Forbid();

            if(!ModelState.IsValid){
                return BadRequest();
            }

            return await usuariorep.Put(usuario);
        }

        private bool ValidaEnderecoEmail(string enderecoEmail)
        {
            try
            {
                Regex expressaoRegex = new Regex(@"\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}");
                return expressaoRegex.IsMatch(enderecoEmail);
            }
            catch (Exception)
            {
                throw;
            }
        }
   
   
    }
}