using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModeladoCliente1.Shared.DTOs
{
    public class ClienteDto
    {
        public int ID { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string DNI { get; set; } = string.Empty;
        public string Gmail { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;

        public List<RegistroClienteDto> Registros { get; set; } = new List<RegistroClienteDto>();
    }
}
