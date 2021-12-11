using System;
using System.Collections.Generic;
using System.Text;

namespace SatelliteCore.Api.Models.Entities
{
    public class CertificadoEsterilizacionEntity
    {
        public int Id { get; set; }
        public string CodigoCertificado { get; set; }
        public DateTime FechaEmision { get; set; }
        public string OrdenServicio { get; set; }
        public string Cliente { get; set; }
        public string Lote { get; set; }
        public string Producto { get; set; }
        public string Marca { get; set; }
        public decimal Cantidad { get; set; }
        public string Equipo { get; set; }
        public string CantidadUnidadMedida { get; set; }
        public string Estado { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaTermino { get; set; }
        public string Metodo { get; set; }
        public string Temperatura { get; set; }
        public int TiempoAireacion { get; set; }
        public string TiempoAireacionUnidadMedida { get; set; }
        public int TiempoExposicion { get; set; }
        public string TiempoExposicionUnidadMedida { get; set; }
        public decimal HRProceso { get; set; }
        public string Observaciones { get; set; }
        public string Conclusion { get; set; }
        public string Usuario { get; set; }

        public int IDLoteCintaTestigo { get; set; }
        public int IDLoteIndicadorQuimico { get; set; }
        public bool ConformeCintaTestigo { get; set; }
        public bool ConformeIndicadorQuimico { get; set; }
        public string ModeloTrazasOE { get; set; }
        public string CodigoTrazasOE { get; set; }
        public bool ConformeTrazasOE { get; set; }
        public string TipoIB { get; set; }
        public string CodigoIB { get; set; }
        public int IDLoteIB { get; set; }
        //public string DescripcionIB { get; set; }
        //public string LoteIB { get; set; }
        //public string ExpiraIB { get; set; }
        public int IBExpuestos { get; set; }
        public bool IBExpuestosResultado { get; set; }
        public int IBNoExpuestos { get; set; }
        public bool IBNoExpuestosResultado { get; set; }
        public bool ConformeIB { get; set; }
    }
}
