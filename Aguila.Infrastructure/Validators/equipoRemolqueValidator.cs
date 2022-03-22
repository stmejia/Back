using Aguila.Core.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;


namespace Aguila.Infrastructure.Validators
{
    public class equipoRemolqueValidator : AbstractValidator<equipoRemolqueDto>
    {
        public equipoRemolqueValidator()
        {
            //RuleFor(e => e.idActivo)
            //    .NotNull().WithMessage("Requerido");

            RuleFor(e => e.idTipoEquipoRemolque)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío");

            RuleFor(e => e.noEjes)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío");

            RuleFor(e => e.tarjetaCirculacion)
              .NotNull().WithMessage("Requerido")
              .NotEmpty().WithMessage("No debe de estar vacío")
              .Length(1, 20).WithMessage("No se admiten más de 20 caracteres");

            RuleFor(e => e.placa)
              .NotNull().WithMessage("Requerido")
              .NotEmpty().WithMessage("No debe de estar vacío")
              .Length(1, 9).WithMessage("No se admiten más de 9 caracteres");


        }
    }
}
