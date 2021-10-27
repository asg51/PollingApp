using FluentValidation;
using PollingApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PollingApp.BL.ValidationRules
{
    public class VoterValidation:AbstractValidator<Voter>
    {
        public VoterValidation()
        {
            RuleFor(p => p.Key).NotEmpty().WithMessage("Anahtar boş geçilemez!");
            RuleFor(p => p.Password).NotEmpty().WithMessage("Şifre boş geçilemez!");
            RuleFor(p => p.Name).NotEmpty().WithMessage("Ad boş geçilemez!");
            RuleFor(p => p.Surname).NotEmpty().WithMessage("Soyad boş geçilemez!");
        }
    }
}
