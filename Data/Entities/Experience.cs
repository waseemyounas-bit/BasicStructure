using Data.Entities.Common;

namespace Data.Entities
{
    public class Experience : BaseEntity
    {
        public string? Title { get; set; }
        public string? EmploymentType { get; set; }
        public string? CompanyName { get; set; }
        public string? Location { get; set; }
        public bool CurrentlyWorking { get; set; } = true;
        public int MyProperty { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Headline { get; set; }
        public string? Industry { get; set; }
        public string? Description { get; set; }
        public string? MediaUrl { get; set; }
        public User? User { get; set; }

        public Guid? UserId { get; set; }



    }
}
