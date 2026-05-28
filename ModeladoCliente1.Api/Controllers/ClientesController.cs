using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ModeladoCliente1.Api.Interfaces;
using ModeladoCliente1.Shared.DTOs;
using Microsoft.AspNetCore.OutputCaching;

namespace ModeladoCliente1.Api.Controllers
{
    [ApiController]
    [Route("api/clientes")]
    [Authorize] // con este cambio cualquier usuario logueado puede entrar...
    public class ClientesController : ControllerBase
    {
        private readonly IRepositorioCliente _repo;

        public ClientesController(IRepositorioCliente repo)
        {
            _repo = repo;
        }

        [HttpGet]
        [OutputCache(Duration = 60)]
        public async Task<ActionResult<List<ClienteDto>>> Get()
        {
            var lista = await _repo.ObtenerClientes();
            return Ok(lista);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClienteDto>> GetPorId(int id)
        {
            var cliente = await _repo.ObtenerClientePorId(id);
            if (cliente == null) return NotFound();
            return Ok(cliente);
        }

        //  aqui solo los administradores puede crear clientes
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ClienteDto>> Post([FromBody] CrearClienteDto dto)
        {
            var creado = await _repo.CrearCliente(dto);
            return CreatedAtAction(nameof(GetPorId), new { id = creado.ID }, creado);
        }
    }
}