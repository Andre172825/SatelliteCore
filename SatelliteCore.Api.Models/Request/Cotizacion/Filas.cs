using System;
using System.Collections.Generic;
using System.Text;

namespace SatelliteCore.Api.Models.Request.Cotizacion
{
    public class Filas
    {
        public List<Fila> lstFilas { get; set; }
        public Filas()
        {
            this.lstFilas = new List<Fila>();
        }
    }
}
