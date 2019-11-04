using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using api_comil.Models;
using api_comil.Repositorios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api_comil.Controllers {
    [Route ("api/[controller]")]
    [ApiController]

    public class TipoUsuarioController : ControllerBase {
       TipoUsuarioRepositorio repositorio = new TipoUsuarioRepositorio();

       /// <summary>
       /// Método de Listar tipo de Usuário 
       /// </summary>
       /// <returns>Retorna o tipo de usuário</returns>
       
        [HttpGet]
        public async Task<ActionResult<List<TipoUsuario>>> Get()
         {
             var tUsuarioRetornado = repositorio.Get();
             if (tUsuarioRetornado == null)
             {
                 return NotFound();
             }
            return await tUsuarioRetornado;
        }

        /// <summary>
        /// Método para encontrar tipo de usuário(nível de acesso) pelo id.
        /// </summary>
        /// <param name="id">Recebe id pelo tipo de usuário</param>
        /// <returns>Retorna o tipo de usuário</returns>

        [HttpGet("{id}")]

        public async Task<ActionResult<TipoUsuario>> Get(int id) 
        {
            var tUsuarioRetornado = repositorio.Get(id);
            if (tUsuarioRetornado == null)
            {
                return NotFound();
            }
            return await tUsuarioRetornado;
        }

    }
}