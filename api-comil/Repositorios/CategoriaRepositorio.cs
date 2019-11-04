using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_comil.Interfaces;
using api_comil.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api_comil.Repositorios
{
    public class CategoriaRepositorio : ICategoria
    {
        communityInLoungeContext db = new communityInLoungeContext();
        public async Task<Categoria> Get(int id)
        {
            return await db.Categoria.Where(w => w.DeletedoEm == null).SingleOrDefaultAsync(a => a.CategoriaId == id);
        }

        public async Task<ActionResult<Categoria>> Delete(Categoria categoria)
        {
            await db.SaveChangesAsync();
            return categoria;
        }

        public async Task<List<Categoria>> Get()
        {
            List<Categoria> ListaDeCategoria = await db.Categoria.Where(w => w.DeletedoEm == null).ToListAsync();
            return ListaDeCategoria;
        }

        public async Task<Categoria> Post(Categoria categoria)
        {
            await db.Categoria.AddAsync(categoria);
            await db.SaveChangesAsync();
            return categoria;
        }
    }
}