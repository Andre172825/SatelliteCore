using System.Threading.Tasks;
using SatelliteCore.Api.Models.Request;

namespace SatelliteCore.Api.DataAccess.Contracts.Repository
{
    public interface IAccesosRepository
    {
        public Task<(int codigo, string mensaje)> ValidarAccesoRuta(ValidacionRutaDataModel datos);
        public Task<bool> ValidarPermisoAcceso(int usuario, string permiso);
    }
}
