using Data.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Post:BaseEntity
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? CompanyName { get; set; }
        public string? Designation { get; set; }
        public string? Location { get; set; }
        public decimal salary { get; set; }
        public string? ImageURL { get; set; }
        public bool? IsJob { get; set; } = false;
        public Guid? UserId { get; set; }
        public User? User { get; set; }
    }
}
