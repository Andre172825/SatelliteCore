using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SatelliteCore.Api.Models.Generic;
using SatelliteCore.Api.Models.Request;
using SatelliteCore.Api.Models.Response;
using SatelliteCore.Api.Services.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SatelliteCore.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class PronosticoController : ControllerBase
    {
        private readonly IPronosticoServices _pronosticoServices;

        public PronosticoController(IPronosticoServices pronosticoServices)
        {
            _pronosticoServices = pronosticoServices;
        }

        [HttpGet("SegimientoCandidatos")]
        public async Task<ActionResult> SeguimientoCandidato(string periodo, bool primerFiltro, bool segundoFiltro, bool tercerFiltro)
        {
            List<SeguimientoCandidatoModel> listaCandidatos = await _pronosticoServices.ListaSeguimientoCandidatos(periodo, primerFiltro, segundoFiltro, tercerFiltro);
            return Ok(listaCandidatos);
        }

        [HttpPost("ListaPedidosCreadoAuto")]
        public async Task<ActionResult> ListaPedidosCreadoAuto(PedidosCreadosDataModel filtro)
        {
            (IEnumerable<PedidosCreadosAutoLogModel> ListaPedidos, int TotalRegistros) result = await _pronosticoServices.ListaPedidosCreadoAuto(filtro);

            PaginacionModel<PedidosCreadosAutoLogModel> PedidosPaginados = 
                new PaginacionModel<PedidosCreadosAutoLogModel>((List<PedidosCreadosAutoLogModel>)result.ListaPedidos, filtro.Pagina, filtro.RegistrosPorPagina, result.TotalRegistros);
                
            return Ok(PedidosPaginados);
        }

        [HttpGet("SegimientoCandidatosMP")]
        public async Task<ActionResult> SegimientoCandidatosMP(string regla)
        {
            SeguimientoCandMPAGenericModel listaCandidatos = await _pronosticoServices.ListaSeguimientoCandidatosMP(regla);
            return Ok(listaCandidatos);
        }
    }
}