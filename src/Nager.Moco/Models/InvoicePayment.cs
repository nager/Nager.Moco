namespace Nager.Moco.Models
{
    public class InvoicePayment
    {
        public int Id { get; set; }
        public DateOnly Date { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public InvoiceShortInfo Invoice { get; set; }
        public float PaidTotal { get; set; }
        public float PaidTotalInAccountCurrency { get; set; }
        public string Currency { get; set; }
        public string Description { get; set; }

    }
}
