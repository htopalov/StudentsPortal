using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static StudentsPortal.Data.Common.ValidationConstants;

namespace StudentsPortal.Data.DataModels
{
    public class Address
    {
        [Key]
        [MaxLength(EntityIdMaxLength)]
        public Guid Id { get; set; }

        [MaxLength(PhysicalAddressMaxLength)]
        [Required]
        public string PhysicalAddress { get; set; }

        [MaxLength(PostalAddressMaxLength)]
        [Required]
        public string PostalAddress { get; set; }

        public Guid StudentId { get; set; }
    }
}
