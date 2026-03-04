namespace Nager.Moco.Models
{
    public class InvoiceCreateRequest
    {
        public int CustomerId { get; set; }
        public string RecipientAddress { get; set; }
        public string Date { get; set; }
        public string DueDate { get; set; }
        public string Title { get; set; }
        public int Tax { get; set; }
        public string Currency { get; set; }
        public InvoiceItem[] Items { get; set; } = [];

        public string? Footer { get; set; }
    }
}
