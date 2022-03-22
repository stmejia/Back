using Aguila.Core.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Infrastructure.Validators
{
    public class tipoVehiculosValidator : AbstractValidator<tipoVehiculosDto>
    {
        public tipoVehiculosValidator()
        {
            RuleFor(e => e.codigo)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío")
                .Length(1, 5).WithMessage("No se admiten más de 5 caracteres");

           RuleFor(e => e.idEmpresa)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío");

            RuleFor(e => e.descripcion)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío")
                .Length(1, 30).WithMessage("No se admiten más de 30 caracteres");

            RuleFor(e => e.prefijo)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío")
                .Length(3, 8).WithMessage("Se admiten valores entre 3 y 8 caracteres");

            //RuleFor(e => e.correlativoLongitud)
            //    .NotNull().WithMessage("Requerido")
            //    .NotEmpty().WithMessage("No debe de estar vacío");

            //RuleFor(e => e.estructuraCoc)
            //   //.NotNull().WithMessage("Requerido")
            //   //.NotEmpty().WithMessage("No debe de estar vacío")
            //   .Length(1, 250).WithMessage("Se admiten como maximo 250 caracteres");
        }
    }
}
