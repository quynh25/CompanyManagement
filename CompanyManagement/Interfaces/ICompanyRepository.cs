using CompanyManagement.Models;

namespace CompanyManagement.Interfaces
{
    public interface ICompanyRepository
    {
        ICollection<Company> GetCompanies();
        Company GetCompany(int id);
        bool CompanyExists(int id);
        bool CreateCompany(Company company);
        bool DeleteCompany(Company company);
        bool Save();
    }
}
