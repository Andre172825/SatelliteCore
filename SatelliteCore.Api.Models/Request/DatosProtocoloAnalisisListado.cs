using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SatelliteCore.Api.Models.Request
{
    public struct DatosProtocoloAnalisisListado
    {
        public string FechaInicio { get; set; }
        public string FechaFinal { get; set; }
        public string NumeroDocumento { get; set; }
        public string Lote { get; set; }
        public string OrdenFabricacion { get; set; }
        public string IdCliente { get; set; }
        public string TipoDoc { get; set; }
        [Required]
        public int Pagina { get; set; }
        [Required]
        public int RegistrosPorPagina { get; set; }
    }
}
