using Entity.Lottery;
using IService.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Bets;

namespace IService
{
    public interface IBetsService: IBaseService<LotteryNumber>
    {
        Task<bool> Bets();

        Task<bool> Bets_Lucky();

        Task<bool> AddRecord();
    }
}
