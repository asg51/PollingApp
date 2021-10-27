using FluentValidation;
using PollingApp.Entities.P2PModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PollingApp.BL.ValidationRules
{
   public class ConnectAsAdminValidation: AbstractValidator<ConnectAsAdmin>
    {
        public ConnectAsAdminValidation()
        {
            RuleFor(x => x.IP).NotEmpty().WithMessage("Ip boş geçilmez!");
            RuleFor(x => x.Key).NotEmpty().WithMessage("Anahtar boş geçilmez!");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Seçim adı boş geçilmez!");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Şifre boş geçilmez!");
        }
    }
}
