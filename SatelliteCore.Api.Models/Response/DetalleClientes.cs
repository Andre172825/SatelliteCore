using System;
using System.Collections.Generic;
using System.Text;

namespace SatelliteCore.Api.Models.Response
{
    public struct DetalleClientes
    {
        public string Persona { get; set; }
        public string NombreCompleto { get; set; }
        public string Estado { get; set; }
    }
}
