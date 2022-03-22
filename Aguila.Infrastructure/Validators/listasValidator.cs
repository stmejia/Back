using Aguila.Core.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Infrastructure.Validators
{
    public class listasValidator : AbstractValidator<listasDto>
    {
        public listasValidator()
        {
            RuleFor(t => t.valor)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe estar Vacio")
                .Length(1, 25).WithMessage("se adminte un maximo de 25 caracteres");

            RuleFor(t => t.descripcion)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe estar Vacio")
                .Length(1, 75).WithMessage("se adminte un maximo de 75 caracteres");

            //RuleFor(t => t.fechaCreacion)
            //   .NotNull().WithMessage("Requerido")
            //   .NotEmpty().WithMessage("No debe estar Vacio");

            RuleFor(t => t.idEmpresa)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe estar Vacio");

            RuleFor(t => t.idTipoLista)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe estar Vacio");
        }
    }
}
