using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SatelliteCore.Api.Services.Contracts;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SatelliteCore.Api.Filters
{
    public class PermitRequirement : IAuthorizationRequirement
    {
        public string Permiso { get; private set; }

        public PermitRequirement(string Permiso)
        {
            this.Permiso = Permiso;
        }
        
    }

    public class CustomPermitRequirementHandler : AuthorizationHandler<PermitRequirement>
    {
        private readonly IValidacionesServices _validacionesServices;
        public CustomPermitRequirementHandler(IValidacionesServices validacionesServices)
        {
            _validacionesServices = validacionesServices;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermitRequirement requirement)
        {
            try
            {
                var usuario = int.Parse(context.User.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value);                

                bool result = await _validacionesServices.ValidarPermisoAcceso(usuario, requirement.Permiso);

                if (result)
                {
                    context.Succeed(requirement);
                    return;
                }

                context.Fail();
            }
            catch (Exception)
            {
                context.Fail();
            }
            
        }
    }

    public class PermitCodeAttribute : AuthorizeAttribute, IAsyncAuthorizationFilter
    {
        private string permiso;

        public PermitCodeAttribute(string permiso)
        {
            this.permiso = permiso;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var service = (IAuthorizationService)context.HttpContext.RequestServices.GetService(typeof(IAuthorizationService));

            var roleRequirement = new PermitRequirement(this.permiso);
            var result = await service.AuthorizeAsync(context.HttpContext.User, null, roleRequirement);

            if (!result.Succeeded)
            {
                context.Result = new ForbidResult();
            }
        }
    }


}
