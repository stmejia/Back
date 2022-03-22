using Aguila.Core.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Infrastructure.Validators
{
    public class tiposListaValidator : AbstractValidator<tiposListaDto>
    {
        public tiposListaValidator()
        {
            RuleFor(t => t.descripcion)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe estar Vacio")
                .Length(1, 50).WithMessage("se adminte un maximo de 50 caracteres");

            RuleFor(t => t.idRecurso)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe estar Vacio");

            RuleFor(t => t.tipoDato)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe estar Vacio")
                .Length(1, 20).WithMessage("se adminte un maximo de 20 caracteres");

            RuleFor(t => t.campo)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe estar Vacio")
                .Length(1, 20).WithMessage("se adminte un maximo de 20 caracteres");

            //RuleFor(t => t.fechaCreacion)
            //    .NotNull().WithMessage("Requerido")
            //    .NotEmpty().WithMessage("No debe estar Vacio");
                
                
        }
    }
}
