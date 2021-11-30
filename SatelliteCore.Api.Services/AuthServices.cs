using Microsoft.IdentityModel.Tokens;
using SatelliteCore.Api.Models.Config;
using SatelliteCore.Api.Models.Request;
using SatelliteCore.Api.Models.Response;
using SatelliteCore.Api.Services.Contracts;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SatelliteCore.Api.Services
{
    public class AuthServices : IAuthServices
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IAppConfig _appConfig;
        public AuthServices(IUsuarioService usuarioService, IAppConfig appConfig) {
            _usuarioService = usuarioService;
            _appConfig = appConfig;
        }
        
        public async Task<AuthResponse> AutenticacionUsuario(AuthRequestModel datosUsuario)
        {
            AuthResponse usuario = await _usuarioService.ObtenerUsuarioLogin(datosUsuario);

            if (usuario == null) return null;

            usuario.Token = ObtenerToken(usuario);

            return usuario;
        }

        private string ObtenerToken(AuthResponse user)
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appConfig.JWTSecretKey));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);
            var header = new JwtHeader(signingCredentials);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.CodUsuario.ToString()),
                new Claim(ClaimTypes.Name, user.Nombres),
                new Claim(ClaimTypes.GivenName, user.ApellidoPaterno),
                new Claim(ClaimTypes.Email, user.Correo)
            };

            var payload = new JwtPayload(
                _appConfig.JWTIssuer,
                _appConfig.JWTAudience,
                claims,
                DateTime.Now,
                DateTime.UtcNow.AddHours(_appConfig.ExpirationTimeInHour)
            );

            JwtSecurityToken token = new JwtSecurityToken(header, payload);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
