using Microsoft.EntityFrameworkCore;
using StudentsPortal.Data.DataModels;

namespace StudentsPortal.Data
{
    public class StudentsPortalDbContext : DbContext
    {
        public StudentsPortalDbContext(DbContextOptions<StudentsPortalDbContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }

        public DbSet<Gender> Genders { get; set; }

        public DbSet<Address> Addresses { get; set; }
    }
}
