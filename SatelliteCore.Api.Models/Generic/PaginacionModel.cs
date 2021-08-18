using System;
using System.Collections.Generic;

namespace SatelliteCore.Api.Models.Generic
{
    public struct PaginacionModel <T> 
    {
        public PaginacionGroupModel paginado { get; set; }
        public List<T> Contenido { get; set; }

        public PaginacionModel(List<T> contenido, int pagina, int registroPorPagina, int totalRegistros)
        {
            paginado = new PaginacionGroupModel()
            {
                PaginaActual = pagina,
                TotalPaginas = (int)Math.Ceiling((totalRegistros * 1.0) / registroPorPagina),
                RegistroPorPagina = registroPorPagina,
                TotalRegistros = totalRegistros,
                Siguiente = pagina < (int)Math.Ceiling((double)((totalRegistros * 1.0) / registroPorPagina)),
                Anterior = pagina > 1,
                PrimeraPagina = pagina > 1,
                UltimaPagina = pagina < (int)Math.Ceiling((double)((totalRegistros * 1.0) / registroPorPagina))
            };

            Contenido = contenido;
        }
    }
}

