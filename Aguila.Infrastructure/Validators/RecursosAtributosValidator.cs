using Aguila.Core.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Infrastructure.Validators
{
    public class RecursosAtributosValidator: AbstractValidator<RecursosAtributosDto>
    {
        public RecursosAtributosValidator()
        {
            RuleFor(recursoAtributo => recursoAtributo.Codigo)
            .NotNull().WithMessage("Requerido")
            .NotEmpty().WithMessage("No debe estar Vacio");

            RuleFor(recursoAtributo => recursoAtributo.Nombre)
            .NotNull().WithMessage("Requerido")
            .NotEmpty().WithMessage("No debe estar Vacio");

            RuleFor(recursoAtributo => recursoAtributo.RecursoId)
            .NotNull().WithMessage("Requerido")
            .NotEmpty().WithMessage("No debe estar Vacio");
        }
    }
}
