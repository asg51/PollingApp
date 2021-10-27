using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PollingApp.Entities;

namespace PollingApp.BL.ValidationRules
{
    public class ChosenValidation: AbstractValidator<Chosen>
    {
       public ChosenValidation()
        {
            RuleFor(p => p.ChosenName).NotEmpty().WithMessage("Seçilecek kısmı boş geçilemez!");
        }
    }
}
