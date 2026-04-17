namespace Nager.Moco.Models
{
    public class Receipt
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Date
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Billable
        /// </summary>
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
