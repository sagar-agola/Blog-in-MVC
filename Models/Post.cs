using System;

namespace Blog.Models
{
    public class Post
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime Time { get; set; }
        public String Detail { get; set; }
    }
}