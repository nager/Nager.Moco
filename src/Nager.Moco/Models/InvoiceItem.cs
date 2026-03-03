namespace Nager.Moco.Models
{
    public class InvoiceItem
    {
        public int? Id { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public object? ServiceType { get; set; }
        public object? Description { get; set; }
        public float? Quantity { get; set; }
        public string? Unit { get; set; }
        public float? UnitPrice { get; set; }
        public float? NetTotal { get; set; }
    }
}
