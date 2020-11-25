using RailroadTransport.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RailroadTransport.ViewModels
{
    public class StopViewModel
    {
        public string NameOfStop { get; set; }
        public IEnumerable<Stop> Stops { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public SortViewModel SortViewModel { get; set; }
    }
}
