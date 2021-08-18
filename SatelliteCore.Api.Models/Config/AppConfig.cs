using Microsoft.Extensions.Configuration;

namespace SatelliteCore.Api.Models.Config
{
    public class AppConfig : IAppConfig
    {
        private readonly IConfiguration _configuration;
        public AppConfig(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string contextSatelliteDB => _configuration.GetSection("ConnectionStrings:SatelliteContext").Value;
        public string contextSpring => _configuration.GetSection("ConnectionStrings:SpringContext").Value;

        public int ExpirationTimeInHour => int.Parse(_configuration.GetSection("JWTValidationParameters:ExpTimeInHour").Value);
        public string JWTSecretKey => _configuration.GetSection("JWTValidationParameters:SecretKey").Value;
        public string JWTIssuer => _configuration.GetSection("JWTValidationParameters:Issuer").Value;
        public string JWTAudience => _configuration.GetSection("JWTValidationParameters:Audience").Value;

    }
}
