using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lab2;
using Microsoft.AspNetCore.Mvc;

namespace lab4.Controllers
{
    public class PostController : Controller
    {
        ApplicationContext ac;
        public PostController (ApplicationContext ac)
        {
            this.ac = ac;
        }
        [ResponseCache(Duration = 258)]
        public IActionResult ShowTable()
        {
            return View(ac.Posts);
        }
    }
}
