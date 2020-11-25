using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RailroadTransport.Models
{
    public class Schedule
    {
        [Key]
        [Display(Name = "Номер строки расписания")]
        public int ScheduleId { get; set; }
        [Display(Name = "Номер поезда")]
        public int TrainId { get; set; }
        [Display(Name = "День отправления")]
        public string Day { get; set; }
        [Display(Name = "Номер остановки")]
        public int StopId { get; set; }
        [Display(Name = "Расстояние маршрута")]
        public double Distance { get; set; }
        [Display(Name = "Время прибытия")]
        [DisplayFormat(DataFormatString = "{0:hh\\:mm}", ApplyFormatInEditMode = true)]
        public TimeSpan TimeOfArrive { get; set; }
        [Display(Name = "Время отправления")]
        [DisplayFormat(DataFormatString = "{0:hh\\:mm}", ApplyFormatInEditMode = true)]
        public TimeSpan TimeOfDeparture { get; set; }
        public Train Train { get; set; }
        public Stop Stop { get; set; }
    }
}
