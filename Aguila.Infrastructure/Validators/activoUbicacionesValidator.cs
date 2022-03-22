using Aguila.Core.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Infrastructure.Validators
{
    public class activoUbicacionesValidator : AbstractValidator<activoUbicacionesDto>
    {
        public activoUbicacionesValidator()
        {
            RuleFor(e => e.idActivo)
              .NotNull().WithMessage("Requerido")
              .NotEmpty().WithMessage("No debe de estar vacío");

            RuleFor(e => e.idUbicacion)
              .NotNull().WithMessage("Requerido")
              .NotEmpty().WithMessage("No debe de estar vacío");

            RuleFor(e => e.observaciones)
              .NotNull().WithMessage("Requerido")
              .NotEmpty().WithMessage("No debe de estar vacío")
              .Length(1, 150).WithMessage("No se admiten más de 150 caracteres");
        }
    }
}
