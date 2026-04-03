namespace Nager.Moco.Models
{
    public class InvoiceDetail
    {
        /// <summary>
        /// Internal invoice ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Customer company ID
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Linked project ID
        /// </summary>
        public int? ProjectId { get; set; }

        /// <summary>
        /// Human-readable invoice number
        /// </summary>
        public string Identifier { get; set; }

        /// <summary>
        /// Invoice date
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Due date
        /// </summary>
        public DateTime? DueDate { get; set; }

        /// <summary>
        /// Human-readable service period
        /// </summary>
        public string? ServicePeriod { get; set; }

        /// <summary>
        /// Service period start date
        /// </summary>
        public string? ServicePeriodFrom { get; set; }

        /// <summary>
        /// Service period end date
        /// </summary>
        public string? ServicePeriodTo { get; set; }
        public string Status { get; set; }
        public bool Reversed { get; set; }
        public object ReversalInvoiceId { get; set; }
        public bool Reversal { get; set; }
        public object ReversedInvoiceId { get; set; }
        public string Title { get; set; }
        public string? RecipientAddress { get; set; }
        public string Currency { get; set; }
        public float NetTotal { get; set; }
        public float Tax { get; set; }
        public BillingVat Vat { get; set; }
        public float GrossTotal { get; set; }
        public float Discount { get; set; }
        public float CashDiscount { get; set; }
        public float CashDiscountDays { get; set; }
        public int? DebitNumber { get; set; }
        public int? CreditNumber { get; set; }
        public bool Locked { get; set; }
        public CustomProperties CustomProperties { get; set; }
        public object[] Tags { get; set; }
        public User User { get; set; }
        public string FileUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Salutation { get; set; }
        public string Footer { get; set; }
        public InvoiceItem[] Items { get; set; }
        public Payment[] Payments { get; set; }
        public object[] Reminders { get; set; }
        public InternalContact InternalContact { get; set; }
        public bool ActivityHoursModified { get; set; }
    }
}
