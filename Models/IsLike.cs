using System;
using System.Collections.Generic;

namespace Project_PRN211.Models
{
    public partial class IsLike
    {
        public int? UserId { get; set; }
        public int? BlogId { get; set; }

        public virtual Blog? Blog { get; set; }
        public virtual Account? User { get; set; }
    }
}
