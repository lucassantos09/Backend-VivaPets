using Dominio.Servicos;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Api.Auth
{
    public sealed class AutorizarAttribute : Attribute, IAuthorizationFilter
    {
        ServicoToken _servicoToken = new ServicoToken();
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context != null)
            {
                // Auth logic
                var bearer = context.HttpContext.Request.Headers["Authorization"].ToString();
                var token = bearer.Split("Bearer")[1];

                var sub = _servicoToken.ValidarToken(token);

                if (sub == null)
                {
                    context.Result =  new UnauthorizedResult();
                }
            }
        }
    }
}
