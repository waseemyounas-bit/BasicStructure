using Data.Entities.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    public class User : BaseEntity
    {
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        [RegularExpression("^([\\w+-.%]+@[\\w-.]+\\.[A-Za-z]{2,4},?)$", ErrorMessage = "Invalid email format.")]
        public string? Email { get; set; }
        public string? ProfileImage { get; set; }
        public string? Title { get; set; }
        public string? Sector { get; set; }
        public string? Education { get; set; }
        public string? Location { get; set; }
        public string? Country { get; set; }
        [NotMapped]
        public int connectionCount { get; set; }
        [NotMapped]
        public DateTime  RequestDate{ get; set; }
        [NotMapped]
        public bool  IsAccepted{ get; set; }
        [NotMapped]
        public Guid?  FriendId{ get; set; }
        public string? City { get; set; }
        [Required]
        [MinLength(6)]
        public string? Password { get; set; }
        public List<Education>? Educations { get; set; }
        public List<Experience>? Experiences { get; set; }
        public List<LicenseOfCertification>? LicenseOfCertifications { get; set; }
        public List<Connection>? Connections { get; set; }



    }
}
