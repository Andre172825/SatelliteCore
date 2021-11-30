using Microsoft.AspNetCore.Mvc;
using SatelliteCore.Api.Models.Request;
using SatelliteCore.Api.Models.Response;
using SatelliteCore.Api.Services.Contracts;
using SatelliteCore.Api.CrossCutting.Config;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
//using SatelliteCore.Api.Filters;

namespace SatelliteCore.Api.Controllers.Auth
{

    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ValidacionAccesosController: ControllerBase
    {
        private readonly IValidacionesServices _validacionesServices;

        public ValidacionAccesosController (IValidacionesServices validacionesServices)
        {
            _validacionesServices = validacionesServices;
        }

        [HttpPost("ValidarAccesoRuta")]
        public async Task<IActionResult> AuthenticateUser(ValidacionRutaDataModel model)
        {

            if (!ModelState.IsValid)
            {
                var listaErrores = ModelState.Values.SelectMany(m => m.Errors).Select(e => e.ErrorMessage).ToList();
                ResponseModel<IEnumerable<string>> responseError =
                        new ResponseModel<IEnumerable<string>>(false, Constant.MODEL_VALIDATION_FAILED, listaErrores);
                return BadRequest(responseError);
            }

            (int codigo, string mensaje) result = await _validacionesServices.ValidarAccesoRuta(model);

            ResponseModel<object> responseSuccesss = new ResponseModel<object>(true, Constant.MESSAGE_SUCCESS, new{ result.codigo, result.mensaje });
            return Ok(responseSuccesss);
        }

        //[PermitCode("MN0002")]
        [HttpGet("validarToken")]
        public ActionResult ValidarToken()
        {
            return Ok("OK");
        }

    }
}
