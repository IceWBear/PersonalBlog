using System;
using System.Collections.Generic;

namespace Project_PRN211.Models
{
    public partial class AddFriend
    {
        public int? UserId { get; set; }
        public int? FriendId { get; set; }
        public int? StatusFid { get; set; }

        public virtual Account? Friend { get; set; }
        public virtual StatusFriend? StatusF { get; set; }
        public virtual Account? User { get; set; }
    }
}
