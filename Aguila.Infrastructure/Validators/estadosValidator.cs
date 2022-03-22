using Aguila.Core.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Infrastructure.Validators
{
    public class estadosValidator : AbstractValidator<estadosDto>
    {
        public estadosValidator()
        {
            RuleFor(e => e.idEmpresa)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío");

            RuleFor(e => e.codigo)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío")
                .Length(1, 5).WithMessage("No se admiten más de 5 caracteres");

            RuleFor(e => e.tipo)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío")
                .Length(1, 30).WithMessage("No se admiten más de 30 caracteres");

            RuleFor(e => e.nombre)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío")
                .Length(1, 50).WithMessage("No se admiten más de 50 caracteres");

            RuleFor(e => e.numeroOrden)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío");

            RuleFor(e => e.evento)
                //.NotNull().WithMessage("Requerido")
                //.NotEmpty().WithMessage("No debe de estar vacío")
                .Length(1, 100).WithMessage("No se admiten más de 100 caracteres");
        }
    }
}
