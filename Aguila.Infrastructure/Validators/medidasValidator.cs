using Aguila.Core.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Infrastructure.Validators
{
    public class medidasValidator : AbstractValidator<medidasDto>
    {
        public medidasValidator()
        {
            RuleFor(e => e.codigo)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío")
                .Length(1, 45).WithMessage("No se admiten más de 45 caracteres");

            RuleFor(e => e.nombre)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío")
                .Length(1, 50).WithMessage("No se admiten más de 50 caracteres");
        }
    }
}
