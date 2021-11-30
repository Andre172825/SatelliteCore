using System.ComponentModel.DataAnnotations;

namespace SatelliteCore.Api.Models.Request
{
    public struct ValidacionRutaDataModel
    {
        [Required(ErrorMessage = "El campo usuario es obligatorio")]
        public int CodUsuario { get; set; }

        [Required(ErrorMessage = "La ruta es obligatoria")]
        public string OpcionMenu { get; set; }
    }
}
