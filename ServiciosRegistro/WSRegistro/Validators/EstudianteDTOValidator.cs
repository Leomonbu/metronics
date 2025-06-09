using WSRegistro.DTOs;
using FluentValidation;

namespace WSRegistro.Validators
{
    public class EstudianteDTOValidator: AbstractValidator<EstudianteDto>
    {
        public EstudianteDTOValidator()
        {
            RuleFor(x => x.TipoDocumento).GreaterThan(0).WithMessage("El ID del estudiante debe ser mayor que cero.");
            RuleFor(x => x.DocumentoEstudiante).GreaterThan(10000000).WithMessage("El documento del estudiante no es un valor valido.");
            RuleFor(x => x.NombresEstudiante).NotEmpty().MaximumLength(70).WithMessage("El nombre del estudiante no es un valor valido.");
            RuleFor(x => x.ApellidosEstudiante).NotEmpty().MaximumLength(70).WithMessage("El apellido(s) del estudiante no es un valor valido.");
            RuleFor(x => x.EmailEstudiante).NotEmpty().EmailAddress().WithMessage("El email del estudiante no es un valor valido.");
            RuleFor(x => x.Password).NotEmpty().MinimumLength(6).WithMessage("La contraseña del estudiante no es un valor valido.");
            RuleFor(x => x.Fecha_nacimiento).LessThanOrEqualTo(DateTime.UtcNow).WithMessage("La fecha de nacimiento no es correcta.");
            RuleFor(x => x.IdRol).GreaterThan(0).WithMessage("El perfil del estudiante no ha sido autorizado.");
        }
    }
}
