using System.ComponentModel.DataAnnotations;

namespace SatelliteCore.Api.Models.Request
{
    public struct DatoUsuarioBasico
    {
        [Required(ErrorMessage = "El campo usuario es obligatorio")]
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "El dato del usuario es obligatorio")]
        public string Apellido { get; set; }
    }
}
