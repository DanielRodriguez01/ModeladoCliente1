using Microsoft.EntityFrameworkCore;
using ModeladoCliente1.Api.Data;
using ModeladoCliente1.Api.Entidades;
using ModeladoCliente1.Api.Interfaces;
using ModeladoCliente1.Shared.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModeladoCliente1.Api.Repositorios
{
    public class RepositorioRegistroCliente : IRepositorioRegistroCliente
    {
        private readonly ApplicationDbContext _context;
        public RepositorioRegistroCliente(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<RegistroClienteDto>> ObtenerTodosLosRegistros()
        {
            var registros = await _context.RegistroClientes.ToListAsync();
            return registros.Select(r => new RegistroClienteDto
            {
                IDRegistro = r.IDRegistro,
                IDCliente = r.IDCliente,
                FechaRegistro = r.FechaRegistro,
                Descripcion = r.Descripcion
            }).ToList();
        }

        public async Task<RegistroClienteDto> CrearRegistro(CrearRegistroClienteDto dto)
        {
            var entidad = new RegistroCliente
            {
                IDCliente = dto.IDCliente,
                FechaRegistro = dto.FechaRegistro,
                Descripcion = dto.Descripcion
            };

            _context.RegistroClientes.Add(entidad);
            await _context.SaveChangesAsync();

            return new RegistroClienteDto
            {
                IDRegistro = entidad.IDRegistro,
                IDCliente = entidad.IDCliente,
                FechaRegistro = entidad.FechaRegistro,
                Descripcion = entidad.Descripcion
            };
        }
    }
}
