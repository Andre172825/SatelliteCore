using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SatelliteCore.Api.Models.Request
{
    public struct DatosLote
    {
        public string Descripcion { get; set; }
        public int Identificador { get; set; }
        [Required]
        public int Pagina { get; set; }
        [Required]
        public int RegistrosPorPagina { get; set; }
    }
}
