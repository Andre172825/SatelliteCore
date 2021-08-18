using SatelliteCore.Api.DataAccess.Contracts.Repository;
using SatelliteCore.Api.Services.Contracts;
using SatelliteCore.Api.Models.Request;
using System.Threading.Tasks;

namespace SatelliteCore.Api.Services
{
    public class ValidacionesServices : IValidacionesServices
    {
        private readonly IAccesosRepository _accesosRepository;

        public ValidacionesServices( IAccesosRepository accesosRepository)
        {
            _accesosRepository = accesosRepository;
        }

        public async Task<(int codigo, string mensaje)> ValidarAccesoRuta(ValidacionRutaDataModel datos)
        {
            (int codigo, string mensaje) result = await _accesosRepository.ValidarAccesoRuta(datos);

            return result;
        }

        public async Task<bool> ValidarPermisoAcceso(int usuario, string permiso)
        {
            bool result = await _accesosRepository.ValidarPermisoAcceso(usuario, permiso);

            return result;
        }
    }
}
