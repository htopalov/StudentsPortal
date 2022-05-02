using System.ComponentModel.DataAnnotations;
using static StudentsPortal.Data.Common.ValidationConstants;

namespace StudentsPortal.Models.Student
{
    public class StudentImportDto
    {
        [MaxLength(FirstNameMaxLength)]
        [Required]
        public string FirstName { get; set; }

        [MaxLength(LastNameMaxLength)]
        [Required]
        public string LastName { get; set; }

        [Required]
        public string BirthDate { get; set; }

        [EmailAddress]
        [MaxLength(EmailMaxLength)]
        public string Email { get; set; }

        [MaxLength(PhoneMaxLength)]
        [Required]
        public string Phone { get; set; }

        [Required]
        public Guid GenderId { get; set; }

        [MaxLength(PhysicalAddressMaxLength)]
        [Required]
        public string PhysicalAddress { get; set; }

        [MaxLength(PostalAddressMaxLength)]
        [Required]
        public string PostalAddress { get; set; }
    }
}
