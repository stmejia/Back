using Aguila.Core.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Infrastructure.Validators
{
    public class pilotosDocumentosValidator : AbstractValidator<pilotosDocumentosDto>
    {
        public pilotosDocumentosValidator()
        {
            RuleFor(e => e.idPiloto)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío");

            RuleFor(e => e.nombreDocumento)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacio")
                .Length(1, 30).WithMessage("No se admiten más de 30 caracteres");

            RuleFor(e => e.tipoDocumento)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacio")
                .Length(1, 20).WithMessage("No se admiten más de 20 caracteres");
        }
    }
}
