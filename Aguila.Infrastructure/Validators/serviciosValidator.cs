using Aguila.Core.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Infrastructure.Validators
{
    public class serviciosValidator : AbstractValidator<serviciosDto>
    {
        public serviciosValidator()
        {
            RuleFor(e => e.idEmpresa)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe estar vacío");

            RuleFor(e => e.codigo)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe estar vacío")
                .Length(1, 15).WithMessage("No se admiten más de 10 caracteres");

            RuleFor(e => e.nombre)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe estar vacío")
                .Length(1, 30).WithMessage("No se admiten más de 30 caracteres");

            RuleFor(e => e.precio)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe estar vacío");
        }
    }
}
