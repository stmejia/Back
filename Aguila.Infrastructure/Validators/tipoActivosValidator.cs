using Aguila.Core.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Infrastructure.Validators
{
    public class tipoActivosValidator : AbstractValidator<tipoActivosDto>
    {
        public tipoActivosValidator()
        {
            RuleFor(t => t.codigo)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe estar Vacio")
                .Length(1, 45).WithMessage("se adminte un maximo de 45 caracteres");


            RuleFor(t => t.nombre)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe estar Vacio")
                .Length(1, 45).WithMessage("se adminte un maximo de 45 caracteres");

            RuleFor(t => t.area)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe estar Vacio")
                .Length(1, 45).WithMessage("se adminte un maximo de 45 caracteres");

            //RuleFor(t => t.operaciones)
            //   .NotNull().WithMessage("Requerido")
            //   .NotEmpty().WithMessage("No debe estar Vacio");

            RuleFor(t => t.porcentajeDepreciacionAnual)
              .NotNull().WithMessage("Requerido")
              .NotEmpty().WithMessage("No debe estar Vacio")
              .GreaterThan(0)
              .WithMessage("Depreciacion debe ser mayor a CERO")
              .LessThan(100)
              .WithMessage("Depreciacion debe ser menor a 100");
        }
    }
}
