using Aguila.Core.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Infrastructure.Validators
{
    public class RecursosValidator : AbstractValidator<RecursosDto>
    {
        public RecursosValidator()
        {
            RuleFor(recurso => recurso.Nombre)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe estar Vacio");

            RuleFor(recurso => recurso.Tipo)
               .NotNull().WithMessage("Requerido")
               .NotEmpty().WithMessage("No debe estar Vacio");

            RuleFor(recurso => recurso.opciones)
               .NotNull().WithMessage("Requerido")
               .NotEmpty().WithMessage("No debe estar Vacio");
        }
    }
}
