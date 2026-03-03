namespace Nager.Moco.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int ProjectId { get; set; }
        public string Identifier { get; set; }
        public DateTime Date { get; set; }
        public string DueDate { get; set; }
        public string ServicePeriod { get; set; }
        public string ServicePeriodFrom { get; set; }
        public string ServicePeriodTo { get; set; }
        public string Status { get; set; }
        public bool Reversed { get; set; }
        public object ReversalInvoiceId { get; set; }
        public bool Reversal { get; set; }
        public object ReversedInvoiceId { get; set; }
        public string Title { get; set; }
        public string RecipientAddress { get; set; }
        public string Currency { get; set; }
        public float NetTotal { get; set; }
        public float Tax { get; set; }
        public BillingVat Vat { get; set; }
        public float GrossTotal { get; set; }
        public float Discount { get; set; }
        public float CashDiscount { get; set; }
        public float CashDiscountDays { get; set; }
        public string DebitNumber { get; set; }
        public string CreditNumber { get; set; }
        public bool Locked { get; set; }
        public CustomProperties CustomProperties { get; set; }
        public object[] Tags { get; set; }
        public User User { get; set; }
        public string FileUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string CreatedOn { get; set; }
        public string UpdatedOn { get; set; }
        public string Salutation { get; set; }
        public string Footer { get; set; }
        public InvoiceItem[] Items { get; set; }
        public Payment[] Payments { get; set; }
        public object[] Reminders { get; set; }
        public InternalContact InternalContact { get; set; }
        public bool ActivityHoursModified { get; set; }
    }
}
