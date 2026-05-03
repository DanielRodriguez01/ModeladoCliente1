using System;
using System.ComponentModel.DataAnnotations; 

namespace ModeladoCliente1.Api.Entidades
{
    public class RegistroCliente
    {
        public int IDRegistro { get; set; }

        public int IDCliente { get; set; }

        
        [Required(ErrorMessage = "La fecha es obligatoria")]
        public DateTime FechaRegistro { get; set; }

        
        [Required(ErrorMessage = "La descripción es obligatoria")]
        [MinLength(5, ErrorMessage = "La descripción debe tener al menos 5 caracteres")]
        public string Descripcion { get; set; } = string.Empty;

        public Cliente? Cliente { get; set; }
    }
}
    
