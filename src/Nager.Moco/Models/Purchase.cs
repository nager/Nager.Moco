namespace Nager.Moco.Models
{
    public class Purchase
    {
        public int Id { get; set; }
        public string Identifier { get; set; }
        public string ReceiptIdentifier { get; set; }
        public string Title { get; set; }
        public string Info { get; set; }
        public string Iban { get; set; }
        public string Reference { get; set; }
        public string Date { get; set; }
        public string DueDate { get; set; }
        public string ServicePeriodFrom { get; set; }
        public string ServicePeriodTo { get; set; }
        public string Status { get; set; }
        public string PaymentMethod { get; set; }
        public float NetTotal { get; set; }
        public float GrossTotal { get; set; }
        public string Currency { get; set; }
        public string FileUrl { get; set; }
        //public CustomProperties CustomProperties { get; set; }
        public string[] Tags { get; set; }
        public Company Company { get; set; }
        public Payment[] Payments { get; set; }
        public User User { get; set; }
        //public object refund_request { get; set; }
        public string ApprovalStatus { get; set; }
        //public object credit_card_transaction { get; set; }
        //public Item[] Items { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
