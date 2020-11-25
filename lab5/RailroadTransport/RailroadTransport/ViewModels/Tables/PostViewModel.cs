using RailroadTransport.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RailroadTransport.ViewModels
{
    public class PostViewModel
    {
        public string NameOfPost { get; set; }
        public IEnumerable<Post> Posts { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public SortViewModel SortViewModel { get; set; }
    }
}
