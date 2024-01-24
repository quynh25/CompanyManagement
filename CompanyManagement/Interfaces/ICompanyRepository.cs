using CompanyManagement.Models;

namespace CompanyManagement.Interfaces
{
    public interface ICompanyRepository
    {
        ICollection<Company> GetCompanies();
    }
}
