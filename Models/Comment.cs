using System;
using System.Collections.Generic;

namespace Project_PRN211.Models
{
    public partial class Comment
    {

        public int CommentId { get; set; }
        public int? BlogId { get; set; }
        public int? AccountId { get; set; }
        public string? CommentDetail { get; set; }
        public DateTime? Commentdate { get; set; }
        public int? CmrepId { get; set; }
        public int? Isrep { get; set; }

        public virtual Account? Account { get; set; }
        public virtual Blog? Blog { get; set; }
    }
}
