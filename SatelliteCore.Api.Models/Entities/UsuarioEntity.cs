using System;

namespace SatelliteCore.Api.Models.Entities
{
    public class UsuarioEntity
    {
        public int CodUsuario { get; set; }
        public string Nombres { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string TipoDocumento { get; set; }
        public string NroDocumento { get; set; }
        public string Sexo { get; set; }
        public string Nacionalidad { get; set; }
        public string Correo { get; set; }
        public string Estado { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Telefono { get; set; }
        public bool FlagCambioClave { get; set; }
        public DateTime? FechaVencimientoClave { get; set; }
    }
}
