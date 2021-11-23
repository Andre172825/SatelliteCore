using System.ComponentModel.DataAnnotations;

namespace SatelliteCore.Api.Models.Request
{
    public class AuthRequestModel
    {
        [Required(ErrorMessage = "El usuario es obligatorio")]
        //[EmailAddress(ErrorMessage = "Formato de correo no válido")]
        public string Correo { get; set; }
        [Required(ErrorMessage ="La contraseña es obligatoria")]
        public string Clave { get; set; }
    }
}
