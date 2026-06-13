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
    public class RepositorioCliente : IRepositorioCliente
    {
        private readonly ApplicationDbContext _context; // Es la instancia del DbContext sirve de puente con la base de datos
        public RepositorioCliente(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ClienteDto>> ObtenerClientes()
        {
            Console.WriteLine("******** CONSULTANDO CLIENTES EN SQL SERVER ********");

            var clientes = await _context.Clientes
                .Include(c => c.Registros)
                .ToListAsync();

            return clientes.Select(c => new ClienteDto
            {
                ID = c.ID,
                Nombre = c.Nombre,
                Apellido = c.Apellido,
                DNI = c.DNI,
                Gmail = c.Gmail,
                Telefono = c.Telefono,
                Registros = c.Registros.Select(r => new RegistroClienteDto
                {
                    IDRegistro = r.IDRegistro,
                    IDCliente = r.IDCliente,
                    FechaRegistro = r.FechaRegistro,
                    Descripcion = r.Descripcion
                }).ToList()
            }).ToList();
        }

        public async Task<ClienteDto?> ObtenerClientePorId(int id)
        {
            var c = await _context.Clientes.Include(x => x.Registros).FirstOrDefaultAsync(x => x.ID == id);
            if (c == null) return null;

            return new ClienteDto
            {
                ID = c.ID,
                Nombre = c.Nombre,
                Apellido = c.Apellido,
                DNI = c.DNI,
                Gmail = c.Gmail,
                Telefono = c.Telefono,
                Registros = c.Registros.Select(r => new RegistroClienteDto
                {
                    IDRegistro = r.IDRegistro,
                    IDCliente = r.IDCliente,
                    FechaRegistro = r.FechaRegistro,
                    Descripcion = r.Descripcion
                }).ToList()
            };
        }

        public async Task<ClienteDto> CrearCliente(CrearClienteDto dto)
        {
            var entidad = new Cliente
            {
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                DNI = dto.DNI,
                Gmail = dto.Gmail,
                Telefono = dto.Telefono
            };

            _context.Clientes.Add(entidad);
            await _context.SaveChangesAsync();

            // Retornamos DTO con ID asignado por DB
            return new ClienteDto
            {
                ID = entidad.ID,
                Nombre = entidad.Nombre,
                Apellido = entidad.Apellido,
                DNI = entidad.DNI,
                Gmail = entidad.Gmail,
                Telefono = entidad.Telefono,
                Registros = new List<RegistroClienteDto>()
            };
        }
    }
}

