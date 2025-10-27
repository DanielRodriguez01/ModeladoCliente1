using System;

namespace ModeladoCliente1.Api.Entidades
{
    public class RegistroCliente
    {
        public int IDRegistro { get; set; } // Clave primaria de mi tabla de Registro
        public int IDCliente { get; set; }  // Clave Forenea de mi tabla Cliente
        public DateTime FechaRegistro { get; set; }
        public string Descripcion { get; set; } = string.Empty;

        // Union de las claves
        public Cliente? Cliente { get; set; }
    }
}
