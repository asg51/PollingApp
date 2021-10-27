using FluentValidation;
using PollingApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PollingApp.BL.ValidationRules
{
    public class PollValidation : AbstractValidator<Poll>
    {
        public PollValidation()
        {
            RuleFor(p => p.PollingName).NotEmpty().WithMessage("Seçim adı boş geçilmez!");
            RuleFor(p => p.PollingName).Must(p => PollNameState(p)).WithMessage("Aynı isimde seçim olmaz!");
        }
        public bool PollNameState(string name) => Managers.pollManager.PollNewNameControl(name);
    }
}
