using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Bets;

namespace IService
{
    public interface IBetsService
    {
        Task<bool> Bets();
    }
}
