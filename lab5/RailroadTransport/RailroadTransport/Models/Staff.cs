using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RailroadTransport.Models
{
    public class Staff
    {
        [Key]
        [Display(Name = "Код сотрудника")]
        public int StaffId { get; set; }
        [Display(Name = "ФИО сотрудника")]
        public string FIO { get; set; }
        [Display(Name = "Возраст сотрудника")]
        public int Age { get; set; }
        [Display(Name = "Стаж работы сотрудника")]
        public int WorkExp { get; set; }
        [Display(Name = "Номер поезда")]
        public int TrainId { get; set; }
        [Display(Name = "Номер должности")]
        public int PostId { get; set; }
        public Post Post { get; set; }
        public Train Train { get; set; }
    }
}
