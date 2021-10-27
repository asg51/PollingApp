using FluentValidation;
using PollingApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PollingApp.BL.ValidationRules
{
    public class PollTimeValidation : AbstractValidator<PollTime>
    {
        public PollTimeValidation()
        {
            RuleFor(p => p).Must(p => CheckBetweenDates(p.StartTime, p.FinishTime))
                .WithMessage("Başlangıç tarihi bitiş tarihiden sonra olamaz!");
        }
        private bool CheckBetweenDates(DateTime dateTime1, DateTime dateTime2) => dateTime1 < dateTime2;
    }
}
