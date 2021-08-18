using System.Collections.Generic;

namespace SatelliteCore.Api.Models.Response
{
    public class MenuxUsuarioModel
    {
        public int Codigo { get; set; }
        public string Ruta { get; set; }
        public string Titulo { get; set; }
        public string Icono { get; set; }
        public string Clase { get; set; }
        public string ClaseEtiqueta { get; set; }
        public bool ExtraLink { get; set; }
        public int MenuPadre { get; set; }
        public List<MenuxUsuarioModel> subMenu { get; set; }
    }
}
