using Aguila.Core.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Infrastructure.Validators
{
    public class invSubCategoriaValidator : AbstractValidator<invSubCategoriaDto>
    {
        public invSubCategoriaValidator()
        {
            RuleFor(e => e.codigo)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe estar vacío")
                .Length(1, 45).WithMessage("No se admiten más de 45 caracteres");

            RuleFor(e => e.idInvCategoria)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe estar vacío");

            RuleFor(e => e.descripcion)
                .NotNull().WithMessage("No debe estar vacío")
                .Length(1, 45).WithMessage("No debe estar vacío");
        }
    }
}
