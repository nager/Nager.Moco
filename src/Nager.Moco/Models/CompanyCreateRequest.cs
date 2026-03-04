namespace Nager.Moco.Models
{
    public class CompanyCreateRequest
    {
        public required string Name { get; set; }

        public string Type { get; set; } = "customer";

        public required string Currency { get; set; }

        public string? CountryCode { get; set; }

        public string? VatIdentifier { get; set; }
    }
}
