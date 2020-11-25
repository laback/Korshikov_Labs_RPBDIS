using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace lab2
{
    public class Train
    {
        [Key]
        public int trainId { get; set; }
        public int typeId { get; set; }
        public bool isFirm { get; set; }
    }
}
