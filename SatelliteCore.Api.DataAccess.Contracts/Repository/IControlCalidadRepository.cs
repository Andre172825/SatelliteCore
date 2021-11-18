using SatelliteCore.Api.Models.Entities;
using SatelliteCore.Api.Models.Request;
using SatelliteCore.Api.Models.Response;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace SatelliteCore.Api.DataAccess.Contracts.Repository
{
    public interface IControlCalidadRepository
    {
        public Task<(List<CertificadoEsterilizacionEntity>, int)> ListarCertificados(DatosListarCertificadoPaginado datos);
        public bool RegistrarCertificado(CertificadoEsterilizacionEntity certificado);
        public Task<(List<LoteEntity>, int)> ListarLotes(DatosLote datos);
        public Task<int> RegistrarLote(LoteEntity lote);
        public Task<(List<CotizacionEntity>, int)> ListarCotizaciones(DatosListarCotizacionesPaginado datos);
    }
}
