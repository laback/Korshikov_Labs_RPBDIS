using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RailroadTransport.Models
{
    public class Stop
    {
        [Key]
        [Display(Name = "Код остановки")]
        public int StopId { get; set; }
        [Display(Name = "Название остановки")]
        public string NameOfStop { get; set; }
        [Display(Name = "Является ли станцией")]
        public bool IsStation { get; set; }
        [Display(Name = "Имеется ли зал ожидания")]
        public bool IsHall { get; set; }
        public ICollection<Schedule> ScheduleStop { get; set; }
    }
}
