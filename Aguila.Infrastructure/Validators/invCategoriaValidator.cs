using Aguila.Core.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Infrastructure.Validators
{
    public class invCategoriaValidator : AbstractValidator<invCategoriaDto>
    {
        public invCategoriaValidator()
        {
            RuleFor(e => e.codigo)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe estar vacío")
                .Length(1, 45).WithMessage("No se admiten más de 45 caracteres");

            RuleFor(e => e.descripcion)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe estar vacío")
                .Length(1, 45).WithMessage("No se admiten más de 45 caracteres");

            RuleFor(e => e.idEmpresa)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe estár vacío");
        }
    }
}
