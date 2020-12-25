using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace lab7.ViewModels
{
    public class TrainViewModel
    {
        public int TrainId { get; set; }
        public int TypeId { get; set; }
        public bool IsFirm { get; set; }
        public string NameOfType { get; set; }
    }
}