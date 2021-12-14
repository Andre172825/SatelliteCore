using Dapper;
using SatelliteCore.Api.DataAccess.Contracts.Repository;
using SatelliteCore.Api.Models.Config;
using SatelliteCore.Api.Models.Entities;
using SatelliteCore.Api.Models.Request;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SatelliteCore.Api.DataAccess.Repository
{
    public class ControlCalidadRepository : IControlCalidadRepository
    {
        private readonly IAppConfig _appConfig;

        public ControlCalidadRepository(IAppConfig appConfig)
        {
            _appConfig = appConfig;
        }
        public async Task<(List<CertificadoEsterilizacionEntity>, int)> ListarCertificados(DatosListarCertificadoPaginado datos)
        {
            (List<CertificadoEsterilizacionEntity> ListaCertificados, int totalRegistros) result;

            using (var connection = new SqlConnection(_appConfig.contextSatelliteDB))
            {
                using (var result_db = await connection.QueryMultipleAsync("usp_ListaCertificadosEsterilizacion", datos, commandType: CommandType.StoredProcedure))
                {
                    result.ListaCertificados = result_db.Read<CertificadoEsterilizacionEntity>().ToList();
                    result.totalRegistros = result_db.Read<int>().First();
                }

                connection.Dispose();
            }

            return result;
        }

        public async Task<(List<LoteEntity>, int)> ListarLotes(DatosLote datos)
        {
            (List<LoteEntity> ListaLotes, int totalRegistros) result;
            datos.Descripcion = string.IsNullOrEmpty(datos.Descripcion) == true ? "" : datos.Descripcion;

            using (var connection = new SqlConnection(_appConfig.contextSatelliteDB))
            {
                string script = "SELECT Id, Descripcion, Lote, Expira FROM TBMCCLote WHERE Grupo = IIF(@Identificador = 0, Grupo, @Identificador) AND Descripcion LIKE '%' + @descripcion + '%' " +
                                " ORDER BY Id OFFSET (@pagina - 1) * @registrosPorPagina ROWS FETCH NEXT @registrosPorPagina ROWS ONLY; " +
                                "SELECT Count(1) FROM TBMCCLote WHERE Descripcion LIKE '%' + @descripcion + '%';";
                using (var result_db = await connection.QueryMultipleAsync(script, new { datos.Descripcion, datos.Identificador, datos.Pagina, datos.RegistrosPorPagina, }))
                {
                    result.ListaLotes = result_db.Read<LoteEntity>().ToList();
                    result.totalRegistros = result_db.Read<int>().First();
                }
                connection.Dispose();
            }

            return result;
        }

        public bool RegistrarCertificado(CertificadoEsterilizacionEntity certificado)
        {
            var si = true;
            using (var connection = new SqlConnection(_appConfig.contextSatelliteDB))
            {
                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@fechaEmision", certificado.FechaEmision, DbType.DateTime, ParameterDirection.Input);
                parameter.Add("@ordenServicio", certificado.OrdenServicio, DbType.String, ParameterDirection.Input);
                parameter.Add("@cliente", certificado.Cliente, DbType.String, ParameterDirection.Input);
                parameter.Add("@lote", certificado.Lote, DbType.String, ParameterDirection.Input);
                parameter.Add("@producto", certificado.Producto, DbType.String, ParameterDirection.Input);
                parameter.Add("@marca", certificado.Marca, DbType.String, ParameterDirection.Input);
                parameter.Add("@cantidad", certificado.Cantidad, DbType.Decimal, ParameterDirection.Input);
                parameter.Add("@equipo", certificado.Equipo, DbType.String, ParameterDirection.Input);
                parameter.Add("@cantidadUnidadMedida", certificado.CantidadUnidadMedida, DbType.String, ParameterDirection.Input);
                parameter.Add("@estado", certificado.Estado, DbType.String, ParameterDirection.Input);
                parameter.Add("@fechaInicio", certificado.FechaInicio, DbType.DateTime, ParameterDirection.Input);
                parameter.Add("@fechaTermino", certificado.FechaTermino, DbType.DateTime, ParameterDirection.Input);
                parameter.Add("@metodo", certificado.Metodo, DbType.String, ParameterDirection.Input);
                parameter.Add("@temperatura", certificado.Temperatura, DbType.String, ParameterDirection.Input);
                parameter.Add("@tiempoAireacion", certificado.TiempoAireacion, DbType.Int32, ParameterDirection.Input);
                parameter.Add("@tiempoAireacionUnidadMedida", certificado.TiempoAireacionUnidadMedida, DbType.String, ParameterDirection.Input);
                parameter.Add("@tiempoExposicion", certificado.TiempoExposicion, DbType.Int32, ParameterDirection.Input);
                parameter.Add("@tiempoExposicionUnidadMedida", certificado.TiempoExposicionUnidadMedida, DbType.String, ParameterDirection.Input);
                parameter.Add("@hrProceso", certificado.HRProceso, DbType.Decimal, ParameterDirection.Input);
                parameter.Add("@observaciones", certificado.Observaciones, DbType.String, ParameterDirection.Input);
                parameter.Add("@conclusion", certificado.Conclusion, DbType.String, ParameterDirection.Input);
                parameter.Add("@usuario", certificado.Usuario, DbType.String, ParameterDirection.Input);
                parameter.Add("@IDLoteCintaTestigo", certificado.IDLoteCintaTestigo, DbType.Int32, ParameterDirection.Input);
                parameter.Add("@IDLoteIndicadorQuimico", certificado.IDLoteIndicadorQuimico, DbType.Int32, ParameterDirection.Input);
                parameter.Add("@conformeCintaTestigo", certificado.ConformeCintaTestigo, DbType.Boolean, ParameterDirection.Input);
                parameter.Add("@conformeIndicadorQuimico", certificado.ConformeIndicadorQuimico, DbType.Boolean, ParameterDirection.Input);
                parameter.Add("@modeloTrazasOE", certificado.ModeloTrazasOE, DbType.String, ParameterDirection.Input);
                parameter.Add("@codigoTrazasOE", certificado.CodigoTrazasOE, DbType.String, ParameterDirection.Input);
                parameter.Add("@conformeTrazasOE", certificado.ConformeTrazasOE, DbType.Boolean, ParameterDirection.Input);
                parameter.Add("@tipoIB", certificado.TipoIB, DbType.String, ParameterDirection.Input);
                parameter.Add("@codigoIB", certificado.CodigoIB, DbType.String, ParameterDirection.Input);
                parameter.Add("@IDLoteIB", certificado.IDLoteIB, DbType.Int32, ParameterDirection.Input);
                //parameter.Add("@descripcionIB", certificado.DescripcionIB, DbType.String, ParameterDirection.Input);
                //parameter.Add("@loteIB", certificado.LoteIB, DbType.String, ParameterDirection.Input);
                //parameter.Add("@expiraIB", certificado.ExpiraIB, DbType.String, ParameterDirection.Input);
                parameter.Add("@IBExpuestos", certificado.IBExpuestos, DbType.Int32, ParameterDirection.Input);
                parameter.Add("@IBExpuestosResultado", certificado.IBExpuestosResultado, DbType.Boolean, ParameterDirection.Input);
                parameter.Add("@IBNoExpuestos", certificado.IBNoExpuestos, DbType.Int32, ParameterDirection.Input);
                parameter.Add("@IBNoExpuestosResultado", certificado.IBNoExpuestosResultado, DbType.Boolean, ParameterDirection.Input);
                parameter.Add("@conformeIB", certificado.ConformeIB, DbType.Boolean, ParameterDirection.Input);

                connection.Execute("usp_RegistrarCertificadoEsterilizacion", parameter, commandType: CommandType.StoredProcedure);

                connection.Dispose();
            }
            return si;
        }

        public async Task<int> RegistrarLote(LoteEntity lote)
        {
            int result = 0;

            using (var connection = new SqlConnection(_appConfig.contextSatelliteDB))
            {
                string sql = "INSERT INTO TBMCCLote(Descripcion, Lote, Expira, Grupo)" +
                             "VALUES(@Descripcion, @Lote, @Expira, @Identificador)";

                result = await connection.ExecuteAsync(sql, new { lote.Descripcion, lote.Lote, lote.Expira, lote.Identificador });
                connection.Dispose();
            }

            return result;
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
    }
}
