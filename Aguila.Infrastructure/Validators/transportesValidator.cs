    using Aguila.Core.DTOs;
    using FluentValidation;
    using System;
    using System.Collections.Generic;
    using System.Text;

namespace Aguila.Infrastructure.Validators
{
    public class transportesValidator : AbstractValidator<transportesDto>
    {
        public transportesValidator()
        {
            RuleFor(e => e.codigo)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío")
                .Length(1, 10).WithMessage("No se admiten más de 10 caracteres");

            RuleFor(e => e.nombre)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío")
                .Length(1, 30).WithMessage("No se admiten más de 30 caracteres");

            RuleFor(e => e.idProveedor)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío");
        }
    }
}
