using SatelliteCore.Api.DataAccess.Contracts.Repository;
using SatelliteCore.Api.Models.Generic;
using SatelliteCore.Api.Models.Request;
using SatelliteCore.Api.Models.Response;
using SatelliteCore.Api.Services.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SatelliteCore.Api.Services
{
    public class PronosticoServices : IPronosticoServices
    {
        private readonly IPronosticoRepository _pronosticoRepository;

        public PronosticoServices(IPronosticoRepository pronosticoRepository)
        {
            _pronosticoRepository = pronosticoRepository;
        }

        public async Task<List<SeguimientoCandidatoModel>> ListaSeguimientoCandidatos(string periodo, bool menorPC, bool mayorPC, bool pedidosAtrasados)
        {
            List<SeguimientoCandidatoModel> listaPronosticos = await _pronosticoRepository.ListaSeguimientoCandidatos(periodo, menorPC, mayorPC, pedidosAtrasados);
            List<PedidosItemTransitoModel> pedidosItemTransitos = await _pronosticoRepository.ListarDetalleTransitoItem();

            foreach (SeguimientoCandidatoModel pronostico in listaPronosticos)
            {
                List<PedidosItemTransitoModel> aux = pedidosItemTransitos.FindAll(x => x.Item == pronostico.Item);
                if (aux.Count > 0)
                    pronostico.PedidosTransito.AddRange(aux);

            }

            return listaPronosticos;
        }

        public async Task<(IEnumerable<PedidosCreadosAutoLogModel> ListaPedidos, int TotalRegistros)> ListaPedidosCreadoAuto(PedidosCreadosDataModel filtro)
        {
            return await _pronosticoRepository.ListaPedidosCreadoAuto(filtro);
        }

        public async Task<SeguimientoCandMPAGenericModel> ListaSeguimientoCandidatosMP(string regla)
        {
            return await _pronosticoRepository.ListaSeguimientoCandidatosMP(regla);
        }
    }
}
