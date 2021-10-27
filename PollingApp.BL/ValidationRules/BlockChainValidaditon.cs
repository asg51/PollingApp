using FluentValidation;
using PollingApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static PollingApp.Entities.BlockChainList;

namespace PollingApp.BL.ValidationRules
{
   public class BlockChainValidaditon:AbstractValidator<BlockChainsData>
    {
        public BlockChainValidaditon()
        {
            RuleFor(x => x).Must(x => BlockChainSetting(x.BlockChainForVoters.GetBlocks()));
            RuleFor(x => x).Must(x => BlockChainSetting(x.BlockChainForAdmins.GetBlocks()));
            RuleFor(x => x).Must(x => BlockChainSetting(x.BlockChainForChosens.GetBlocks()));
            RuleFor(x => x).Must(x => IsValid(x.BlockChainForVoters.Chain)).WithMessage("Hata seçmen verileri hatalı!");
            RuleFor(x => x).Must(x => IsValid(x.BlockChainForAdmins.Chain)).WithMessage("Hata admin verileri hatalı!");
            RuleFor(x => x).Must(x => IsValid(x.BlockChainForChosens.Chain)).WithMessage("Hata aday verileri hatalı!");
            RuleFor(x => x.BlockChainForPollName).NotEmpty().WithMessage("Seçim adı yok!");
            RuleFor(x => x).NotEmpty().WithMessage("Seçim adı yok!");
            RuleFor(x => x.PollTime).Must(x => CheckBetweenDates(x.StartTime, x.FinishTime))
              .WithMessage("Başlangıç tarihi bitiş tarihiden sonra olamaz!");
        }
        private bool BlockChainSetting<T>(IList<Block<T>> list) => BlockChain<T>.BlockChainSetting(list);
        private bool IsValid<T>(IList<Block<T>> list) => BlockChain<T>.IsValid(list);
        private bool CheckBetweenDates(DateTime dateTime1, DateTime dateTime2) => dateTime1 < dateTime2;
    }
}
