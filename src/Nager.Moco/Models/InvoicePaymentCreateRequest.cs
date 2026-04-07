namespace Nager.Moco.Models
{
    public class InvoicePaymentCreateRequest
    {
        public DateOnly Date { get; set; }

        public float PaidTotal { get; set; }

        public string Currency { get; set; }

        public int InvoiceId { get; set; }

        public bool PartiallyPaid { get; set; }

        public string? Description { get; set; }
    }
}
