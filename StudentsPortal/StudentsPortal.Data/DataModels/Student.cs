using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static StudentsPortal.Data.Common.ValidationConstants;

namespace StudentsPortal.Data.DataModels
{
    public class Student
    {
        [Key]
        [MaxLength(EntityIdMaxLength)]
        public Guid Id { get; set; }

        [MaxLength(FirstNameMaxLength)]
        [Required]
        public string FirstName { get; set; }

        [MaxLength(LastNameMaxLength)]
        [Required]
        public string LastName { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [EmailAddress]
        [MaxLength(EmailMaxLength)]
        public string Email { get; set; }

        [MaxLength(PhoneMaxLength)]
        [Required]
        public string Phone { get; set; }

        [MaxLength(ProfileImageUrlMaxLength)]
        [Required]
        public string ProfileImageUrl { get; set; }

        [ForeignKey(nameof(Gender))]
        [MaxLength(EntityIdMaxLength)]
        public Guid GenderId { get; set; }
        public Gender Gender { get; set; }

        [ForeignKey(nameof(Address))]
        [MaxLength(EntityIdMaxLength)]
        public Guid AddressId { get; set; }
        public Address Address { get; set; }
    }
}
