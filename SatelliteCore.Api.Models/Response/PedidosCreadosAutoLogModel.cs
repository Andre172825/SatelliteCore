using System;

namespace SatelliteCore.Api.Models.Response
{
    public struct PedidosCreadosAutoLogModel
    {
        public DateTime Fecha { get; set; }
        public string Item { get; set; }
        public string Descripcion { get; set; }
        public string Pedido { get; set; }
        public string Lote { get; set; }
        public int StockDisponible { get; set; }
        public int StockTransito { get; set; }
        public int PuntoControl { get; set; }
        public int LimiteSuperior { get; set; }
        public int CantSolicitada { get; set; }
        public int CantContraMuestra { get; set; }
        public string EstadoPedido { get; set; }
        public string EstadoLote { get; set; }
    }
}
