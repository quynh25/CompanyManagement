namespace CompanyManagement.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public ICollection<Center> Centers { get; set; }

    }
}
