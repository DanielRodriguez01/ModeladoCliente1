using System.Collections.Generic;
using System.Threading.Tasks;
using ModeladoCliente1.Shared.DTOs;

namespace ModeladoCliente1.Api.Interfaces
{
    public interface IRepositorioCliente
    {
        Task<List<ClienteDto>> ObtenerClientes(); //
        Task<ClienteDto?> ObtenerClientePorId(int id); //
        Task<ClienteDto> CrearCliente(CrearClienteDto dto); //
    }
}
