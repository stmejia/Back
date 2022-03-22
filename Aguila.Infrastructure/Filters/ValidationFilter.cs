using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/**
 * Filtros Globales del API
 */

namespace Aguila.Infrastructure.Filters
{
    public class ValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //FIltro para validar que la peticion venga correcta (model state valid) "modelo correcto del request"
            if (!context.ModelState.IsValid)
            {

                var modelStateEntries = context.ModelState.Where(e => e.Value.Errors.Count > 0).ToArray();
                var errors = new Dictionary<string, object>();

                var details = "Detalle en validacionErrores ";

                if (modelStateEntries.Any())
                {
                    if (modelStateEntries.Length == 1 && modelStateEntries[0].Value.Errors.Count == 1 && modelStateEntries[0].Key == string.Empty)
                    {
                        details = modelStateEntries[0].Value.Errors[0].ErrorMessage;
                    }
                    else
                    {
                        foreach (var modelStateEntry in modelStateEntries)
                        {
                            var descriptions = new List<string>();                             
                            foreach (var modelStateError in modelStateEntry.Value.Errors)
                            {
                                descriptions.Add(modelStateError.ErrorMessage);
                            }
                            errors.Add(modelStateEntry.Key, descriptions);
                        }
                    }
                }

                var validation = new
                {
                    Estatus = 400,
                    Titulo = "Errores de validacion",
                    Detalle = details,
                    validacionErrores = errors
                };

                var json = new
                {
                    AguilaErrores = new[] { validation }
                };

                context.Result = new ObjectResult(json);

                context.HttpContext.Response.StatusCode = 400;                              

                return;
            }

            await next();
        }
    }

}
