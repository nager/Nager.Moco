namespace Nager.Moco.Models
{
    public class CompanyCreateRequest
    {
        public required string Name { get; set; }

        public string Type { get; set; } = "customer";

        public required string Currency { get; set; }

        public string? CountryCode { get; set; }

        public string? VatIdentifier { get; set; }

        public string? Address { get; set; }

        public bool AlternativeCorrespondenceLanguage { get; set; } = false;
    }
}
