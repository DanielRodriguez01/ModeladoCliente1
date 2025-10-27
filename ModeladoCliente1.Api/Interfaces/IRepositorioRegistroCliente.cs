using System.Collections.Generic;
using System.Threading.Tasks;
using ModeladoCliente1.Shared.DTOs;

namespace ModeladoCliente1.Api.Interfaces
{
    public interface IRepositorioRegistroCliente
    {
        Task<List<RegistroClienteDto>> ObtenerTodosLosRegistros();
        Task<RegistroClienteDto> CrearRegistro(CrearRegistroClienteDto dto);
    }
}
