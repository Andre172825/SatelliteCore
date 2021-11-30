using Dapper;
using SatelliteCore.Api.DataAccess.Contracts.Repository;
using SatelliteCore.Api.Models.Config;
using SatelliteCore.Api.Models.Generic;
using SatelliteCore.Api.Models.Request;
using SatelliteCore.Api.Models.Response;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace SatelliteCore.Api.DataAccess.Repository
{
    public class PronosticoRepository : IPronosticoRepository
    {
        private readonly IAppConfig _appConfig;

        public PronosticoRepository(IAppConfig appConfig)
        {
            _appConfig = appConfig;
        }

        public async Task<List<PedidosItemTransitoModel>> ListarDetalleTransitoItem()
        {
            List<PedidosItemTransitoModel> result = new List<PedidosItemTransitoModel>();

            using (var connection = new SqlConnection(_appConfig.contextSpring))
            {
                string sql = "DECLARE @FechaActual DATETIME = GETDATE(), @Periodo CHAR(6) = CONVERT(CHAR(6), GETDATE(), 112) "
                    + "; WITH Temp_PronosticoPeriodo AS(SELECT '01000000' CompaniaSocio, ItemSpring FROM ML_Pronostico a WHERE Periodo = @Periodo AND LEFT(a.Regla, 2) IN ('PT','ExtPT')) "
                    + ",temp_PedidosItemPronostico AS(SELECT a.ItemSpring Item, b.NumeroLote, c.PedidoNumero, c.FechaPreparacion, DATEDIFF(DAY, c.FechaPreparacion, @FechaActual) DifDias, b.CantidadPedida, b.AlmacenCodigo "
                    + "FROM Temp_PronosticoPeriodo a INNER JOIN EP_PedidoDetalle b ON a.ItemSpring = b.Item AND a.CompaniaSocio = b.CompaniaSocio "
                    + "INNER JOIN EP_Pedido c ON b.CompaniaSocio = c.CompaniaSocio AND b.PedidoNumero = c.PedidoNumero AND c.ESTADO<> 'AN' AND c.TipoVenta = 'STP' "
                    + "),temp_ItemStockEnTransito AS(SELECT b.Item, b.Lote, SUM(b.Cantidad) CantidadIngresada FROM temp_PedidosItemPronostico a INNER JOIN WH_TransaccionDetalle b WITH(NOLOCK) "
                    + "ON b.TipoDocumento = 'NI' AND a.Item = b.Item AND a.NumeroLote = b.Lote INNER JOIN WH_TransaccionHeader c WITH(NOLOCK) ON c.CompaniaSocio = '01000000' AND b.TipoDocumento = c.TipoDocumento "
                    + "AND b.NumeroDocumento = c.NumeroDocumento AND c.TipoDocumento = 'NI' AND c.TransaccionCodigo = 'CCI' WHERE c.Estado <> 'AN' GROUP BY b.Item, b.Lote ) "
                    + "SELECT a.PedidoNumero, ISNULL(a.NumeroLote, 'Sin lote') NumeroLote, a.Item, ISNULL(a.CantidadPedida, 0) CantidadPedida, ISNULL(b.CantidadIngresada, 0) CantidadIngresada, "
                    + "(ISNULL(a.CantidadPedida, 0) - ISNULL(b.CantidadIngresada, 0)) CantidadPendiente, a.FechaPreparacion, a.DifDias "
                    + "FROM temp_PedidosItemPronostico a LEFT JOIN temp_ItemStockEnTransito b ON a.Item = b.Item AND a.NumeroLote = b.Lote WHERE ISNULL(a.CantidadPedida, 0) - ISNULL(b.CantidadIngresada, 0) > 0";

                result = (List<PedidosItemTransitoModel>)await connection.QueryAsync<PedidosItemTransitoModel>(sql);
                connection.Dispose();
            }

            return result;
        }

        public async Task<List<SeguimientoCandidatoModel>> ListaSeguimientoCandidatos(string periodo, bool menorPC, bool mayorPC, bool pedidosAtrasados )
        {
            List<SeguimientoCandidatoModel> result =  new List<SeguimientoCandidatoModel>();

            using (var satelliteContext = new SqlConnection(_appConfig.contextSpring))
            {
                result = (List<SeguimientoCandidatoModel>) await satelliteContext.QueryAsync<SeguimientoCandidatoModel>("usp_Pro_SeguimientoCandidatos", new { periodo, menorPC, mayorPC, pedidosAtrasados },  commandType: CommandType.StoredProcedure);
                satelliteContext.Dispose();
            }

            return result;
        }

        public async Task<SeguimientoCandMPAGenericModel> ListaSeguimientoCandidatosMP(string regla)
        {
            SeguimientoCandMPAGenericModel result = new SeguimientoCandMPAGenericModel();

            using (var satelliteContext = new SqlConnection(_appConfig.contextSpring))
            {
                result.SeguimientoCandidatosMPA =  await satelliteContext.QueryAsync<SeguimientoCandMPAModel>("usp_pro_SeguimientoCandidatoMPA", new { regla }, commandType: CommandType.StoredProcedure);
                result.OrdenComprasPendientes = await satelliteContext.QueryAsync<DetalleSeguimientoCandMPAModel>("usp_pro_SeguimientoDetalleCandidatoMPA", new { regla }, commandType: CommandType.StoredProcedure);

                satelliteContext.Dispose();
            }

            return result;
        }

        public async Task<(IEnumerable<PedidosCreadosAutoLogModel> ListaPedidos, int TotalRegistros)> ListaPedidosCreadoAuto(PedidosCreadosDataModel filtro)
        {

            (IEnumerable<PedidosCreadosAutoLogModel> ListaPedidos, int TotalRegistros) result;
            DynamicParameters parametros = new DynamicParameters();

            parametros.Add("@FechaInicio", filtro.FechaInicio);
            parametros.Add("@FechaFin", filtro.FechaFin);
            parametros.Add("@Item", filtro.Item);
            parametros.Add("@Pagina", filtro.Pagina);
            parametros.Add("@RegistrosPorPagina", filtro.RegistrosPorPagina);
            parametros.Add("@TotalRegistos", dbType: DbType.Int32, direction: ParameterDirection.Output);

            using (var satelliteContext = new SqlConnection(_appConfig.contextSpring))
            {
                result.ListaPedidos = await satelliteContext.QueryAsync<PedidosCreadosAutoLogModel>("usp_Pro_ListarPedidosCreadosAuto", parametros, commandType: CommandType.StoredProcedure);
                result.TotalRegistros = parametros.Get<int>("@TotalRegistos");
                satelliteContext.Dispose();
            }

            return result;
        }


        


    }
}
