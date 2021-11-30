using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SatelliteCore.Api.CrossCutting.Config;
using SatelliteCore.Api.Models.Config;
using SatelliteCore.Api.Models.Entities;
using SatelliteCore.Api.Models.Generic;
using SatelliteCore.Api.Models.Request;
using SatelliteCore.Api.Models.Response;
using SatelliteCore.Api.Services.Contracts;



namespace SatelliteCore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ControlCalidadController : ControllerBase
    {
        private readonly IControlCalidadServices _controlCalidadServices;
        private readonly IAppConfig _appConfig;
        public ControlCalidadController(IControlCalidadServices controlCalidadServices, IAppConfig appConfig)
        {
            _controlCalidadServices = controlCalidadServices;
            _appConfig = appConfig;
        }

        [HttpPost("ListarCertificados")]
        public async Task<ActionResult> ListarCertificados(DatosListarCertificadoPaginado datos)
        {
            try {
                if (!ModelState.IsValid)
                {
                    ResponseModel<string> responseError =
                            new ResponseModel<string>(false, Constant.MODEL_VALIDATION_FAILED, "");

                    return BadRequest(responseError);
                }

                (List<CertificadoEsterilizacionEntity> lista, int totalRegistros) certificados = await _controlCalidadServices.ListarCertificados(datos);

                PaginacionModel<CertificadoEsterilizacionEntity> response
                        = new PaginacionModel<CertificadoEsterilizacionEntity>(certificados.lista, datos.Pagina, datos.RegistrosPorPagina, certificados.totalRegistros);
                return Ok(response);
            }
            catch(Exception ex)
            {

                return BadRequest(ex.Message);
            }
           
        }

        [HttpPost("RegistrarCertificado")]
        public ActionResult RegistrarCertificado(CertificadoEsterilizacionEntity certificado)
        {
            try
            {
                _controlCalidadServices.RegistrarCertificado(certificado);

                return Ok();
            }
            catch(Exception ex)
            {
                ResponseModel<string> response
                        = new ResponseModel<string>(true, "No se completó ", ex.Message);
                return BadRequest(response);
            }
            

        }

        [HttpPost("ListarLotes")]
        public async Task<ActionResult> ListarLotes(DatosLote datos)
        {
            if (!ModelState.IsValid)
            {
                ResponseModel<string> responseError =
                        new ResponseModel<string>(false, Constant.MODEL_VALIDATION_FAILED, "");

                return BadRequest(responseError);
            }

            (List<LoteEntity> lista, int totalRegistros) lotes = await _controlCalidadServices.ListarLotes(datos);

            PaginacionModel<LoteEntity> response
                    = new PaginacionModel<LoteEntity>(lotes.lista, datos.Pagina, datos.RegistrosPorPagina, lotes.totalRegistros);

            return Ok(response);
        }

        [HttpPost("GenerarReporte")]
        public async Task<ActionResult> GenerarReporte(DatosReporte datos)
        {
            try
            {
                string ReporteEsterilizacion = "Certificado_Esterilizacion&rs:Command=Render";
                string Formato = "&rs:Format=PDF";
                string Parametros = "&Id=" + datos.Id;

                var theURL = _appConfig.ReportControlDeCalidad + ReporteEsterilizacion + Parametros + Formato;

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
            catch(Exception ex)
            {
                ResponseModel<string> response
                        = new ResponseModel<string>(true, "El reporte no se generó", ex.Message);
                return BadRequest(response);
            }
            
        }

        [HttpPost("RegistrarLote")]
        public async Task<ActionResult> RegistrarLote(LoteEntity lote)
        {
            int result = await _controlCalidadServices.RegistrarLote(lote);

            ResponseModel<int> response
                    = new ResponseModel<int>(true, "El lote se registró correctamente", result);

            return Ok(response);

        }

        [HttpPost("ListarCotizaciones")]
        public async Task<ActionResult> ListarCotizaciones(DatosListarCotizacionesPaginado datos)
        {
            if (!ModelState.IsValid)
            {
                ResponseModel<string> responseError =
                        new ResponseModel<string>(false, Constant.MODEL_VALIDATION_FAILED, "");

                return BadRequest(responseError);
            }

            (List<CotizacionEntity> lista, int totalRegistros) certificados = await _controlCalidadServices.ListarCotizaciones(datos);

            PaginacionModel<CotizacionEntity> response
                    = new PaginacionModel<CotizacionEntity>(certificados.lista, datos.Pagina, datos.RegistrosPorPagina, certificados.totalRegistros);

            return Ok(response);
        }
    }
}
