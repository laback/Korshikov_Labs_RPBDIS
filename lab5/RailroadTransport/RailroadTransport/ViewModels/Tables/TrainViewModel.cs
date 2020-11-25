using RailroadTransport.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RailroadTransport.ViewModels
{
    public class TrainViewModel
    {
        public IEnumerable<Train> Trains { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public SortViewModel SortViewModel { get; set; }
        public string NameOfType { get; set; }
    }
}
