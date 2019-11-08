using System.Collections.Generic;
using System.Threading.Tasks;
using api_comil.Models;

namespace api_comil.Interfaces
{
    public interface ISalaRepositorio
    {
        Task<List<Sala>> Get();

        Task<Sala> Get(int id);
    }
}