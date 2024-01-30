using CompanyManagement.Data;
using CompanyManagement.Interfaces;
using CompanyManagement.Models;

namespace CompanyManagement.Repository
{
    public class CenterRepository : ICenterRepository
    {
        private DataContext _context;
        public CenterRepository(DataContext context)
        {
            _context = context;
        }

        public bool CenterExists(int id)
        {
            return _context.Centers.Any(x => x.Id == id);
        }

        public bool CreateCenter(Center center)
        {
            _context.Add(center);
            return Save();
        }

        public bool DeleteCenter(Center center)
        {
            _context.Remove(center);
            return Save();
        }

        public ICollection<Center> GetCenter()
        {
            return _context.Centers.ToList();
        }

        public Center GetCenter(int id)
        {
            return _context.Centers.Where(c => c.Id == id).FirstOrDefault();
        }

        public ICollection<Company> GetCompanyByCenter(int companyId)
        {
            return _context.Centers.Where(c => c.Id==companyId).Select(c => c.Company).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
