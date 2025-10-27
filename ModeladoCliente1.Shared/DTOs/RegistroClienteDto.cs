using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModeladoCliente1.Shared.DTOs
{
    public class RegistroClienteDto
    {
        public int IDRegistro { get; set; }
        public int IDCliente { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string Descripcion { get; set; } = string.Empty;

    }
}
