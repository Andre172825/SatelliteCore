
namespace SatelliteCore.Api.Models.Generic
{
    public struct PaginacionGroupModel
    {
        public int PaginaActual { get; set; }
        public int TotalPaginas { get; set; }
        public int RegistroPorPagina { get; set; }
        public int TotalRegistros { get; set; }
        public bool Siguiente { get; set; }
        public bool Anterior { get; set; }
        public bool PrimeraPagina { get; set; }
        public bool UltimaPagina { get; set; }
    }
}
