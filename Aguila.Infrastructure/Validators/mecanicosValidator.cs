using Aguila.Core.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Infrastructure.Validators
{
    public class mecanicosValidator : AbstractValidator<mecanicosDto>
    {
        public mecanicosValidator()
        {
            RuleFor(e => e.codigo)
              .NotNull().WithMessage("Requerido")
              .NotEmpty().WithMessage("No debe de estar vacío")
              .Length(1, 45).WithMessage("No se admiten más de 45 caracteres");

            RuleFor(e => e.idTipoMecanico)
              .NotNull().WithMessage("Requerido")
              .NotEmpty().WithMessage("No debe de estar vacío");

            RuleFor(e => e.idEmpleado)
              .NotNull().WithMessage("Requerido")
              .NotEmpty().WithMessage("No debe de estar vacío");
        }
    }
}
