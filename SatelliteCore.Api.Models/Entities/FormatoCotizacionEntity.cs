using SatelliteCore.Api.Models.Request.Cotizacion;
using System;
using System.Collections.Generic;
using System.Text;

namespace SatelliteCore.Api.Models.Entities
{
    public class FormatoCotizacionEntity
    {
        public int IdFormato { get; set; }
        public string NombreFormato { get; set; }
        public List<Campo> Campos { get; set; }
        public List<Cabecera> Cabeceras { get; set; }
        public List<Filas> Data { get; set; }
        public FormatoCotizacionEntity()
        {
            this.Data = new List<Filas>();
        }
        
        public class Campo
        {
            public int IdCampo { get; set; }
            public string Identificador { get; set; }
            public string DescripcionCampo { get; set; }
            public string ValorDefault { get; set; }
            public string TipoCampo { get; set; }
            //public int IdValorCampo { get; set; }
            public string CodigoValorCampo { get; set; }
            public string ValorCampo { get; set; }
        }
        public class Cabecera
        {
            public string NombreCabecera { get; set; }
        }
        public class Filas
        {
            public List<Fila> lstFilas { get; set; }
            public Filas()
            {
                this.lstFilas = new List<Fila>();
            }
            public class Fila
            {
                public string ValorColumna { get; set; }
            }
        }
    }
}
