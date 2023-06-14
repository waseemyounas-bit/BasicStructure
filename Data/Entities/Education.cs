using Data.Entities.Common;

namespace Data.Entities
{
    public class Education : BaseEntity
    {
        public string? School { get; set; }
        public string? Degree { get; set; }
        public string? FieldOfStudy { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Grade { get; set; }
        public string? ActivitiesAndSocities { get; set; }
        public string? Description { get; set; }
        public string? MediaUrl { get; set; }
        public User? User { get; set; }
        
        public Guid? UserId { get; set; }

    }
}
