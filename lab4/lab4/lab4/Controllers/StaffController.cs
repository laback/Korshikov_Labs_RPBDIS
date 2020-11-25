using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lab2;
using lab4.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace lab4.Controllers
{
    public class StaffController : Controller
    {
        ApplicationContext ac;
        public StaffController(ApplicationContext ac)
        {
            this.ac = ac;
        }
        [ResponseCache(Duration = 258)]
        public IActionResult ShowTable()
        {
            var result = ac.Staffs.Include(p => p.Post).Take(20);
            return View(result.ToList());
        }
    }
}
