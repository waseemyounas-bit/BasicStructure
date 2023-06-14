using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options)
        {
            
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Experience> Experiences { get; set; }
        public DbSet<LicenseOfCertification> LicenseOfCertifications { get; set; }
        public DbSet<Connection> Connections { get; set; }
        public DbSet<Post> Posts { get; set; }

    }
}
