using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; 

namespace ModeladoCliente1.Api.Entidades
{
    public class Cliente
    {
        public int ID { get; set; }

        
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; } = string.Empty;

        
        [Required(ErrorMessage = "El apellido es obligatorio")]
        public string Apellido { get; set; } = string.Empty;

        
        [Required(ErrorMessage = "El DNI es obligatorio")]
        [StringLength(8, MinimumLength = 7, ErrorMessage = "El DNI debe tener entre 7 y 8 caracteres")]
        public string DNI { get; set; } = string.Empty;

        
        [Required(ErrorMessage = "El Gmail es obligatorio")]
        [EmailAddress(ErrorMessage = "Ingrese un correo válido")]
        public string Gmail { get; set; } = string.Empty;

        
        [Required(ErrorMessage = "El teléfono es obligatorio")]
        [MinLength(8, ErrorMessage = "El teléfono debe tener al menos 8 números")]
        public string Telefono { get; set; } = string.Empty;

        public List<RegistroCliente> Registros { get; set; } = new List<RegistroCliente>();
    }
}