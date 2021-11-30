using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SatelliteCore.Api.CrossCutting.Config;
using SatelliteCore.Api.Models.Entities;
using SatelliteCore.Api.Models.Generic;
using SatelliteCore.Api.Models.Request;
using SatelliteCore.Api.Models.Response;
using SatelliteCore.Api.Services.Contracts; 

namespace SatelliteCore.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

       
        [HttpPost("ObtenerUsuario")]
        public async Task<ActionResult> ObtenerUsuario( DatoUsuarioBasico user)
        {
            if (!ModelState.IsValid)
            {
                //IEnumerable<string> errorList = ModelState.Values.SelectMany(m => m.Errors).Select(e => e.ErrorMessage).ToList();
                ResponseModel<string> responseError =
                        new ResponseModel<string>(false, Constant.MODEL_VALIDATION_FAILED, "Los datos enviados no son válidos");

                return BadRequest(responseError);
            }

            UsuarioEntity usuario = await _usuarioService.ObtenerUsuario(user);

            if (usuario == null)
            {
                ResponseModel<string> response = new ResponseModel<string>(false, "No se pudo encontrar al usuario", "");
                return Ok(response);
            }

            ResponseModel<UsuarioEntity> responseSuccesss =
                new ResponseModel<UsuarioEntity>(true, Constant.MESSAGE_SUCCESS, usuario);

            return Ok(responseSuccesss);
        }


        [HttpPost("ListarUsuarios")]
        public async Task<ActionResult> ListarUsuario(DatosListarUsuarioPaginado datos)
        {
            if (!ModelState.IsValid)
            {
                ResponseModel<string> responseError =
                        new ResponseModel<string>(false, Constant.MODEL_VALIDATION_FAILED, "");

                return BadRequest(responseError);
            }

            PaginacionModel<UsuarioEntity> response = await _usuarioService.ListarUsuarios(datos);

            return Ok(response);

        }

        [HttpPost("CambioClave")]
        public async Task<ActionResult> CambiarClaveUsuario(ActualizarClaveModel datos)
        {
            if (!ModelState.IsValid)
            {
                ResponseModel<string> responseError =
                        new ResponseModel<string>(false, Constant.MODEL_VALIDATION_FAILED, "");

                return BadRequest(responseError);
            }

            int result = await _usuarioService.CambiarClave(datos);

            ResponseModel <int> response
                    = new ResponseModel<int>(true, "La clave se ha cambiado correcamente", result);

            return Ok(response);

        }






    }
}
