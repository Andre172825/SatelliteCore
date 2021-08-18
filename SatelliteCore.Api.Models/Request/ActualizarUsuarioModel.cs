using System.ComponentModel.DataAnnotations;

namespace SatelliteCore.Api.Models.Request
{
    public class ActualizarUsuarioModel
    {
        [Required(ErrorMessage ="El nombre es obligatorio")]
        [MinLength(5, ErrorMessage = "El nombre debe tener mínimo 5 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El apellido paterno es obligatorio")]
        public string ApellidoPaterno { get; set; }

        [Required(ErrorMessage = "El apellido materno es obligatorio")]
        public string ApellidoMaterno { get; set; }

        [Required(ErrorMessage = "El nro del documento es obligatorio")]
        public string NroDocumento { get; set; }

        [Required(ErrorMessage = "El correo del documento es obligatorio")]
        [EmailAddress(ErrorMessage = "El correo no es válido")]
        public string Correo { get; set; }
    }
}
