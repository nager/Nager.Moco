namespace Nager.Moco.Models
{
    public class InvoiceShortInfo
    {
        public int Id { get; set; }
        public string Identifier { get; set; }
        public string Title { get; set; }

        public override string ToString()
        {
            return $"Id:{this.Id} Identifier:{this.Identifier} Title:{this.Title}";
        }
    }
}
