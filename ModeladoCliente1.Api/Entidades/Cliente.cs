using System.Collections.Generic;

namespace ModeladoCliente1.Api.Entidades
{
    public class Cliente
    {
        // Propiedades de Cliente
        public int ID { get; set; } // Clave primaria de mi tabla cliente

        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string DNI { get; set; } = string.Empty;
        public string Gmail { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;


        // Lista de registro con List
        public List<RegistroCliente> Registros { get; set; } = new List<RegistroCliente>();
    }
}
