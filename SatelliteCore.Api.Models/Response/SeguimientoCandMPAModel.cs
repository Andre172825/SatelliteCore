
namespace SatelliteCore.Api.Models.Response
{
    public struct SeguimientoCandMPAModel
    {
        public int Candidato { get; set; }
        public string Item { get; set; }
        public string Descripcion { get; set; }
        public int Pronostico { get; set; }
        public int CoeficienteVariacion { get; set; }
        public int LimiteSuperior { get; set; }
        public int PuntoControl { get; set; }
        public int Maximo { get; set; }
        public decimal Aduanas { get; set; }
        public decimal PendienteOC { get; set; }
        public decimal TransporteCC { get; set; }
        public decimal StockActual { get; set; }
        public decimal StockComprometido { get; set; }
        public decimal StockDisponible { get; set; }
        public decimal CantPedir { get; set; }
        
    }
}
