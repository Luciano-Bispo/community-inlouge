using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_comil.Interfaces;
using api_comil.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api_comil.Repositorios
{
    public class ResponsavelEventoTwRepositorio : IResponsavelEventoTw
    {
        communityInLoungeContext db = new communityInLoungeContext();

        public async Task<ActionResult<ResponsavelEventoTw>> Delete(ResponsavelEventoTw responsavelEventoTW)
        {
            await db.SaveChangesAsync();
            return responsavelEventoTW;
        }

        public async Task<List<ResponsavelEventoTw>> Get()
        {
            List<ResponsavelEventoTw> ListaDeResponsavelEventoTw = await db.ResponsavelEventoTw.Where(w => w.DeletedoEm == null).ToListAsync();
            return ListaDeResponsavelEventoTw;
        }

        public async Task<ResponsavelEventoTw> Get(int id)
        {
            return await db.ResponsavelEventoTw.Where(w => w.DeletedoEm == null).SingleOrDefaultAsync(a => a.ResponsavelEventoTwId == id);
        }

        public async Task<ResponsavelEventoTw> Post(ResponsavelEventoTw responsavelEventoTw)
        {
            await db.ResponsavelEventoTw.AddAsync(responsavelEventoTw);
            await db.SaveChangesAsync();

            return responsavelEventoTw;
        }

        public async Task<ResponsavelEventoTw> Put(int id, ResponsavelEventoTw responsavelEventoTw)
        {
            db.Entry(responsavelEventoTw).State = EntityState.Modified;
            await db.SaveChangesAsync();

            return responsavelEventoTw;
        }
    }
}