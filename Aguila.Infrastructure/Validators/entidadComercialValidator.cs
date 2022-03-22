using Aguila.Core.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Infrastructure.Validators
{
    public class entidadComercialValidator : AbstractValidator<entidadComercialDto>
    {
        public entidadComercialValidator()
        {
            RuleFor(e => e.nombre)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío")
                .Length(1, 100).WithMessage("No se admiten más de 100 caracteres");

            RuleFor(e => e.razonSocial)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío")
                .Length(1, 150).WithMessage("No se admiten más de 150 caracteres");

            RuleFor(e => e.idDireccionFiscal)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío");

            //RuleFor(e => e.tipo)
            //    .NotNull().WithMessage("Requerido")
            //    .NotEmpty().WithMessage("No debe de estar vacío")
            //    .Length(1).WithMessage("No se admite más de 1 caracter");

            RuleFor(e => e.nit)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío")
                .Length(1, 30).WithMessage("No se admiten más de 30 caracteres");

            RuleFor(e => e.tipoNit)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío")
                .Length(1).WithMessage("No se admite más de 1 caracter");

        }
    }
}
