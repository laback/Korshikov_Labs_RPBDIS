using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lab7.Models
{
    public class Post
    {
        [Key]
        [Display(Name = "Код должности")]
        public int PostId { get; set; }
        [Display(Name = "Название должности")]
        public string NameOfPost { get; set; }
        public virtual ICollection<Staff> Staffs { get; set; }
    }
}
