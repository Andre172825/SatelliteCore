using Dapper;
using SatelliteCore.Api.DataAccess.Contracts.Repository;
using SatelliteCore.Api.Models.Config;
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
                + "; WITH Temp_PronosticoPeriodo AS( SELECT '01000000' CompaniaSocio, ItemSpring FROM ML_Pronostico WHERE Periodo = @Periodo AND Regla = 'PTSUAR1' "
                + "), temp_PedidosItemPronostico AS( SELECT a.ItemSpring Item, b.NumeroLote, c.PedidoNumero, c.FechaPreparacion, "
                + "DATEDIFF(DAY, c.FechaPreparacion, @FechaActual) DifDias, b.CantidadPedida, b.AlmacenCodigo "
                + "FROM Temp_PronosticoPeriodo a "
                + "INNER JOIN EP_PedidoDetalle b ON a.ItemSpring = b.Item AND a.CompaniaSocio = b.CompaniaSocio "
                + "INNER JOIN EP_Pedido c ON b.CompaniaSocio = c.CompaniaSocio AND b.PedidoNumero = c.PedidoNumero AND c.ESTADO<> 'AN' AND c.TipoVenta = 'STP') "
                + "SELECT a.PedidoNumero, ISNULL(a.NumeroLote, 'Sin lote') NumeroLote, a.Item, a.CantidadPedida, a.FechaPreparacion, a.DifDias "
                + "FROM temp_PedidosItemPronostico a "
                + "LEFT JOIN WH_ItemAlmacenLote b ON a.Item = b.Item AND b.Condicion = 0 AND a.NumeroLote = b.Lote AND a.AlmacenCodigo = b.AlmacenCodigo "
                + "WHERE b.Item IS NULL ";

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
