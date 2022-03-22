using Aguila.Core.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Infrastructure.Validators
{
    public class direccionesValidator : AbstractValidator<direccionesDto>
    {
        public direccionesValidator()
        {
            RuleFor(e => e.idMunicipio)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío");

            RuleFor(e => e.colonia)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe estar vacío")
                .Length(1, 30).WithMessage("No se admiten más de 30 caracteres");

            RuleFor(e => e.zona)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe estar vacío")
                .Length(1, 20).WithMessage("No se admiten más de 20 caracteres");

            RuleFor(e => e.codigoPostal)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe estar vacío")
                .Length(1, 5).WithMessage("No se admiten más de 5 caracteres");

            RuleFor(e => e.direccion)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe estar vacío")
                .Length(1, 100).WithMessage("No se admiten más de 100 caracteres");
        }
    }
}
