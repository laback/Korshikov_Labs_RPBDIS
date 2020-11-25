using RailroadTransport.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RailroadTransport.ViewModels
{
    public class ScheduleViewModel
    {
        public IEnumerable<Schedule> Schedules { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public SortViewModel SortViewModel { get; set; }
        public string Stop { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int CountOfTrains { get; set; }
        public int CountOfStops { get; set; }
        public bool IsSorted { get; set; }
    }
}
