using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using SatelliteCore.Api.DataAccess.Contracts.Repository;
using SatelliteCore.Api.Models.Config;
using SatelliteCore.Api.Models.Entities;
using SatelliteCore.Api.Models.Request;
using SatelliteCore.Api.Models.Response;

namespace SatelliteCore.Api.DataAccess.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly IAppConfig _appConfig;

        public UsuarioRepository(IAppConfig appConfig)
        {
            _appConfig = appConfig;
        }

        public async Task<UsuarioEntity> ObtenerUsuario(DatoUsuarioBasico datos)
        {
            UsuarioEntity usuario = new UsuarioEntity ();

            using (var connection = new SqlConnection(_appConfig.contextSatelliteDB))
            {
                string script = "SELECT IDUsuario, Nombre, ApellidoPaterno, ApellidoMaterno, TipoDocumento, " +
                    "NroDocumento, Sexo, Pais, Correo, Estado, FechaNacimiento, Celular FROM TBMUsuario " +
                    "WHERE IDUsuario = @IdUsuario AND ApellidoPaterno = @Apellido";
                usuario = await connection.QueryFirstOrDefaultAsync<UsuarioEntity>(script, new { datos.IdUsuario, datos.Apellido });

                connection.Dispose();
            }

            return usuario;
        }

        public async Task<AuthResponse> ObtenerUsuarioLogin(AuthRequestModel datosUsuario)
        {
           
            AuthResponse usuario = new AuthResponse();

            using (var connection = new SqlConnection(_appConfig.contextSatelliteDB))
            {
                string sql = "SELECT a.CodUsuario, RTRIM(b.Nombres) Nombres, RTRIM(b.ApellidoPaterno) ApellidoPaterno, a.Correo FROM TBMUsuario a " +
                    " INNER JOIN PROD_UNILENE2.dbo.PersonaMast b ON a.CodUsuario = b.Persona " +
                    "WHERE a.Estado = 'A' AND b.Estado = 'A' AND Usuario = @Usuario AND Clave = @Clave";
                usuario = await connection.QueryFirstOrDefaultAsync<AuthResponse>(sql, datosUsuario);

                connection.Dispose();
            }
            return usuario;

        }

        public async Task<int> CambiarClave(ActualizarClaveModel datos)
        {
            int result = 0;

            using (var connection = new SqlConnection(_appConfig.contextSatelliteDB))
            {
                string sql = "UPDATE TBMUsuario SET Clave = @clave, FlagCambioClave = @exigirCambioClave" +
                    "WHERE IDUsuario = @idUsuario AND NroDocumento = @nroDocumento AND Estado = 'A'";

                result = await connection.ExecuteAsync(sql, datos);
                connection.Dispose();
            }

            return result;
        }

        public async Task<(List<UsuarioEntity>, int)> ListarUsuarios(DatosListarUsuarioPaginado datos)
        {
            (List<UsuarioEntity> ListaUsuarios, int totalRegistros) result;

            using (var connection = new SqlConnection(_appConfig.contextSatelliteDB))
            {
                using (var result_db = await connection.QueryMultipleAsync("usp_listar_Usuario", datos, commandType: CommandType.StoredProcedure))
                {
                    result.ListaUsuarios = result_db.Read<UsuarioEntity>().ToList();
                    result.totalRegistros = result_db.Read<int>().First();
                }

                connection.Dispose();
            }

            return result;
        }
       
    }
}
