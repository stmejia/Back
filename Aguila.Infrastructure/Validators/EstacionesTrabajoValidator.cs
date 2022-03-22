using Aguila.Core.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Infrastructure.Validators
{
    public class EstacionesTrabajoValidator : AbstractValidator<EstacionesTrabajoDto>
    {
        public EstacionesTrabajoValidator()
        {
            RuleFor(estacion => estacion.Tipo)
               .NotNull().WithMessage("Requerido")
               .NotEmpty().WithMessage("No debe estar Vacio");

            RuleFor(estacion => estacion.Nombre)
               .NotNull().WithMessage("Requerido")
               .NotEmpty().WithMessage("No debe estar Vacio");

            RuleFor(estacion => estacion.Activa)
               .NotNull().WithMessage("Requerido")
               .NotEmpty().WithMessage("No debe estar Vacio");
        }
    }
}
