namespace Nager.Moco.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Identifier { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public bool Billable { get; set; }
    }
}
