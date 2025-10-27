using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModeladoCliente1.Shared.DTOs
{
    public class CrearRegistroClienteDto
    {
        public int IDCliente { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string Descripcion { get; set; } = string.Empty;
    }
}
