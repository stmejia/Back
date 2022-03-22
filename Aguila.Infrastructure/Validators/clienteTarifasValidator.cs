using Aguila.Core.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;


namespace Aguila.Infrastructure.Validators
{
    public class clienteTarifasValidator : AbstractValidator<clienteTarifasDto>
    {
        public clienteTarifasValidator()
        {
            RuleFor(e => e.idCliente)
              .NotNull().WithMessage("Requerido")
              .NotEmpty().WithMessage("No debe de estar vacío");
            
            RuleFor(e => e.idTarifa)
              .NotNull().WithMessage("Requerido")
              .NotEmpty().WithMessage("No debe de estar vacío");
            
            RuleFor(e => e.precio)
              .NotNull().WithMessage("Requerido")
              .NotEmpty().WithMessage("No debe de estar vacío");

            RuleFor(e => e.vigenciaHasta)
              .NotNull().WithMessage("Requerido")
              .NotEmpty().WithMessage("No debe de estar vacío");
        }
    }
}
