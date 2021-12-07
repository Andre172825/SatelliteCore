using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SatelliteCore.Api.Models.Config;
using SatelliteCore.Api.Models.Request;
using SatelliteCore.Api.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SatelliteCore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RRHHController : ControllerBase
    {
        private readonly IAppConfig _appConfig;

        public RRHHController( IAppConfig appConfig)
        {
            _appConfig = appConfig;
        }

        [HttpPost("GenerarReporteAsistencia")]
        public async Task<ActionResult> GenerarReporteAsistencia(DatosReporteRRHH datos)
        {
            try
            {
                string Reporte = "ReporteDiarioAsistencia&rs:Command=Render";                
                string Formato = "&rs:Format=excel";
                string Parametros = "&Fecha=" + datos.FechaReporte.ToString().Substring(0,10);
                

                var theURL = _appConfig.ReportRRHH + Reporte + Parametros + Formato;

                var httpClientHandler = new HttpClientHandler()
                {
                    UseDefaultCredentials = true
                };

                HttpClient webClient = new HttpClient(httpClientHandler);

                Byte[] result = await webClient.GetByteArrayAsync(theURL);
                string base64String = Convert.ToBase64String(result, 0, result.Length);
                ResponseModel<string> response
                        = new ResponseModel<string>(true, "El reporte se generó correctamente", base64String);
                return Ok(response);
            }
            catch (Exception ex)
            {
                ResponseModel<string> response
                        = new ResponseModel<string>(true, "El reporte no se generó", ex.Message);
                return BadRequest(response);
            }

        }
    }
}
