using System.Collections.Generic;
using System.Threading.Tasks;
using api_comil.Models;
using Microsoft.AspNetCore.Mvc;

namespace api_comil.Interfaces
{
    public interface IResponsavelEventoTw
    {
        Task<List<ResponsavelEventoTw>> Get();
        Task<ResponsavelEventoTw> Get(int id);
        Task<ResponsavelEventoTw> Post(ResponsavelEventoTw responsavelEventoTw);
        Task<ResponsavelEventoTw> Put(int id, ResponsavelEventoTw responsavelEventoTw);
        Task<ActionResult<ResponsavelEventoTw>> Delete(ResponsavelEventoTw responsavelEventoTW);
    }
}