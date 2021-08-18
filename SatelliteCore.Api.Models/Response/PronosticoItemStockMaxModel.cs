
using System;
using System.Collections.Generic;

namespace SatelliteCore.Api.Models.Response
{
    public class PronosticoItemStockMaxModel
    {
        public int Candidato { get; set; }
        public string CodSutReduc { get; set; }
        public string CodSut { get; set; }
        public string ItemSpring { get; set; }
        public int StockActual { get; set; }
        public int StockComprometido { get; set; }
        public int StockDisponible { get; set; }
        public int StockEnTransito { get; set; }
        public decimal CoeficienteVariacion { get; set; }
        public int Pronostico { get; set; }
        public int PuntoControl { get; set; }
        public int LimiteSuperior { get; set; }
        public int StockAFabricar { get; set; }
        public DateTime FechaRegistro { get; set; }
        public int DiferenciaDias { get; set; }
        public List<PedidosItemTransitoModel> PedidosTransito { get; set; }
        public PronosticoItemStockMaxModel()
        {
            PedidosTransito = new List<PedidosItemTransitoModel>();
        }
    }
}
