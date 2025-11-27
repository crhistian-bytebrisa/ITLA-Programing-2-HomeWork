using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Application.Validations
{
    public class IdStringValidation : AbstractValidator<string>
    {
        public IdStringValidation()
        {
            RuleFor(x => x)
                .Must(id => Guid.TryParse(id, out _))
                .WithMessage("El ID debe ser un GUID válido.");
        }
    }
}
