using SatelliteCore.Api.Models.Response;
using System.Collections.Generic;

namespace SatelliteCore.Api.Models.Generic
{
    public struct SeguimientoCandMPAGenericModel
    {
       public IEnumerable<SeguimientoCandMPAModel> SeguimientoCandidatosMPA { get; set; }
       public IEnumerable<DetalleSeguimientoCandMPAModel> OrdenComprasPendientes { get; set; }
    }
}
