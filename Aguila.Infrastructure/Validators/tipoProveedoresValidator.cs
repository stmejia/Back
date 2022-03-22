using Aguila.Core.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Infrastructure.Validators
{
    public class tipoProveedoresValidator : AbstractValidator<tipoProveedoresDto>
    {
        public tipoProveedoresValidator()
        {
            RuleFor(t => t.codigo)
               .NotNull().WithMessage("Requerido")
               .NotEmpty().WithMessage("No debe estar Vacio")
               .Length(1, 5).WithMessage("se admite un maximo de 5 caracteres");

            RuleFor(t => t.descripcion)
              .NotNull().WithMessage("Requerido")
              .NotEmpty().WithMessage("No debe estar Vacio")
              .Length(1, 45).WithMessage("se admite un maximo de 45 caracteres");
        }
    }
}
