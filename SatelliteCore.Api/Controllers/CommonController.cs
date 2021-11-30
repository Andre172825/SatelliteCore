using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SatelliteCore.Api.Models.Entities;
using SatelliteCore.Api.Models.Response;
using SatelliteCore.Api.Services.Contracts;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SatelliteCore.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class CommonController : ControllerBase
    {
        private readonly ICommonServices _commonService;
        public CommonController(ICommonServices commonService)
        {
            _commonService = commonService;
        }

        [HttpGet("ListarTipoDocumentoIdentidad")]
        public async Task<ActionResult> ListarTipoDocumentoIdentidad()
        {
            IEnumerable<TipoDocumentoIdentidadEntity> lista = await _commonService.ListarTipoDocumentoIndentidad();
            return Ok(lista);
        }

        [HttpGet("ListarPaises")]
        public async Task<ActionResult> ListarPaises()
        {
            IEnumerable<PaisEntity> lista = await _commonService.ListarPaises();

            return Ok(lista);
        }


        [HttpGet("ObtenerMenuUsuarioSesion")]
        public async Task<ActionResult> ObtenerMenuUsuarioSesion()
        {
            var claims = HttpContext.User.Identity as ClaimsIdentity;
            int idUsuario = int.Parse(claims.FindFirst(ClaimTypes.NameIdentifier).Value);
            List<MenuxUsuarioModel> lista = await _commonService.ListarMenuxUsuario(idUsuario);

            return Ok(lista);
        }

        [HttpGet("listarRoles")]
        public async Task<ActionResult> ListarRoles(string estado)
        {
            IEnumerable<RolEntity> listaUsuario = await _commonService.ListarRoles(estado);
            return Ok(listaUsuario);
        }

    }
}
