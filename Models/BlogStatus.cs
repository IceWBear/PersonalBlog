using System;
using System.Collections.Generic;

namespace Project_PRN211.Models
{
    public partial class BlogStatus
    {
        public BlogStatus()
        {
            Blogs = new HashSet<Blog>();
        }

        public int StatusId { get; set; }
        public string? StatusName { get; set; }

        public virtual ICollection<Blog> Blogs { get; set; }
    }
}
