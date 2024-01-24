using CompanyManagement.Data;
using CompanyManagement.Interfaces;
using CompanyManagement.Models;

namespace CompanyManagement.Repository
{
    public class CompanyRepository:ICompanyRepository
    {
        public readonly DataContext _context;
        public CompanyRepository(DataContext context)
        {
            _context = context;
        }
        public ICollection<Company> GetCompanies()
        {
            return _context.Companys.OrderBy(p=>p.Id).ToList();
        }
    }
}
