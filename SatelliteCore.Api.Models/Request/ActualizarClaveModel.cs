
using System.ComponentModel.DataAnnotations;

namespace SatelliteCore.Api.Models.Request
{
    public struct ActualizarClaveModel
    {
        [Required]
        public int IdUsuario { get; set; }
        [Required]
        public string NroDocumento { get; set; }
        [Required]
        public string Clave { get; set; }
        [Required]
        public bool ExigirCambioClave { get; set; }
    }
}
