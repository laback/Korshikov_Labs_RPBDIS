using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace lab2
{
    public class Staff
    {
        [Key]
        public int staffId { get; set; }
        public int trainId { get; set; }
        public int postId { get; set; }
        public Staff(int trainId, int postId)
        {
            this.trainId = trainId;
            this.postId = postId;
        }
        public virtual Post Post { get; set; }
    }
}
