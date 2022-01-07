
namespace SatelliteCore.Api.Models.Config
{
    public interface IAppConfig
    {
        string contextSatelliteDB { get; }
        string JWTSecretKey { get; }
        int ExpirationTimeInHour { get; }
        string contextSpring { get; }
        string JWTIssuer { get; }
        string JWTAudience { get; }
        string ReportControlDeCalidad { get; }
        string ReportComercialFormatoCotizacion { get; }
        string ReportComercialProtocoloAnalisis { get; }
        string ReportRRHH { get; }
    }
}
