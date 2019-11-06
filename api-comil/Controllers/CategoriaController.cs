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
    public class CategoriaController : ControllerBase
    {
        CategoriaRepositorio repositorio = new CategoriaRepositorio();


        [Authorize(Roles ="Administrador")]
        [HttpGet]

        public async Task<ActionResult<List<Categoria>>> Get()
        {
            List<Categoria> ListaDeCategorias = await repositorio.Get();
            if (ListaDeCategorias == null)
            {
                return NotFound();
            }
            return ListaDeCategorias;
        }


        [Authorize(Roles ="Administrador")]
        [HttpGet("{id}")]

        public async Task<ActionResult<Categoria>> Get(int id)
        {
            Categoria categoriaRetornada = await repositorio.Get(id);
            if (categoriaRetornada == null)
            {
                return NotFound();
            }
            return categoriaRetornada;
        }


        [Authorize(Roles ="Administrador")]
        [HttpDelete("{id}")]

        public async Task<ActionResult<Categoria>> Delete(int id)
        {
            var categoriaRetornada = await repositorio.Get(id);

            if (categoriaRetornada != null)
            {
                categoriaRetornada.DeletedoEm = DateTime.Now;
                await repositorio.Delete(categoriaRetornada);
            }
            else
            {
                BadRequest();
            }
            return categoriaRetornada;
        }


        [Authorize(Roles ="Administrador")]
        [HttpPost]
        public async Task<ActionResult<Categoria>> Post(Categoria categoria)
        {
            try
            {
                var categoriaNome = repositorio.Get(categoria);
                if (categoriaNome != null)
                {
                    await repositorio.Post(categoria);
                    return categoria;
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
                throw;
            }



        }
    }
}