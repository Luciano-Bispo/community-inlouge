using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_comil.Interfaces;
using api_comil.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace api_comil.Repositorios
{
    public class EventoRepositorio : IEvento
    {
        communityInLoungeContext db = new communityInLoungeContext();

        public Task<ActionResult> Accept(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<ActionResult<List<Evento>>> ApprovedUser()
        {
            throw new System.NotImplementedException();
        }

        public Task<ActionResult> Delete()
        {
            throw new System.NotImplementedException();
        }

        public Task<ActionResult<List<Evento>>> EventByCategory(string cat)
        {
            throw new System.NotImplementedException();
        }

        public async Task<ActionResult<List<Evento>>> Get()
        {

             var listEven = await db.Evento
            .Include(i => i.Categoria)
            .Include(i => i.Comunidade).ToListAsync();

            foreach (var item in listEven)
            {
            item.Categoria.Evento = null;
            item.Comunidade.Evento = null;
            }

            return listEven;
        }

        public async Task<ActionResult<Evento>> Get(int id)
        {
            var evento = await db.Evento
             .Include(i => i.Categoria)
            .Include(c => c.Comunidade)
            .FirstOrDefaultAsync(f => f.EventoId == id);

            evento.Categoria.Evento = null;
            evento.Comunidade.Evento = null;
            
            return evento;
        }

        public Task<ActionResult<List<Evento>>> Mounth()
        {
            throw new System.NotImplementedException();
        }

        public Task<ActionResult<List<Evento>>> MyEventsAccept()
        {
            throw new System.NotImplementedException();
        }

        public Task<ActionResult<List<Evento>>> MyEventsReject()
        {
            throw new System.NotImplementedException();
        }

        public Task<ActionResult<List<Evento>>> PendingMounth()
        {
            throw new System.NotImplementedException();
        }

        public Task<ActionResult<List<Evento>>> PendingUser()
        {
            throw new System.NotImplementedException();
        }

        public Task<ActionResult<List<Evento>>> PendingWeek()
        {
            throw new System.NotImplementedException();
        }

        public async Task<ActionResult<Evento>> Post(Evento evento)
        {
            Categoria categoria = await db.Categoria.Where(c => c.CategoriaId == evento.CategoriaId).FirstOrDefaultAsync();
            Sala sala = await db.Sala.Where(s => s.SalaId == evento.SalaId).FirstOrDefaultAsync();
            Comunidade comunidade = await db.Comunidade.Where(c => c.ComunidadeId == evento.ComunidadeId).FirstOrDefaultAsync();

            if(categoria != null &&  sala != null && comunidade != null)
            {
                   db.Add(evento);
                  await db.SaveChangesAsync(); 
            }
        
           return null;
        }

        public Task<ActionResult<List<Evento>>> RealizeUser()
        {
            throw new System.NotImplementedException();
        }

        public Task<ActionResult> Reject(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<ActionResult<List<Evento>>> Search(string filtro)
        {
            throw new System.NotImplementedException();
        }

        public Task<ActionResult<List<Evento>>> ThougthworksEvents()
        {
            throw new System.NotImplementedException();
        }

        public Task<ActionResult<Evento>> update()
        {
            throw new System.NotImplementedException();
        }

        public Task<ActionResult<List<Evento>>> Week()
        {
            throw new System.NotImplementedException();
        }
    }
}