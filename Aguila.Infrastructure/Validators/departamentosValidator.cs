using Aguila.Core.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Infrastructure.Validators
{
    public class departamentosValidator : AbstractValidator<departamentosDto>
    {
        public departamentosValidator()
        {
            RuleFor(e => e.idPais)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío");

            RuleFor(e => e.codigo)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío")
                .Length(1, 5).WithMessage("No se admiten más de 5 caracteres");

            RuleFor(e => e.nombre)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío")
                .Length(1, 25).WithMessage("No se admiten más de 25 caracteres");

            //RuleFor(e => e.fechaCreacion)
            //    .NotNull().WithMessage("Requerido")
            //    .NotEmpty().WithMessage("El campo fecha no debe de estar vacío");

        }
    }
}
