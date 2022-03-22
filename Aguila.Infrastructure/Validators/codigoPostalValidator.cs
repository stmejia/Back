using Aguila.Core.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aguila.Infrastructure.Validators
{
    public class codigoPostalValidator : AbstractValidator<codigoPostalDto>
    {
        public codigoPostalValidator()
        {
            RuleFor(e => e.idMunicipio)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío");

            RuleFor(e => e.codigo)
              .NotNull().WithMessage("Requerido")
              .NotEmpty().WithMessage("No debe de estar vacío")
              .Length(1, 25).WithMessage("No se admiten más de 25 caracteres");

        }
    }
}
