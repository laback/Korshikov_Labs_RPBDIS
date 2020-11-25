using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace lab2
{
    public class Post
    {
        [Key]
        public int postId { get; set; }
        public string nameOfPost { get; set; }
        public Post(string nameOfPost)
        {
            this.nameOfPost = nameOfPost;
        }
        public Post() { }
    }
}
