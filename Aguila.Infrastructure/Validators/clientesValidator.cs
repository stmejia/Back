using Aguila.Core.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Infrastructure.Validators
{
    public class clientesValidator : AbstractValidator<clientesDto>
    {
        public clientesValidator()
        {
            RuleFor(e => e.idTipoCliente)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío");

            RuleFor(e => e.codigo)
              .NotNull().WithMessage("Requerido")
              .NotEmpty().WithMessage("No debe de estar vacío")
              .Length(1, 5).WithMessage("No se admiten más de 5 caracteres");
            

            //RuleFor(e => e.idDireccion)
            //    .NotNull().WithMessage("Requerido")
            //    .NotEmpty().WithMessage("No debe de estar vacío");

            //RuleFor(e => e.idEntidadComercial)
            //    .NotNull().WithMessage("Requerido")
            //    .NotEmpty().WithMessage("No debe de estar vacío");

            //RuleFor(e => e.idCorporacion)
            //    .NotNull().WithMessage("Requerido")
            //    .NotEmpty().WithMessage("No debe de estar vacío");

            RuleFor(e => e.diasCredito)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío");

        }
    }
}
