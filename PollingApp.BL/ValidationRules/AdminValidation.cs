using FluentValidation;
using PollingApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PollingApp.BL.ValidationRules
{
    public class AdminValidation : AbstractValidator<Admin>
    {
        public AdminValidation()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("Admin adı boş geçilemez!");
            RuleFor(p => p.Surname).NotEmpty().WithMessage("Admin soyadı boş geçilemez!");
            RuleFor(p => p.Key).NotEmpty().WithMessage("Admin Anahtarı boş geçilemez!");
            RuleFor(p => p.Password).NotEmpty().WithMessage("Admin şifresi boş geçilemez!");
        }
    }
}
