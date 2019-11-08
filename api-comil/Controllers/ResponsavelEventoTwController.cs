using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using api_comil.Models;
using api_comil.Repositorios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



namespace api_comil.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ResponsavelEventoTwController : ControllerBase
    {
        ResponsavelEventoTwRepositorio repositorio = new ResponsavelEventoTwRepositorio();
        communityInLoungeContext db = new communityInLoungeContext();

        /// <summary>
        /// Método Get, é para fazer a requisição HTTP ResponsavelEventoTw onde transmite a informação identificada na URL.
        /// </summary>
        /// <returns>Está retornando o ListaDeResponsavelEventoTw</returns>

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<ResponsavelEventoTw>>> Get()
        {
            List<ResponsavelEventoTw> ListaDeResponsavelEventoTw = await repositorio.Get();
            if (ListaDeResponsavelEventoTw == null)
            {
                return NotFound();
            }
            return ListaDeResponsavelEventoTw;
        }

        /// <summary>
        /// Método Get(id) é para fazer a requisição HTTP ResponsavelEventoTw pelo Id, onde transmite a informação identificada na URL.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Está retornando o responsavelEventoTwRetornado</returns>

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponsavelEventoTw>> Get(int id)
        {
            ResponsavelEventoTw responsavelEventoTwRetornado = await repositorio.Get(id);
            if (responsavelEventoTwRetornado == null)
            {
                return NotFound();
            }
            return responsavelEventoTwRetornado;
        }

        /// <summary>
        /// Método Put é utilizado para criar ou atualizar uma informação identificada pela URL. Por exemplo: ResponsavelEventoTw.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="responsavelEventoTw"></param>
        /// <returns>Esta retornando o return NoContent</returns>
        
        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<ResponsavelEventoTw>> Put(int id, ResponsavelEventoTw responsavelEventoTw)
        {
            if (id != responsavelEventoTw.ResponsavelEventoTwId)
            {
                return BadRequest();
            }// fim  if
            db.Entry(responsavelEventoTw).State = EntityState.Modified;
            {
                try
                {
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    var responsavelEventoTwValido = db.ResponsavelEventoTw.FindAsync(id);
                    if (responsavelEventoTwValido == null)
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
        }

        /// <summary>
        /// Método post, utilizado para criação de um novo dado no banco de dados.
        /// </summary>
        /// <param name="responsavelEventoTw"></param>
        /// <returns>Está retornando o valor BadRequest</returns>
        
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<ResponsavelEventoTw>> Post(ResponsavelEventoTw responsavelEventoTw)
        {
            try
            {
                var evento = await db.Evento.FindAsync(responsavelEventoTw.Evento);
                var usuario = await db.Usuario.FindAsync(responsavelEventoTw.Evento);
                if (evento == null || usuario == null)
                {
                    return NotFound();
                }
                else
                {

                    await repositorio.Post(responsavelEventoTw);
                    return responsavelEventoTw;
                }


            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
                throw;
            }
        }
        /// <summary>
        /// O método delete, serve para deletar dados cadastrados no banco de dados.
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Está retornando o responsavelEventoTwRetornado</returns>
        
        [Authorize]
        [HttpDelete("{id}")]

        public async Task<ActionResult<ResponsavelEventoTw>> Delete(int id)
        {
            var responsavelEventoTwRetornado = await repositorio.Get(id);

            if (responsavelEventoTwRetornado != null)
            {
                responsavelEventoTwRetornado.DeletedoEm = DateTime.Now;
                await repositorio.Delete(responsavelEventoTwRetornado);

            }
            else
            {
                BadRequest();
            }
            return responsavelEventoTwRetornado;
        }





    }
}