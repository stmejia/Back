using Aguila.Core.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Infrastructure.Validators
{
   public class activoGeneralesValidator :AbstractValidator<activoGeneralesDto>
    {
        public activoGeneralesValidator()
        {
            RuleFor(t => t.codigo)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe estar Vacio")
                .Length(1, 10).WithMessage("se adminte un maximo de 10 caracteres");

            RuleFor(t => t.descripcion)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe estar Vacio")
                .Length(1, 125).WithMessage("se adminte un maximo de 125 caracteres");

            RuleFor(t => t.fechaCompra)
               .NotNull().WithMessage("Requerido")
               .NotEmpty().WithMessage("No debe estar Vacio");

            RuleFor(t => t.valorCompra)
               .NotNull().WithMessage("Requerido")
               .NotEmpty().WithMessage("No debe estar Vacio");

            RuleFor(t => t.valorLibro)
               .NotNull().WithMessage("Requerido")
               .NotEmpty().WithMessage("No debe estar Vacio");

            RuleFor(t => t.valorRescate)
              .NotNull().WithMessage("Requerido")
              .NotEmpty().WithMessage("No debe estar Vacio");

            RuleFor(t => t.depreciacionAcumulada)
              .NotNull().WithMessage("Requerido")
              .NotEmpty().WithMessage("No debe estar Vacio");

            RuleFor(t => t.idDocumentoCompra)
              .NotNull().WithMessage("Requerido")
              .NotEmpty().WithMessage("No debe estar Vacio");

            RuleFor(t => t.idTipoActivo)
             .NotNull().WithMessage("Requerido")
             .NotEmpty().WithMessage("No debe estar Vacio");
        }
    }
}
