using SatelliteCore.Api.CrossCutting.Helpers;
using SatelliteCore.Api.DataAccess.Contracts.Repository;
using SatelliteCore.Api.Models.Entities;
using SatelliteCore.Api.Models.Request;
using SatelliteCore.Api.Models.Response;
using SatelliteCore.Api.Services.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SatelliteCore.Api.Services
{
    public class ComercialServices : IComercialServices
    {
        private readonly IComercialRepository _comercialRepository;

        public ComercialServices(IComercialRepository comercialRepository)
        {
            _comercialRepository = comercialRepository;
        }
        public async Task<(List<CotizacionEntity>, int)> ListarCotizaciones(DatosListarCotizacionesPaginado datos)
        {
            return await _comercialRepository.ListarCotizaciones(datos);
        }

        public async Task<FormatoCotizacionEntity> ObtenerEstructuraFormato(DatosEstructuraFormatoCotizacion datos)
        {
            return await _comercialRepository.ObtenerEstructuraFormato(datos);
        }

        public async Task<int> RegistrarRespuestas(FormatoCotizacionRespuesta datos)
        {
            return await _comercialRepository.RegistrarRespuestas(datos);
        }

        public async Task<(List<DetalleProtocoloAnalisis>, int)> ListarProtocoloAnalisis(DatosProtocoloAnalisisListado datos)
        {
            return await _comercialRepository.ListarProtocoloAnalisis(datos);
        }
        public async Task<List<DetalleClientes>> ListarClientes()
        {
            return await _comercialRepository.ListarClientes();
        }

    }
}
