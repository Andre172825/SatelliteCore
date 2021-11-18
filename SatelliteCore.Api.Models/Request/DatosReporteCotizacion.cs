using System;
using System.Collections.Generic;
using System.Text;

namespace SatelliteCore.Api.Models.Request
{
    public struct DatosReporteCotizacion
    {
        public string NumeroDocumento { get; set; }
        public int IdFormato { get; set; }
    }
}
