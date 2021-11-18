using System;

namespace SatelliteCore.Api.Models.Response
{
    public struct DetalleSeguimientoCandMPAModel
    {
        public string Almacen { get; set; }
        public string Item { get; set; }
        public string NumeroOrden { get; set; }
        public string Proveedor { get; set; }
        public decimal Cantidad { get; set; }
        public decimal CantidadRecibida { get; set; }
        public decimal PendienteOC { get; set; }
        public DateTime Fecha { get; set; }
        public int DiferenciaFecha { get; set; }
    }
}
