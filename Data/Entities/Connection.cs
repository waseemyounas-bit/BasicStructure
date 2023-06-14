using Data.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Connection:BaseEntity
    {
        public Guid? UserId { get; set; }
        public Guid? FriendId { get; set; }
        public bool IsAccepted { get; set; } = false;
        public User? User { get; set; }
    }
}
