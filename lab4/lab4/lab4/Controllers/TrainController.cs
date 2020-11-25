using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lab2;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace lab4.Controllers
{
    public class TrainController : Controller
    {
        ApplicationContext ac;
        public TrainController(ApplicationContext ac)
        {
            this.ac = ac;
        }
        [ResponseCache(Duration = 258)]
        public IActionResult ShowTable()
        {
            var t = ac.Trains.Take(20);
            return View(t);
        }
    }
}
