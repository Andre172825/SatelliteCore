using System.ComponentModel.DataAnnotations;

namespace SatelliteCore.Api.Models.Request
{
    public struct AuthRequestModel
    {
        //[Required(ErrorMessage = "El usuario es obligatorio")]
        public string Usuario { get; set; }


        //[Required(ErrorMessage = "La contraseña es obligatoria")]
        public string Clave { get; set; }
    }
}
