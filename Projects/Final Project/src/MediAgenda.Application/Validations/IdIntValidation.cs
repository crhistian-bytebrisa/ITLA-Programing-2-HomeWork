using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Application.Validations
{
    public class IdIntValidation : AbstractValidator<int>
    {
        public IdIntValidation()
        {
            RuleFor(x => x)
                .GreaterThan(0).WithMessage("El Id debe ser mayor a 0.");
        }
    }
}
