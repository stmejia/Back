using Aguila.Core.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Infrastructure.Validators
{
    public class asesoresValidator : AbstractValidator<asesoresDto>
    {
        public asesoresValidator()
        {
            RuleFor(e => e.codigo)
              .NotNull().WithMessage("Requerido")
              .NotEmpty().WithMessage("No debe de estar vacío")
              .Length(1, 10).WithMessage("No se admiten más de 10 caracteres");

            RuleFor(e => e.nombre)
              .NotNull().WithMessage("Requerido")
              .NotEmpty().WithMessage("No debe de estar vacío")
              .Length(1, 45).WithMessage("No se admiten más de 45 caracteres");

            RuleFor(e => e.idEmpleado)
              .NotNull().WithMessage("Requerido")
              .NotEmpty().WithMessage("No debe de estar vacío");
        }
    }
}
