using System;
using System.Collections.Generic;

namespace Project_PRN211.Models
{
    public partial class Blog
    {
        public Blog()
        {
            Comments = new HashSet<Comment>();
        }

        public int BlogId { get; set; }
        public int? AccountId { get; set; }
        public int? CategoryId { get; set; }
        public string? Tilte { get; set; }
        public string? BlogDetail { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int? NumOfLike { get; set; }
        public int? StatusId { get; set; }
        public string? Image { get; set; }

        public virtual Account? Account { get; set; }
        public virtual BlogCategory? Category { get; set; }
        public virtual BlogStatus? Status { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
