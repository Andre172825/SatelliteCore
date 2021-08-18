using System;

namespace SatelliteCore.Api.Models.Response
{
    public struct PedidosItemTransitoModel
    {
        public string PedidoNumero { get; set; }
        public string NumeroLote { get; set; }
        public string  Item { get; set; }
        public int CantidadPedida { get; set; }
        public DateTime FechaPreparacion { get; set; }
        public int DifDias { get; set; }
    }
}
