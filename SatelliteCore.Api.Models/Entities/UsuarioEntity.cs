using System;

namespace SatelliteCore.Api.Models.Entities
{
    public class UsuarioEntity
    {
        public int IDUsuario { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public int TipoDocumento { get; set; }
        public string NroDocumento { get; set; }
        public string Sexo { get; set; }
        public int Pais { get; set; }
        public string Correo { get; set; }
        public bool FlagCambioClave { get; set; }
        public string Estado { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Celular { get; set; }
        public string MotivoInactivo { get; set; }

    }
}
