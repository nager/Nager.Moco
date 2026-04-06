namespace Nager.Moco.Models
{
    public class InvoicePayment
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public decimal PaidTotal { get; set; }
        public string Currency { get; set; }
        public string CreatedOn { get; set; }
        public string UpdatedOn { get; set; }
    }
}
