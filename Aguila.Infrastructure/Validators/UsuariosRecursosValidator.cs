using Aguila.Core.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Infrastructure.Validators
{
    public class UsuariosRecursosValidator: AbstractValidator<UsuariosRecursosDto>
    {
        public UsuariosRecursosValidator()
        {
            RuleFor(usuarioRecurso => usuarioRecurso.estacionTrabajo_id)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe estar Vacio");

            RuleFor(usuarioRecurso => usuarioRecurso.recurso_id)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe estar Vacio");

            RuleFor(usuarioRecurso => usuarioRecurso.usuario_id)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe estar Vacio");

            RuleFor(usuarioRecurso => usuarioRecurso.opcionesAsignadas)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe estar Vacio");
        }
    }
}
