namespace StudentsPortal.Models.Address
{
    public class AddressExportDto
    {
        public Guid Id { get; init; }

        public string PhysicalAddress { get; init; }

        public string PostalAddress { get; init; }

        public Guid StudentId { get; init; }
    }
}
