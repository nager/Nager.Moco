namespace Nager.Moco.QueryFilters
{
    public class InvoicePaymentQueryFilter
    {
        public DateOnly? DateFrom { get; set; }

        public DateOnly? DateTo { get; set; }

        public int? InvoiceId { get; set; }
    }
}
