using Aguila.Core.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Net;

namespace Aguila.Infrastructure.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception.GetType() == typeof(Microsoft.EntityFrameworkCore.DbUpdateException))
            {
                // Aqui validamos errores de duplicacion de registros, cuya restriccion este en la base de datos, indices unicos.
                var validation = new
                {
                    Estatus = 500,
                    Titulo = "Error Interno",
                    Detalle = context.Exception.InnerException.Message.ToString().Trim()
                };

                var json = new
                {
                    AguilaErrores = new[] { validation }
                };

                //context.Result = new BadRequestObjectResult(json);
                context.Result = new ObjectResult(json);
                context.HttpContext.Response.StatusCode = 500;
                context.ExceptionHandled = true;
            }

            if (context.Exception.GetType() == typeof(AguilaException))
            {
                var exception = (AguilaException)context.Exception;

                string tittle = "Peticion erronea";

                switch (exception.status)
                {
                    case 404:
                        tittle = "Recurso no encontrado";
                        break;

                    case 423:
                        tittle = "Recurso bloqueado";
                        break;

                    case 428:
                        tittle = "Pre requisito necesario";
                        break;
                    case 406:
                        tittle = "Validacion incorrecta";
                        break;
                    default:
                        // bad request
                        tittle = "Peticion erronea";
                        break;
                }

                var validation = new
                {
                    Estatus = exception.status,
                    Titulo = tittle,
                    Detalle = exception.Message
                };

                var json = new
                {
                    AguilaErrores = new[] { validation }
                };

                context.Result = new ObjectResult(json);
                context.HttpContext.Response.StatusCode = exception.status;
                context.ExceptionHandled = true;
            }
        }
    }
}
