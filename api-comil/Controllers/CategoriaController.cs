using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using api_comil.Models;
using api_comil.Repositorios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api_comil.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        CategoriaRepositorio repositorio = new CategoriaRepositorio();

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

        [HttpGet("{id}")]

        public async Task<ActionResult<Categoria>> Get(int id)
        {
            Categoria categoriaRetornada = await repositorio.Get(id);
            if(categoriaRetornada == null){
                return NotFound();
            }
            return categoriaRetornada;
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult<Categoria>> Delete(int id)
        {
            var categoriaRetornada = await repositorio.Get(id);

            if(categoriaRetornada != null)
            {
                categoriaRetornada.DeletedoEm = DateTime.Now;
                await repositorio.Delete(categoriaRetornada);
            }else
            {
                BadRequest();
            }
            return categoriaRetornada;
        }
        [HttpPost]
        public async Task<ActionResult<Categoria>> Post(Categoria categoria)
        {
            try
            {
                await repositorio.Post(categoria);
                return categoria;
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
                throw;
            }
        }
    }
}