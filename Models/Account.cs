using System;
using System.Collections.Generic;

namespace Project_PRN211.Models
{
    public partial class Account
    {
        public Account()
        {
            Blogs = new HashSet<Blog>();
            Comments = new HashSet<Comment>();
        }

        public int AccountId { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Avatar { get; set; }
        public string? Name { get; set; }
        public bool? Gender { get; set; }
        public string? Mobile { get; set; }
        public string? Address { get; set; }

        public virtual ICollection<Blog> Blogs { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
