namespace Nager.Moco.Models
{
    public class Receipt
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public bool Billable { get; set; }
        public bool Pending { get; set; }
        public float GrossTotal { get; set; }
        public string Currency { get; set; }
        public ReceiptItem[] Items { get; set; }
        public Project Project { get; set; }
        public string Info { get; set; }
        public User User { get; set; }
        public object RefundRequest { get; set; }
        public string AttachmentUrl { get; set; }
    }
}
