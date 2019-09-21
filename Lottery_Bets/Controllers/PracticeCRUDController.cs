using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Lottery_Bets.Controllers
{
    public class PracticeCRUDController : Controller
    {

        /// <summary>
        /// 构造函数
        /// </summary>
        public PracticeCRUDController()
        {

        }

        public IActionResult Index()
        {
            return View();
        }


    }
}