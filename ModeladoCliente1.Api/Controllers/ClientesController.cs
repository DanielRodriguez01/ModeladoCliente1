using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ModeladoCliente1.Api.Interfaces;
using ModeladoCliente1.Shared.DTOs;
using Microsoft.AspNetCore.OutputCaching;

namespace ModeladoCliente1.Api.Controllers
{
    [ApiController]
    [Route("api/clientes")]
    [Authorize]
    public class ClientesController : ControllerBase
    {
        private const string CacheTag = "Clientes";

        private readonly IRepositorioCliente _repo;
        private readonly IOutputCacheStore _cacheStore;

        public ClientesController(
            IRepositorioCliente repo,
            IOutputCacheStore cacheStore)
        {
            _repo = repo;
            _cacheStore = cacheStore;
        }

        [HttpGet]
        [OutputCache(Duration = 60, Tags = new[] { CacheTag })]
        public async Task<ActionResult<List<ClienteDto>>> Get()
        {
            var lista = await _repo.ObtenerClientes();
            return Ok(lista);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClienteDto>> GetPorId(int id)
        {
            var cliente = await _repo.ObtenerClientePorId(id);

            if (cliente == null)
                return NotFound();

            return Ok(cliente);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ClienteDto>> Post([FromBody] CrearClienteDto dto)
        {
            var creado = await _repo.CrearCliente(dto);

            await _cacheStore.EvictByTagAsync(CacheTag, default);

            return CreatedAtAction(
                nameof(GetPorId),
                new { id = creado.ID },
                creado);
        }
    }
}