using Aguila.Core.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Infrastructure.Validators
{
    public class vehiculosValidator : AbstractValidator<vehiculosDto>
    {
        public vehiculosValidator()
        {
            RuleFor(e => e.motor)
                .Length(1, 25).WithMessage("Motor no debe exceder los 25 caracteres.");

            RuleFor(e => e.tarjetaCirculacion)
                .Length(1, 20).WithMessage("Tarjeta de Circulacion no debe exceder los 20 caracteres.");

            RuleFor(e => e.placa)
               .Length(1, 9).WithMessage("Placa no debe exceder los 9 caracteres.");

            RuleFor(e => e.llantas)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío");

        }
    }
}
