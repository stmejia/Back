using Aguila.Core.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Infrastructure.Validators
{
    public class llantaTiposValidator : AbstractValidator<llantaTiposDto>
    {
        public llantaTiposValidator()
        {
            RuleFor(e => e.codigo)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío")
                .Length(1, 10).WithMessage("No se admiten más de 10 caracteres");

            RuleFor(e => e.descripcion)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío")
                .Length(1, 50).WithMessage("No se admiten más de 50 caracteres");

            //RuleFor(e => e.proposito)
            //    .NotNull().WithMessage("Requerido")
            //    .NotEmpty().WithMessage("No debe de estar vacío");
        }
    }
}
