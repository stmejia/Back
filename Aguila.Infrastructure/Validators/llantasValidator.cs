using Aguila.Core.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Infrastructure.Validators
{
    public class llantasValidator : AbstractValidator<llantasDto>
    {
        public llantasValidator()
        {
            RuleFor(e => e.idEstadoIngreso)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío");

            RuleFor(e => e.proveedorId)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío");

            RuleFor(e => e.idLlantaTipo)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío");

            RuleFor(e => e.codigo)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío")
                .Length(1, 10).WithMessage("No se admiten más de 10 caracteres");

            RuleFor(e => e.marca)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío")
                .Length(1, 15).WithMessage("No se admiten más de 15 caracteres");

            RuleFor(e => e.serie)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío")
                .Length(1, 25).WithMessage("No se admiten más de 25 caracteres");

            RuleFor(e => e.reencaucheIngreso)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío")
                .Length(1, 20).WithMessage("No se admiten más de 20 caracteres");

            RuleFor(e => e.medidas)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío")
                .Length(1, 15).WithMessage("No se admiten más de 15 caracteres");

            RuleFor(e => e.precio)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío");

            //RuleFor(e => e.fechaIngreso)
            //    .NotNull().WithMessage("Requerido")
            //    .NotEmpty().WithMessage("No debe de estar vacío");

            //RuleFor(e => e.fechaIngreso)
            //    .NotNull().WithMessage("Requerido")
            //    .NotEmpty().WithMessage("No debe de estar vacío");

            RuleFor(e => e.propositoIngreso)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío")
                .Length(1, 15).WithMessage("No se admiten más de 15 caracteres");
        }
    }
}
