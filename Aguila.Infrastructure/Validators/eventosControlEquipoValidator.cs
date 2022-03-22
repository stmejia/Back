using Aguila.Core.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aguila.Infrastructure.Validators
{
    public class eventosControlEquipoValidator : AbstractValidator<eventosControlEquipo>
    {
        public eventosControlEquipoValidator()
        {
            RuleFor(e => e.idActivo)
               .NotNull().WithMessage("Requerido")
               .NotEmpty().WithMessage("No debe de estar vacío");

            RuleFor(e => e.idUsuarioCreacion)
              .NotNull().WithMessage("Requerido")
              .NotEmpty().WithMessage("No debe de estar vacío");

            RuleFor(e => e.idEstacionTrabajo)
             .NotNull().WithMessage("Requerido")
             .NotEmpty().WithMessage("No debe de estar vacío");

            RuleFor(e => e.fechaCreacion)
              .NotNull().WithMessage("Requerido")
              .NotEmpty().WithMessage("No debe de estar vacío");

            RuleFor(e => e.descripcionEvento)
             .NotNull().WithMessage("Requerido")
             .NotEmpty().WithMessage("No debe de estar vacío");

            RuleFor(e => e.bitacoraObservaciones)
             .NotNull().WithMessage("Requerido")
             .NotEmpty().WithMessage("No debe de estar vacío");
        }
    }
}
