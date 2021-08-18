using SatelliteCore.Api.Models.Request;
using System.Threading.Tasks;

namespace SatelliteCore.Api.Services.Contracts
{
    public interface IValidacionesServices
    {
        public Task<(int codigo, string mensaje)> ValidarAccesoRuta(ValidacionRutaDataModel datos);
        public Task<bool> ValidarPermisoAcceso(int usuario, string permiso);
    }
}
