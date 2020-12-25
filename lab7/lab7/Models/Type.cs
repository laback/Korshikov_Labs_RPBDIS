using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lab7.Models
{
    public class Type
    {
        [Key]
        [Display(Name = "Код типа поезда")]
        public int TypeId { get; set; }
        [Display(Name = "Название типа поезда")]
        public string NameOfType { get; set; }
        public ICollection<Train> Trains { get; set; }
    }
}
