using Aguila.Core.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Infrastructure.Validators
{
   public class condicionActivosValidator : AbstractValidator<condicionActivosDto>
    {
        public condicionActivosValidator()
        {
            RuleFor(e => e.tipoCondicion)
              .NotNull().WithMessage("Requerido")
              .NotEmpty().WithMessage("No debe de estar vacío")
              .Length(1, 25).WithMessage("No se admiten más de 25 caracteres");

            //RuleFor(e => e.ubicacionIdEntrega)              
            //  .Length(1, 50).WithMessage("No se admiten más de 50 caracteres");

            RuleFor(e => e.movimiento)
              .NotNull().WithMessage("Requerido")
              .NotEmpty().WithMessage("No debe de estar vacío")
              .Length(1, 25).WithMessage("No se admiten más de 25 caracteres");

            //RuleFor(e => e.serie)
            // .NotNull().WithMessage("Requerido")
            // .NotEmpty().WithMessage("No debe de estar vacío")
            // .Length(1, 15).WithMessage("No se admiten más de 15 caracteres");

            //RuleFor(e => e.idEstado)
            // .NotNull().WithMessage("Requerido")
            // .NotEmpty().WithMessage("No debe de estar vacío");

            RuleFor(e => e.observaciones)          
            .Length(0, 300).WithMessage("No se admiten más de 300 caracteres");
        }
        
    }
}
