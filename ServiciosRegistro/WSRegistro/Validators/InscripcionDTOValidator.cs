using FluentValidation;
using WSRegistro.DTOs;

namespace WSRegistro.Validators
{
    public class InscripcionDTOValidator : AbstractValidator<InscripcionDto>
    {
        public InscripcionDTOValidator()
        {
            RuleFor(x => x.IdEstudiante).GreaterThan(0).WithMessage("El ID del estudiante debe ser mayor que cero.");
            RuleFor(x => x.IdMateria).GreaterThan(0).WithMessage("El ID de la materia debe ser mayor que cero.");
            RuleFor(x => x.IdSemestre).GreaterThan(0).WithMessage("El ID del semestre debe ser mayor que cero.");
            RuleFor(x => x.EstadoInscripcion).GreaterThan(0).WithMessage("El estado debe ser mayor que cero.");
        }
    }
}
