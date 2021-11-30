using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SatelliteCore.Api.Models.Request;
using SatelliteCore.Api.Models.Response;
using SatelliteCore.Api.Services.Contracts;
using SatelliteCore.Api.CrossCutting.Config;
    
namespace SatelliteCore.Api.Controllers.Auth
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthServices _authServices;
        public AuthenticationController(IAuthServices authServices)
        {
            _authServices = authServices;
        }

        [HttpPost("authenticateUser")]
        public async Task<IActionResult> AuthenticateUser(AuthRequestModel model)
        {

            if (!ModelState.IsValid)
            {
                var listaErrores = ModelState.Values.SelectMany(m => m.Errors).Select(e => e.ErrorMessage).ToList();
                ResponseModel<IEnumerable<string>> responseError =
                        new ResponseModel<IEnumerable<string>>(false, Constant.MODEL_VALIDATION_FAILED, listaErrores);
                return BadRequest(responseError);
            }

            AuthResponse usuario = await _authServices.AutenticacionUsuario(model);

            if (usuario == null)
            {
                ResponseModel<string> responseSuccess = new ResponseModel<string>(false, "Correo o contraseña incorrecta.", "");
                return Unauthorized(responseSuccess);
            }

            ResponseModel<AuthResponse> responseSuccesss = new ResponseModel<AuthResponse>(true, Constant.MESSAGE_SUCCESS, usuario);
            return Ok(responseSuccesss);
        }


    }
}
