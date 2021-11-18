using SatelliteCore.Api.Models.Generic;
using SatelliteCore.Api.Models.Request;
using SatelliteCore.Api.Models.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SatelliteCore.Api.Services.Contracts
{
    public interface IPronosticoServices
    {
        public Task<List<SeguimientoCandidatoModel>> ListaSeguimientoCandidatos(string periodo, bool menorPC, bool mayorPC, bool pedidosAtrasados);
        public Task<(IEnumerable<PedidosCreadosAutoLogModel> ListaPedidos, int TotalRegistros)> ListaPedidosCreadoAuto(PedidosCreadosDataModel filtro);
        public Task<SeguimientoCandMPAGenericModel> ListaSeguimientoCandidatosMP(string regla);
    }
}
