using SatelliteCore.Api.Models.Entities;
using SatelliteCore.Api.Models.Request;
using SatelliteCore.Api.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SatelliteCore.Api.DataAccess.Contracts.Repository
{
    public interface IComercialRepository
    {
        public Task<FormatoCotizacionEntity> ObtenerEstructuraFormato(DatosEstructuraFormatoCotizacion datos);
        //public Task<int> RegistrarLote(LoteEntity lote);
        public Task<(List<CotizacionEntity>, int)> ListarCotizaciones(DatosListarCotizacionesPaginado datos);
        public Task<int> RegistrarRespuestas(FormatoCotizacionRespuesta datos);
        public Task<(List<DetalleProtocoloAnalisis>, int)> ListarProtocoloAnalisis(DatosProtocoloAnalisisListado datos);
        public Task<List<DetalleClientes>> ListarClientes();
    }
}
