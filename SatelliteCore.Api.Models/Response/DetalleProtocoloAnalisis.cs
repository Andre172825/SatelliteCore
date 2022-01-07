using System;
using System.Collections.Generic;
using System.Text;

namespace SatelliteCore.Api.Models.Response
{
    public struct DetalleProtocoloAnalisis
    {
        public string CompaniaSocio { get; set; }
        public string TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public string Estado { get; set; }
        public DateTime FechaDocumento { get; set; }
        public string ClienteNombre { get; set; }
        public string MonedaDocumento { get; set; }
        public decimal MontoTotal { get; set; }
        public string VoucherPeriodo { get; set; }
        public string VoucherNo { get; set; }
        public int ClienteNumero { get; set; }
        public string ImpresionPendienteFlag { get; set; }
        public decimal MontoPagado { get; set; }
        public string Comentarios { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public string ProcesoImportacionNumero { get; set; }
        public string FormaPagoNombre { get; set; }
        public string CreditoFlag { get; set; }
        public string Clasificacion { get; set; }
        public string ClienteReferencia { get; set; }
        public string ItemCodigo { get; set; }
        public string Descripcion { get; set; }
        public string OrdenFabricacion { get; set; }
        public string Lote { get; set; }
        public string UnidadCodigo { get; set; }
        public decimal CantidadPedida { get; set; }
        public decimal Monto { get; set; }
        public string NumeroDeParte { get; set; }
        public string ClasificacionRotacion { get; set; }
        public string AlmacenCodigo { get; set; }
        public string ItemSerie { get; set; }
        public string ProtocoloFlag { get; set; }
        public int Linea { get; set; }
    }   
}
