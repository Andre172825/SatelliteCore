using System;
using System.Collections.Generic;
using System.Text;

namespace SatelliteCore.Api.Models.Entities
{
    public class CotizacionEntity
    {
        public string IdFormato { get; set; }
        public string NumeroDocumento{ get; set; }
        public string ClienteNombre { get; set; }
        public string ClienteRUC { get; set; }
        public string ClienteDireccion { get; set; }
        public string Contacto { get; set; }

    }
}
