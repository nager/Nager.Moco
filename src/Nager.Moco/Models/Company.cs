namespace Nager.Moco.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Website { get; set; }
        public object Email { get; set; }
        public object BillingEmailCc { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Address { get; set; }
        public string CountryCode { get; set; }
        public object VatIdentifier { get; set; }
        public bool AlternativeCorrespondenceLanguage { get; set; }
        public bool EnglishCorrespondenceLanguage { get; set; }
        public object[] Tags { get; set; }
        public User User { get; set; }
        public object[] Labels { get; set; }
        public string Info { get; set; }
        public object Footer { get; set; }
        public CustomProperties CustomProperties { get; set; }
        public bool Active { get; set; }
        public object ArchivedOn { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Identifier { get; set; }
        public bool Intern { get; set; }
        public float BillingTax { get; set; }
        public BillingVat BillingVat { get; set; }
        public CustomerVat CustomerVat { get; set; }
        public string Currency { get; set; }
        public bool CustomRates { get; set; }
        public bool IncludeTimeReport { get; set; }
        public string BillingNotes { get; set; }
        public float DefaultDiscount { get; set; }
        public float DefaultCashDiscount { get; set; }
        public int DefaultCashDiscountDays { get; set; }
        public int DefaultInvoiceDueDays { get; set; }
        public string InvoiceFormat { get; set; }
        public object DefaultPaymentMeans { get; set; }
        public Project[] Projects { get; set; }

        public override string ToString()
        {
            return $"{this.Name} {this.Type} [{this.Id}]";
        }
    }
}
