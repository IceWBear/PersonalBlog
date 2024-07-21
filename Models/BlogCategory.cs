using System;
using System.Collections.Generic;

namespace Project_PRN211.Models
{
    public partial class BlogCategory
    {
        public BlogCategory()
        {
            Blogs = new HashSet<Blog>();
        }

        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public string? Image { get; set; }

        public virtual ICollection<Blog> Blogs { get; set; }
    }
}
