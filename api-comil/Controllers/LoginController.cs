using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using api_comil.Models;
using api_comil.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace api_comil.Controllers
{
     [ApiController]
    [Route("api/{controller}")]
    public class LoginController : ControllerBase
    {
         IConfiguration configuration;

        communityInLoungeContext db = new communityInLoungeContext();   
    
        public LoginController(IConfiguration configuration){
            this.configuration = configuration;
        }


        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(LoginViewModel login){
        
           return Auth(login);
        }

      
        private string gerarToken(Usuario usr){

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:Key"]));  
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            //definindo claims / dados da sessão 
            var claims = new[]{
                new Claim(JwtRegisteredClaimNames.Jti, usr.UsuarioId.ToString()),
                new Claim(JwtRegisteredClaimNames.NameId, usr.Nome),
                new Claim(ClaimTypes.Role, usr.TipoUsuario.Titulo),
                new Claim(JwtRegisteredClaimNames.Sub, Guid.NewGuid().ToString()),
                new Claim("Roles", usr.TipoUsuario.Titulo)

            };

            var token = new JwtSecurityToken(configuration["Token:Issuer"], 
                configuration["Token:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(20),
                signingCredentials: credentials
            ); 

            return new JwtSecurityTokenHandler().WriteToken(token);
        } 
     
       
        private IActionResult Auth(LoginViewModel login){

             IActionResult res = Unauthorized();
                try
                {

                    var resp = db.Usuario.Include(t => t.TipoUsuario).FirstOrDefault(user => user.Email == login.Email && user.Senha == login.Senha);
                  

                            if (resp != null && resp.DeletedoEm == null)
                            {
                                var tokenString = gerarToken(resp);
                                var id = resp.UsuarioId;
                                res = Ok(new { token = tokenString, UsuarioId = id });
                            }

                            return res;
                }
                catch (Exception)
                {
                    
                    throw;
                }
        }



    }
}