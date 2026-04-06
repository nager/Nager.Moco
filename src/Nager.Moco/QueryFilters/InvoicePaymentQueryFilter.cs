namespace Nager.Moco.Models
{
    public class InvoiceQueryFilter
    {
        public DateOnly? DateFrom { get; set; }

        public DateOnly? DateTo { get; set; }

        public int? CompanyId { get; set; }

        public int? ProjectId { get; set; }
    }
}
