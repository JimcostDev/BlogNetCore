using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlogNetCore.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage = "El nombre es requerido")]
        public string Nombre { get; set; }

        public string Direccion { get; set; }

        [Required(ErrorMessage = "La ciudad es requerida")]
        public string Ciudad { get; set; }

        [Required(ErrorMessage = "El país es requerido")]
        public string Pais { get; set; }
    }
}
