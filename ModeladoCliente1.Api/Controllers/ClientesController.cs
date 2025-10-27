using Microsoft.AspNetCore.Mvc;
using ModeladoCliente1.Api.Interfaces;
using ModeladoCliente1.Shared.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ModeladoCliente1.Api.Controllers
{
    [ApiController]
    [Route("api/clientes")]
    public class ClientesController : ControllerBase
    {
        private readonly IRepositorioCliente _repo;
        public ClientesController(IRepositorioCliente repo)
        {
            _repo = repo;
        }

        // GET: api/clientes
        [HttpGet]
        public async Task<ActionResult<List<ClienteDto>>> Get()
        {
            var lista = await _repo.ObtenerClientes();
            return Ok(lista);
        }

        // GET: api/clientes/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ClienteDto>> GetPorId(int id)
        {
            var cliente = await _repo.ObtenerClientePorId(id);
            if (cliente == null) return NotFound();
            return Ok(cliente);
        }

        // POST: api/clientes
        [HttpPost]
        public async Task<ActionResult<ClienteDto>> Post([FromBody] CrearClienteDto dto)
        {
            var creado = await _repo.CrearCliente(dto);
            return CreatedAtAction(nameof(GetPorId), new { id = creado.ID }, creado);
        }
    }
}
