using Aguila.Core.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Infrastructure.Validators
{
    public class tipoReparacionesValidator : AbstractValidator<tipoReparacionesDto>
    {
        public tipoReparacionesValidator()
        {
            RuleFor(t => t.codigo)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe estar Vacio")
                .Length(1, 5).WithMessage("se adminte un maximo de 5 caracteres");

            RuleFor(t => t.nombre)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe estar Vacio")
                .Length(1, 25).WithMessage("se adminte un maximo de 25 caracteres");

            RuleFor(t => t.descripcion)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe estar Vacio")
                .Length(1, 30).WithMessage("se adminte un maximo de 30 caracteres");

        }
    }
}
