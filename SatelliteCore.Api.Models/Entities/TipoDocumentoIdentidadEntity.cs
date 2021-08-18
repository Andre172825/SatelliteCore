
namespace SatelliteCore.Api.Models.Entities
{
    public struct TipoDocumentoIdentidadEntity
    {
        public int Codigo { get; set; }
        public string Descripcion { get; set; }
        public string Abreviatura { get; set; }
        public int Longitud { get; set; }
        public bool FlagLongExacta { get; set; }
    }
}
