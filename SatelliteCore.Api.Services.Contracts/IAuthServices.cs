using SatelliteCore.Api.Models.Request;
using SatelliteCore.Api.Models.Response;
using System.Threading.Tasks;

namespace SatelliteCore.Api.Services.Contracts
{
    public interface IAuthServices
    {
        public Task<AuthResponse> AutenticacionUsuario(AuthRequestModel datosUsuario);
    }
}
