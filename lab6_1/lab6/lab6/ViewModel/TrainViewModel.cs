using RailroadTransport.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab6.ViewModel
{
    public class TrainViewModel
    {
        public int TrainId { get; set; }
        public bool IsFirm { get; set; }
        public int TypeId { get; set; }
        public string NameOfType { get; set; }
    }
}
