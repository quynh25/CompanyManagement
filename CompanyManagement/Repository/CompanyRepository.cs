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
        //
        public bool CompanyExists(int id)
        {
            return _context.Companys.Any(x => x.Id == id);
        }

        public bool CreateCompany(Company company)
        {
            _context.Add(company);
            return Save();
        }

        public bool DeleteCompany(Company company)
        {
            _context.Remove(company);
            return Save();
        }

        //HIỂN THỊ
        public ICollection<Company> GetCompanies()
        {
            return _context.Companys.OrderBy(p => p.Id).ToList();
        }
        //  tìm kiếm theo id
        public Company GetCompany(int id)
        {
            return _context.Companys.Where(c => c.Id == id).FirstOrDefault();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved >0 ? true : false;
        }

        public bool UpdateCompany(Company company)
        {
            _context.Update(company);
            return Save();
        }
    }
}
