namespace Nager.Moco.Models
{
    public class InvoiceSendEmailRequest
    {
        public required string EmailsTo { get; set; }
        public required string Subject { get; set; }
        public required string Text { get; set; }
    }
}
