using Aguila.Core.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aguila.Infrastructure.Validators
{
    public  class controlEquipoAjenoValidator : AbstractValidator<controlEquipoAjenoDto>
    {
        public controlEquipoAjenoValidator()
        {
            RuleFor(e => e.nombrePiloto)
               .NotNull().WithMessage("Requerido")
               .NotEmpty().WithMessage("No debe de estár vacío")
               .Length(1, 100).WithMessage("No se admiten más de 100 caracteres");

            //RuleFor(e => e.ingreso)
            //   .NotNull().WithMessage("Requerido")
            //   .NotEmpty().WithMessage("No debe de estár vacío")
            //   ;

            RuleFor(e => e.idUsuario)
              .NotNull().WithMessage("Requerido")
              .NotEmpty().WithMessage("No debe de estár vacío")
              ;

            RuleFor(e => e.idEstacionTrabajo)
              .NotNull().WithMessage("Requerido")
              .NotEmpty().WithMessage("No debe de estár vacío")
              ;
        }
    }
}
