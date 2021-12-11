using System;
using System.Collections.Generic;
using System.Text;

namespace SatelliteCore.Api.Models.Entities
{
    public class LoteEntity
    {
        public string Id { get; set; }
        public string Descripcion { get; set; }
        public string Lote { get; set; }
        public string Expira { get; set; }
        public int Identificador { get; set; }
    }
}
