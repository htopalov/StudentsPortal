using System.ComponentModel.DataAnnotations;
using static StudentsPortal.Data.Common.ValidationConstants;

namespace StudentsPortal.Data.DataModels
{
    public class Gender
    {
        [Key]
        [MaxLength(EntityIdMaxLength)]
        public Guid Id { get; set; }

        [MaxLength(GenderDescriptionMaxLength)]
        [Required]
        public string Description { get; set; }
    }
}
