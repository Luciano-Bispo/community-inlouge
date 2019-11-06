using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
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

        


        public async Task<Evento> Accept(Evento evento, int responsavel)
        {
            evento.StatusEvento = "Aprovado";
            db.Evento.Update(evento);

            db.ResponsavelEventoTw.Add(new ResponsavelEventoTw{ Evento = evento.EventoId, ResponsavelEvento = responsavel });
            Mensagem(evento.EmailContato);
            await db.SaveChangesAsync();
            return evento;
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
            .Include(i => i.Comunidade)
            .Where(w => w.StatusEvento == "Aprovado")
            .Where(w => w.DeletedoEm == null )
            .ToListAsync();

            foreach (var item in listEven)
            {
            item.Categoria.Evento = null;
            item.Comunidade.Evento = null;
            }

            return listEven;
        }


        public async Task<Evento> Get(int id)
        {
            var evento = await db.Evento
            .Include(i => i.Categoria)
            .Include(c => c.Comunidade)
            .Where(w => w.StatusEvento == "Aprovado")
            .Where(w => w.DeletedoEm == null )
            .FirstOrDefaultAsync(f => f.EventoId == id);

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

        public async Task<Evento> Reject(Evento evento, int idResponsavel)
        {
            evento.StatusEvento = "Recusado"; 
            db.Evento.Update(evento);

           Mensagem(evento.EmailContato);

            db.ResponsavelEventoTw.Add(new ResponsavelEventoTw{ Evento = evento.EventoId, ResponsavelEvento = idResponsavel });

            await db.SaveChangesAsync();
            return evento;
        }


        private void Mensagem (string email) {
            try {
                // Estancia da Classe de Mensagem
                MailMessage _mailMessage = new MailMessage ();
                // Remetente
                _mailMessage.From = new MailAddress (email);

                // Destinatario seta no metodo abaixo

                //Contrói o MailMessage
                _mailMessage.CC.Add(email);
                _mailMessage.Subject = "TESTELIGHT CODE XP";
                _mailMessage.IsBodyHtml = true;
                _mailMessage.Body = "<b>Olá Tudo bem ??</b><p>Teste Parágrafo</p>";

                //CONFIGURAÇÃO COM PORTA
                SmtpClient _smtpClient = new SmtpClient("smtp.gmail.com", Convert.ToInt32("587"));

                //CONFIGURAÇÃO SEM PORTA
                // SmtpClient _smtpClient = new SmtpClient(UtilRsource.ConfigSmtp);

                // Credencial para envio por SMTP Seguro (Quando o servidor exige autenticação);
                _smtpClient.UseDefaultCredentials = false;

                _smtpClient.Credentials = new NetworkCredential("communitythoughtworks@gmail.com", "tw.123456");

                _smtpClient.EnableSsl = true;

                _smtpClient.Send (_mailMessage);

            } catch (Exception ex) {
                throw ex;
            }
        }



        public Task<ActionResult<List<Evento>>> Search(string filtro)
        {
            throw new System.NotImplementedException();
        }

        public Task<ActionResult<List<Evento>>> ThougthworksEvents()
        {
            throw new System.NotImplementedException();
        }

        public async Task<ActionResult<Evento>> Update(Evento evento)
        {
        if(evento.StatusEvento == "Aprovado")
        {
            Evento eventoRetornado = await db.Evento.FindAsync(evento.EventoId);
            eventoRetornado.Nome = evento.Nome;
            db.Evento.Update(eventoRetornado);
        }else
        {
           db.Entry(evento).State = EntityState.Modified;
        }
        return evento;
        }

        public Task<ActionResult<List<Evento>>> Week()
        {
            throw new System.NotImplementedException();
        }

    }
}