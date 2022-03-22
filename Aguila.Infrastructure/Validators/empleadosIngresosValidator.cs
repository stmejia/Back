using Aguila.Core.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aguila.Infrastructure.Validators
{
    public class empleadosIngresosValidator : AbstractValidator<empleadosIngresosDto>
    {
        public empleadosIngresosValidator()
        {
            RuleFor(e => e.idEstacionTrabajo)
               .NotNull().WithMessage("Requerido")
               .NotEmpty().WithMessage("No debe de estár vacío");

            RuleFor(e => e.cui)
               .NotNull().WithMessage("Requerido")
               .NotEmpty().WithMessage("No debe de estár vacío");

            RuleFor(e => e.evento)
               .NotNull().WithMessage("Requerido")
               .NotEmpty().WithMessage("No debe de estár vacío");
        }
    }
}
