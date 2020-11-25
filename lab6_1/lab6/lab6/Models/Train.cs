using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RailroadTransport.Models
{
    public class Train
    {
        [Key]
        [Display(Name = "Код поезда")]
        public int TrainId { get; set; }
        [Display(Name = "Код типа поезда")]
        public int TypeId { get; set; }
        [Display(Name = "Фирменный ли поезд")]
        public bool IsFirm { get; set; }
        public Type Type { get; set; }
        public ICollection<Staff> Staffs { get; set; }
    }
}
