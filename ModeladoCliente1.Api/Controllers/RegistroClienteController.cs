using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ModeladoCliente1.Api.Interfaces;
using ModeladoCliente1.Shared.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ModeladoCliente1.Api.Controllers
{
    [ApiController]
    [Route("api/RegistroCliente")] 
    public class RegistroClienteController : ControllerBase
    {
        private readonly IRepositorioRegistroCliente _repo;
        public RegistroClienteController(IRepositorioRegistroCliente repo)
        {
            _repo = repo;
        }

        
        [HttpGet]
        public async Task<ActionResult<List<RegistroClienteDto>>> Get()
        {
            var lista = await _repo.ObtenerTodosLosRegistros();
            return Ok(lista);
        }

        [HttpPost]
        public async Task<ActionResult<RegistroClienteDto>> Post([FromBody] CrearRegistroClienteDto dto)
        {
            var creado = await _repo.CrearRegistro(dto);
            return CreatedAtAction(nameof(Get), creado);
        }
    }
}
