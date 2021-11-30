using Dapper;
using SatelliteCore.Api.DataAccess.Contracts.Repository;
using SatelliteCore.Api.Models.Config;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using SatelliteCore.Api.Models.Request;

namespace SatelliteCore.Api.DataAccess.Repository
{
    public class AccesosRepository : IAccesosRepository
    {
        private readonly IAppConfig _appConfig;

        public AccesosRepository(IAppConfig appConfig)
        {
            _appConfig = appConfig;
        }

        public async Task<(int codigo, string mensaje)> ValidarAccesoRuta(ValidacionRutaDataModel datos)
        {

            (int codigo, string mensaje) result;

            using (var satelliteContext = new SqlConnection(_appConfig.contextSatelliteDB))
            {
                result = await satelliteContext.QuerySingleAsync<(int codigo, string mensaje)>("usp_Validacion_AccesoRutaSatelite",
                                    datos, commandType: CommandType.StoredProcedure);
                satelliteContext.Dispose();
            }

            return result;

        }

        public async Task<bool> ValidarPermisoAcceso(int usuario, string permiso)
        {
            bool result;

            using (var satelliteContext = new SqlConnection(_appConfig.contextSatelliteDB))
            {
                result = await satelliteContext.QuerySingleAsync<bool>("usp_validacion_PermisoAcceso",
                                    new { usuario, permiso }, commandType: CommandType.StoredProcedure);
                satelliteContext.Dispose();
            }

            return result;
        }
    }
}
