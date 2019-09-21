using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
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
        //public async Task<ApiResult<bool>> Index() => (await _betsService.Bets()).ReturnBool();
        public async Task<ApiResult<bool>> Index() => (await _betsService.Bets_Lucky()).ReturnBool();

        public async Task<ApiResult<bool>> Index2() => (await _betsService.AddRecord()).ReturnBool();

    }
}