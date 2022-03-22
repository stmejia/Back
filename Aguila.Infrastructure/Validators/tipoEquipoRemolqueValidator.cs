using Aguila.Core.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Infrastructure.Validators
{
    public class tipoEquipoRemolqueValidator : AbstractValidator<tipoEquipoRemolqueDto>
    {
        public tipoEquipoRemolqueValidator()
        {
            RuleFor(e => e.codigo)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío")
                .Length(1, 5).WithMessage("No se admiten más de 5 caracteres");

            RuleFor(e => e.descripcion)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío")
                .Length(1, 30).WithMessage("No se admiten más de 30 caracteres");

            RuleFor(e => e.idEmpresa)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío");

            RuleFor(e => e.prefijo)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío")
                .Length(1, 8).WithMessage("No se admiten más de 8 caracteres");

            RuleFor(e => e.estructuraCoc)
                //.NotNull().WithMessage("Requerido")
               // .NotEmpty().WithMessage("No debe de estar vacío")
                .Length(1, 250).WithMessage("No se admiten más de 250 caracteres");
        }
    }
}
