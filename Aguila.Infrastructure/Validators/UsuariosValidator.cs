using Aguila.Core.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Infrastructure.Validators
{
    public class UsuariosValidator: AbstractValidator<UsuariosDto>
    {
        public UsuariosValidator()
        {
            RuleFor(usuario => usuario.Username)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe estar Vacio");

            RuleFor(usuario => usuario.Nombre)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe estar Vacio")
                .Length(5,80).WithMessage("tamaño de 5 a 80 caracteres");
                        
            RuleFor(usuario => usuario.Email)
               .NotNull().WithMessage("Requerido")
               .NotEmpty().WithMessage("No debe estar Vacio")
               .EmailAddress().WithMessage("Email No Valido");
                       
            RuleFor(usuario => usuario.ModuloId)
              .NotNull().WithMessage("Requerido")
              .NotEmpty().WithMessage("No debe estar Vacio");

            RuleFor(usuario => usuario.EstacionTrabajoId)
             .NotNull().WithMessage("Requerido")
             .NotEmpty().WithMessage("No debe estar Vacio");

            RuleFor(usuario => usuario.SucursalId)
            .NotNull().WithMessage("Requerido")
            .NotEmpty().WithMessage("No debe estar Vacio");

            RuleFor(usuario => usuario.fchNacimiento)
            .NotNull().WithMessage("Requerido")
            .NotEmpty().WithMessage("No debe estar Vacio");
        }
    }
}
