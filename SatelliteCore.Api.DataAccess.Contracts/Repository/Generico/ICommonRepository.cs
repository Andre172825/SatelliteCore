using SatelliteCore.Api.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SatelliteCore.Api.DataAccess.Contracts.Repository
{
    public interface ICommonRepository
    {
        public Task<IEnumerable<TipoDocumentoIdentidadEntity>> ListarTipoDocumentoIndentidad();

        public Task<IEnumerable<PaisEntity>> ListarPaises();

        public Task<List<MenuEntity>> ListarMenuxUsuario(int usuario);
        public Task<IEnumerable<RolEntity>> ListarRoles(string estado);
    }
}
