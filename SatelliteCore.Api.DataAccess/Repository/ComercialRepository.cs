using Dapper;
using SatelliteCore.Api.DataAccess.Contracts.Repository;
using SatelliteCore.Api.Models.Config;
using SatelliteCore.Api.Models.Entities;
using SatelliteCore.Api.Models.Request;
using SatelliteCore.Api.Models.Response;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SatelliteCore.Api.DataAccess.Repository
{
    public class ComercialRepository : IComercialRepository
    {
        private readonly IAppConfig _appConfig;

        public ComercialRepository(IAppConfig appConfig)
        {
            _appConfig = appConfig;
        }
        public async Task<(List<CotizacionEntity>, int)> ListarCotizaciones(DatosListarCotizacionesPaginado datos)
        {
            (List<CotizacionEntity> ListaCertificados, int totalRegistros) result;

            using (var connection = new SqlConnection(_appConfig.contextSpring))
            {
                using (var result_db = await connection.QueryMultipleAsync("usp_COT_ConsultarCotizaciones", datos, commandType: CommandType.StoredProcedure))
                {
                    result.ListaCertificados = result_db.Read<CotizacionEntity>().ToList();
                    result.totalRegistros = result_db.Read<int>().First();
                }

                connection.Dispose();
            }

            return result;
        }

        public async Task<FormatoCotizacionEntity> ObtenerEstructuraFormato(DatosEstructuraFormatoCotizacion datos)
        {
            FormatoCotizacionEntity result = new FormatoCotizacionEntity();
            using (var connection = new SqlConnection(_appConfig.contextSatelliteDB))
            {
                using (var result_db = await connection.QueryMultipleAsync("usp_ObtenerEstructuraFormatoCotizacion", new { datos.IdFormato, datos.NumeroDocumento }, commandType: CommandType.StoredProcedure))
                {
                    result.Campos = result_db.Read<FormatoCotizacionEntity.Campo>().ToList();
                    result.IdFormato = result_db.Read<int>().First();
                    result.NombreFormato = "";
                }

                using (var result_db = await connection.QueryMultipleAsync("usp_ObtenerEstructuraFormatoCotizacionDetalle", new { datos.IdFormato, datos.NumeroDocumento }, commandType: CommandType.StoredProcedure))
                {
                    result.Cabeceras = result_db.Read<FormatoCotizacionEntity.Cabecera>().ToList();
                    var Lista = result_db.Read<object>().ToList();
                    int cnt = result.Cabeceras.Count;
                    foreach (var item in Lista)
                    {
                        FormatoCotizacionEntity.Filas filas = new FormatoCotizacionEntity.Filas();
                        var Heading = ((IDictionary<string, object>)item).Keys.ToArray();
                        var tmp = ((IDictionary<string, object>)item);

                        for (int i = 1; i <= cnt; i++)
                        {
                            FormatoCotizacionEntity.Filas.Fila fila = new FormatoCotizacionEntity.Filas.Fila
                            {
                                ValorColumna = tmp[Heading[i]].ToString()
                            };
                            filas.lstFilas.Add(fila);
                        }
                        result.Data.Add(filas);
                    }
                }
                connection.Dispose();
            }
            return result;
        }

        public async Task<int> RegistrarRespuestas(FormatoCotizacionRespuesta datos)
        {
            int IdCotizacionGenerada;

            try
            {
                using (var connection = new SqlConnection(_appConfig.contextSatelliteDB))
                {
                    using (var result_db = await connection.QueryMultipleAsync("usp_RegistrarCotizacionGenerada", new { datos.IdFormato, datos.NroDocumento, datos.CodUsuario }, commandType: CommandType.StoredProcedure))
                    {
                        IdCotizacionGenerada = result_db.Read<int>().First();
                    }
                    foreach (var item in datos.Campos)
                    {
                        DynamicParameters parameter = new DynamicParameters();
                        parameter.Add("@IdCotizacionGenerada", IdCotizacionGenerada, DbType.Int32, ParameterDirection.Input);
                        parameter.Add("@IdCampo", item.IdCampo, DbType.Int32, ParameterDirection.Input);
                        parameter.Add("@DescripcionCampo", item.DescripcionCampo, DbType.String, ParameterDirection.Input);
                        parameter.Add("@CodigoDescripcionCampo", item.CodigoDescripcionCampo, DbType.String, ParameterDirection.Input);
                        parameter.Add("@TipoCampo", item.TipoCampo, DbType.String, ParameterDirection.Input);
                        parameter.Add("@Respuesta", item.Respuesta, DbType.String, ParameterDirection.Input);
                        parameter.Add("@CodigoRespuesta", item.CodigoRespuesta, DbType.String, ParameterDirection.Input);

                        using (var result_db = await connection.QueryMultipleAsync("usp_RegistrarRespuestasCotizaciones", parameter, commandType: CommandType.StoredProcedure))
                        {

                        }
                    }
                    DynamicParameters parameter2 = new DynamicParameters();
                    parameter2.Add("@IdCotizacionGenerada", IdCotizacionGenerada, DbType.Int32, ParameterDirection.Input);
                    parameter2.Add("@IdCampo", 9999, DbType.Int32, ParameterDirection.Input);
                    parameter2.Add("@DescripcionCampo", "Detalle", DbType.String, ParameterDirection.Input);
                    parameter2.Add("@CodigoDescripcionCampo", "", DbType.String, ParameterDirection.Input);
                    parameter2.Add("@TipoCampo", "T", DbType.String, ParameterDirection.Input);
                    parameter2.Add("@Respuesta", datos.Detalle, DbType.String, ParameterDirection.Input);
                    parameter2.Add("@CodigoRespuesta", "", DbType.String, ParameterDirection.Input);
                    using (var result_db = await connection.QueryMultipleAsync("usp_RegistrarRespuestasCotizaciones", parameter2, commandType: CommandType.StoredProcedure))
                    {

                    }

                    connection.Dispose();
                }
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public async Task<(List<DetalleProtocoloAnalisis>, int)> ListarProtocoloAnalisis(DatosProtocoloAnalisisListado datos)
        {
            (List<DetalleProtocoloAnalisis> ListaProtocoloAnalisis, int totalRegistros) result;

            using (var connection = new SqlConnection(_appConfig.contextSatelliteDB))
            {
                using (var result_db = await connection.QueryMultipleAsync("usp_ListarProtocoloAnalisis", datos, commandType: CommandType.StoredProcedure))
                {
                    result.ListaProtocoloAnalisis = result_db.Read<DetalleProtocoloAnalisis>().ToList();
                    result.totalRegistros = result_db.Read<int>().First();
                }
                connection.Dispose();
            }
            return result;
        }

        public async Task<List<DetalleClientes>> ListarClientes()
        {
            List<DetalleClientes>  result;

            using (var connection = new SqlConnection(_appConfig.contextSatelliteDB))
            {
                using (var result_db = await connection.QueryMultipleAsync("usp_ListarClientes", commandType: CommandType.StoredProcedure))
                {
                    result = result_db.Read<DetalleClientes>().ToList();
                }
                connection.Dispose();
            }
            return result;
        }
    }
}
