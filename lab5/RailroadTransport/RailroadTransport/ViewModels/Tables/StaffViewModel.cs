using RailroadTransport.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RailroadTransport.ViewModels
{
    public class StaffViewModel
    {
        public IEnumerable<Staff> Staffs { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public SortViewModel SortViewModel { get; set; }
        public string FIO { get; set; }
        public string NameOfPost { get; set; }
        public int Age { get; set; }
        public int WorkExp { get; set; }
    }
}
