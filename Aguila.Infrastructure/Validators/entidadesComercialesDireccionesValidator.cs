using Aguila.Core.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Infrastructure.Validators
{
    public class entidadesComercialesDireccionesValidator : AbstractValidator<entidadesComercialesDireccionesDto>
    {
        public entidadesComercialesDireccionesValidator()
        {
            RuleFor(e => e.idEntidadComercial)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío");

            //RuleFor(e => e.idDireccion)
            //    .NotNull().WithMessage("Requerido")
            //    .NotEmpty().WithMessage("No debe de estar vacío");

            RuleFor(e => e.descripcion)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío")
                .Length(1, 25).WithMessage("No se admiten más de 25 caracteres");

        }
    }
}
