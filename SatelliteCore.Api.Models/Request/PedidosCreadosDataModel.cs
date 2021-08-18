using System;

namespace SatelliteCore.Api.Models.Request
{
    public struct PedidosCreadosDataModel
    {
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string Item { get; set; }
        public int Pagina { get; set; }
        public int RegistrosPorPagina { get; set; }
    }
}
