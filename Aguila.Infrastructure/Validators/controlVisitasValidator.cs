using Aguila.Core.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aguila.Infrastructure.Validators
{
    public class controlVisitasValidator : AbstractValidator<controlVisitasDto>
    {
        public controlVisitasValidator()
        {
            RuleFor(e => e.nombre)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estár vacío")
                .Length(1, 150).WithMessage("No se admiten más de 150 caracteres");

            RuleFor(e => e.identificacion)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estár vacío")
                .Length(1, 30).WithMessage("No se admiten más de 30 caracteres");

            RuleFor(e => e.motivoVisita)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estár vacío")
                .Length(1, 150).WithMessage("No se admiten más de 100 caracteres");

            RuleFor(e => e.areaVisita)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estár vacío")
                .Length(1, 150).WithMessage("No se admiten más de 50 caracteres");

            RuleFor(e => e.nombreQuienVisita)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estár vacío")
                .Length(1, 150).WithMessage("No se admiten más de 100 caracteres");

            RuleFor(e => e.idEstacionTrabajo)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estár vacío");

            RuleFor(e => e.empresaVisita)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estár vacío");

        }
    }
}
