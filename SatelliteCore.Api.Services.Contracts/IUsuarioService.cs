using SatelliteCore.Api.Models.Entities;
using SatelliteCore.Api.Models.Request;
using SatelliteCore.Api.Models.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SatelliteCore.Api.Services.Contracts
{
    public interface IUsuarioService
    {
        public Task<AuthResponse> ObtenerUsuarioLogin(AuthRequestModel datosUsuario);
        public Task<UsuarioEntity> ObtenerUsuario(DatoUsuarioBasico datos);
        public Task<(List<UsuarioEntity>, int)> ListarUsuarios(DatosListarUsuarioPaginado datos);
        public Task<int> CambiarClave(ActualizarClaveModel datos);
    }
}
