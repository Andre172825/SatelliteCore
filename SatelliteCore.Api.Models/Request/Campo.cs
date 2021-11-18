using System;
using System.Collections.Generic;
using System.Text;

namespace SatelliteCore.Api.Models.Request
{
    public class Campo
    {
        public int IdCampo { get; set; }
        public string DescripcionCampo { get; set; }
        public string CodigoDescripcionCampo { get; set; }
        public string TipoCampo { get; set; }
        public string Respuesta { get; set; }
        public string CodigoRespuesta { get; set; }
    }
}
