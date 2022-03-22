using Aguila.Core.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aguila.Infrastructure.Validators
{
    public class detalleCondicionValidator: AbstractValidator<detalleCondicion>
    {
        public detalleCondicionValidator()
        {
            RuleFor(e => e.idUsuario)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío");

            RuleFor(e => e.idUsuarioAutoriza)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío");

            RuleFor(e => e.idCondicion)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío");

            RuleFor(e => e.idReparacion)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío");

            RuleFor(e => e.nombreAutoriza)
                //.NotNull().WithMessage("Requerido")
                //.NotEmpty().WithMessage("No debe de estar vacío")
                .Length(5, 45).WithMessage("No se admiten más de 45 caracteres y menos de 5");
        }
    }
}
