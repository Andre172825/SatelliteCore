using System.ComponentModel.DataAnnotations;

namespace SatelliteCore.Api.Models.Request
{
    public struct DatosListarCertificadoPaginado
    {
        public string OrdenServicio { get; set; }
        public string Codigo { get; set; }
        [Required]
        public int Pagina { get; set; }
        [Required]
        public int RegistrosPorPagina { get; set; }
    }
}
