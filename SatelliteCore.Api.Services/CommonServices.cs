using SatelliteCore.Api.DataAccess.Contracts.Repository;
using SatelliteCore.Api.Models.Entities;
using SatelliteCore.Api.Models.Response;
using SatelliteCore.Api.Services.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SatelliteCore.Api.Services
{
    public class CommonServices: ICommonServices
    {
        private readonly ICommonRepository _commonRepository;

        public CommonServices(ICommonRepository commonRepository)
        {
            _commonRepository = commonRepository;
        }

        public async Task<IEnumerable<TipoDocumentoIdentidadEntity>> ListarTipoDocumentoIndentidad()
        {
            return await _commonRepository.ListarTipoDocumentoIndentidad();
        }

        public async Task<IEnumerable<PaisEntity>> ListarPaises()
        {
            return await _commonRepository.ListarPaises();
        }

        public async Task<List<MenuxUsuarioModel>> ListarMenuxUsuario(int usuario)
        {
            List<MenuEntity> listaMenu = await _commonRepository.ListarMenuxUsuario(usuario);

            List<MenuxUsuarioModel> menuResponse = new List<MenuxUsuarioModel>();

            int index = -1;

            foreach (MenuEntity menu in listaMenu)
            {
                if (menu.MenuPadre == 0)
                {
                    menuResponse.Add(new MenuxUsuarioModel()
                    {
                        Codigo = menu.Codigo,
                        Ruta = menu.Ruta,
                        Titulo = menu.Titulo,
                        Icono = menu.Icono,
                        Clase = menu.Clase,
                        ClaseEtiqueta = menu.ClaseEtiqueta,
                        ExtraLink = menu.ExtraLink,
                        MenuPadre = menu.MenuPadre,
                        subMenu = new List<MenuxUsuarioModel>()
                    });
                }
                else
                {
                    index =  menuResponse.FindIndex(x => x.Codigo == menu.MenuPadre && x.MenuPadre == 0);

                    if(index != -1)
                    {
                        menuResponse.Add(new MenuxUsuarioModel()
                        {
                            Codigo = menu.Codigo,
                            Ruta = menu.Ruta,
                            Titulo = menu.Titulo,
                            Icono = menu.Icono,
                            Clase = menu.Clase,
                            ClaseEtiqueta = menu.ClaseEtiqueta,
                            ExtraLink = menu.ExtraLink,
                            MenuPadre = menu.MenuPadre,
                            subMenu = new List<MenuxUsuarioModel>()
                        });
                    }
                    else
                    {
                        index = -1;

                        index = menuResponse.FindIndex(x => x.Codigo == menu.MenuPadre && x.MenuPadre != 0);

                        if (index != -1)
                        {
                            menuResponse[index].subMenu.Add(new MenuxUsuarioModel()
                            {
                                Codigo = menu.Codigo,
                                Ruta = menu.Ruta,
                                Titulo = menu.Titulo,
                                Icono = menu.Icono,
                                Clase = menu.Clase,
                                ClaseEtiqueta = menu.ClaseEtiqueta,
                                ExtraLink = menu.ExtraLink,
                                MenuPadre = menu.MenuPadre,
                                subMenu = new List<MenuxUsuarioModel>()
                            });
                        }

                        else {
                            menuResponse.Add(new MenuxUsuarioModel()
                            {
                                Codigo = menu.Codigo,
                                Ruta = menu.Ruta,
                                Titulo = menu.Titulo,
                                Icono = menu.Icono,
                                Clase = menu.Clase,
                                ClaseEtiqueta = menu.ClaseEtiqueta,
                                ExtraLink = menu.ExtraLink,
                                MenuPadre = menu.MenuPadre,
                                subMenu = new List<MenuxUsuarioModel>()
                            });
                        }
                    }

                    index = -1;
                }
            }

            return menuResponse;
        }

        public async Task<IEnumerable<RolEntity>> ListarRoles(string estado)
        {
            return await _commonRepository.ListarRoles(estado);
        }
    }
}
