using lab2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab4.Models
{
    public class ViewModel
    {
        public IEnumerable<Post> Posts { get; set; }
        public IEnumerable<Staff> Staffs { get; set; }
    }
}
