using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IService;
using Microsoft.AspNetCore.Mvc;
using ViewModel;
using ViewModel.ExpandingMethod;

namespace Lottery_Bets.Controllers
{
    public class BetsController : Controller
    {
        private IBetsService _betsService;

        public BetsController(IBetsService betsService)
        {
            _betsService = betsService;
        }

        /// <summary>
        /// 下注
        /// </summary>
        /// <returns></returns>
        public async Task<ResponseRsp<bool>> Index() => (await _betsService.Bets()).ReturnBool();

    }
}