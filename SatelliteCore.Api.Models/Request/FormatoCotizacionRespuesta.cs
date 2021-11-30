using System.Collections.Generic;

namespace SatelliteCore.Api.Models.Request
{
    public class FormatoCotizacionRespuesta
    {
        public int IdFormato { get; set; }
        public string NroDocumento { get; set; }
        public List<Campo> Campos { get; set; }
        public string Detalle { get; set; }
        public int CodUsuario { get; set; }
    }
}
