using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_comil.Interfaces;
using api_comil.Models;
using Microsoft.EntityFrameworkCore;

namespace api_comil.Repositorios
{
    public class SalaRepositorio : ISalaRepositorio

    {
        communityInLoungeContext db = new communityInLoungeContext();


        public async Task<List<Sala>> Get()
        {
            List<Sala> ListaDeSala = await db.Sala.Where(w => w.DeletedoEm == null).ToListAsync();
            return ListaDeSala;
        }


        public async Task<Sala> Get(int id)
        {
            return await db.Sala.FindAsync(id);
        }
    }
}