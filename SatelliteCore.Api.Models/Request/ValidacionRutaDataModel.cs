using System.ComponentModel.DataAnnotations;

namespace SatelliteCore.Api.Models.Request
{
    public struct ValidacionRutaDataModel
    {
        [Required(ErrorMessage = "El campo usuario es obligatorio")]
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "Los datos del usuario es obligatorio")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "La ruta es obligatoria")]
        public string OpcionMenu { get; set; }
    }
}
