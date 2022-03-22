using Aguila.Core.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

/**
 * Validadores de campos de los request al API
 * se implementa el paquete FluentValidations para ASPNETcore
 */

namespace Aguila.Infrastructure.Validators
{
    public class EmpresasValidator: AbstractValidator<EmpresasDto>
    {

        public EmpresasValidator()
        {
            RuleFor(empresa => empresa.Pais)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe estar vacio");

            RuleFor(empresa => empresa.Nombre)
                .NotNull().WithMessage("Rquerido")
                .NotEmpty().WithMessage("No debe estar vacio");

            RuleFor(empresa => empresa.Abreviatura)
               .NotNull().WithMessage("Requerido")
               .NotEmpty().WithMessage("No debe estar vacio")
               .Length(2, 50).WithMessage("tamaño 1 a 50 caracteres");

            RuleFor(empresa => empresa.FchCreacion)
              .NotNull().WithMessage("Requerido")
              .NotEmpty().WithMessage("No debe estar vacio");
              ;

            RuleFor(empresa => empresa.esEmpleador)
               .NotNull().WithMessage("Requerido")
               .NotEmpty().WithMessage("No debe estar vacio");

            RuleFor(empresa => empresa.Aleas) 
               .NotNull().WithMessage("Requerido")
               .NotEmpty().WithMessage("No debe estar vacio")
               .Length(2, 20).WithMessage("tamaño 2 a 20 caracteres");

            RuleFor(empresa => empresa.Nit)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe ser vacio")
                .Length(2, 20).WithMessage("tamaño 2 a 20 caracteres");

            RuleFor(empresa => empresa.Direccion)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe ser vacio")
                .Length(2, 100).WithMessage("tamaño 1 a 100 caracteres");


        }
    }
}
