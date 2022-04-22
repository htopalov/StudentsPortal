using StudentsPortal.Models.Address;
using StudentsPortal.Models.Gender;

namespace StudentsPortal.Models.Student
{
    public class StudentExportDto
    {
        public Guid Id { get; init; }
        public string FirstName { get; init; }

        public string LastName { get; init; }

        public DateTime BirthDate { get; init; }

        public string Email { get; init; }

        public string Phone { get; init; }

        public string ProfileImageUrl { get; init; }

        public Guid GenderId { get; init; }
        public GenderExportDto Gender { get; init; }

        public Guid AddressId { get; init; }
        public AddressExportDto Address { get; init; }
    }
}
