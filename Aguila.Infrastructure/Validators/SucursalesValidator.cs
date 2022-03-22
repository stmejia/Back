using Aguila.Core.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Infrastructure.Validators
{
    public class SucursalesValidator : AbstractValidator<SucursalDto>
    {
        public SucursalesValidator()
        {
            RuleFor(sucursal => sucursal.Nombre)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe estar Vacio");

            RuleFor(sucursal => sucursal.Direccion)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe estar Vacio");

        }
    }
}
