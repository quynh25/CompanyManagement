namespace CompanyManagement.Models
{
    public class Center
    {
        public int Id { get; set; }
        public string CenterName { get; set; }
        public Company Company { get; set; }
        public ICollection<Derpartment>Derpartments { get; set; }
    }
}
