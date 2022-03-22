using Aguila.Core.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Infrastructure.Validators
{
    public class pilotosValidator : AbstractValidator<pilotosDto>
    {
        public pilotosValidator()
        {
            RuleFor(e => e.idTipoPilotos)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío");

            RuleFor(e => e.idEmpleado)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío");

            RuleFor(e => e.codigoPiloto)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío")
                .Length(1, 10).WithMessage("No se admiten más de 10 caracteres");
        }
    }
}
