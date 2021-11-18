using SatelliteCore.Api.Models.Entities;
using SatelliteCore.Api.Models.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SatelliteCore.Api.Services.Contracts
{
    public interface IComercialServices
    {
        public Task<FormatoCotizacionEntity> ObtenerEstructuraFormato(DatosEstructuraFormatoCotizacion datos);
        //public Task<int> RegistrarLote(LoteEntity lote);
        public Task<(List<CotizacionEntity>, int)> ListarCotizaciones(DatosListarCotizacionesPaginado datos);
        public Task<int> RegistrarRespuestas(FormatoCotizacionRespuesta datos);
    }
}
