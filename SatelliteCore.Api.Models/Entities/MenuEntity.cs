
namespace SatelliteCore.Api.Models.Entities
{
    public class MenuEntity
    {
        public int Codigo { get; set; }
        public string Ruta { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string Icono { get; set; }
        public string Clase { get; set; }
        public bool ExtraLink { get; set; }
        public string ClaseEtiqueta { get; set; }
        public string Estado { get; set; }
        public int MenuPadre { get; set; }
        public int Orden { get; set; }
        public string Permiso { get; set; }
        public bool FlagMenuArbol { get; set; }
    }
}
