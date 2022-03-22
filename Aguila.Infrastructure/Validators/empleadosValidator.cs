using Aguila.Core.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Infrastructure.Validators
{
    public class empleadosValidator : AbstractValidator<empleadosDto>
    {
        public empleadosValidator()
        {
            //RuleFor(e => e.codigo)
            //    //.NotNull().WithMessage("Requerido")
            //    //.NotEmpty().WithMessage("No debe de estár vacío")
            //    .Length(1, 10).WithMessage("No se admiten más de 10 caracteres");

            RuleFor(e => e.nombres)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacio")
                .Length(1, 50).WithMessage("No se admiten más de 50 caracteres");

            //RuleFor(e => e.apellidos)
            //    .NotNull().WithMessage("Requerido")
            //    .NotEmpty().WithMessage("No debe de estar vacio")
            //    .Length(1, 50).WithMessage("No se admiten más de 50 caracteres");

            RuleFor(e => e.dpi)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío")
                .Length(1, 50).WithMessage("No se admiten más de 50 caracteres");

            //RuleFor(e => e.idDireccion)
            //    .NotNull().WithMessage("Requerido")
            //    .NotEmpty().WithMessage("No debe de estar vacío");

            //RuleFor(e => e.correlativo)
            //    //.NotNull().WithMessage("Requerido")
            //    //.NotEmpty().WithMessage("No debe de estár vacío")
            //    .Length(1, 4).WithMessage("No se admiten más de 4 caracteres");

            //RuleFor(e => e.cop)
            //    //.NotNull().WithMessage("Requerido")
            //    //.NotEmpty().WithMessage("No debe de estár vacío")
            //    .Length(1, 30).WithMessage("No se admiten más de 30 caracteres");

            //RuleFor(e => e.telefono)
            //    .NotNull().WithMessage("Requerido")
            //    .NotEmpty().WithMessage("No debe de estar vacío");

            RuleFor(e => e.fechaAlta)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío");

            RuleFor(e => e.fechaNacimiento)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío");

            RuleFor(e => e.idEmpresa)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío");
        }
    }
}
