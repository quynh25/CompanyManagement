using CompanyManagement.Data;
using CompanyManagement.Interfaces;
using CompanyManagement.Models;

namespace CompanyManagement.Repository
{
    public class DerpartmentRepository : IDerpartmentRepository
    {
        private DataContext _context;
        public DerpartmentRepository(DataContext context)
        {
            _context = context;
        }
        public bool DerpartmentExits(int id)
        {
            return _context.Derpartments.Any(x => x.Id == id);
        }

        public ICollection<Center> GetCenterByDerpartment(int centerId)
        {
            return _context.Derpartments.Where(d=>d.Id == centerId).Select(d=>d.Center).ToList();
        }

        public ICollection<Derpartment> GetDerpartments()
        {
            return _context.Derpartments.ToList();
        }

        public Derpartment GetDerpartmentsById(int id)
        {
            return _context.Derpartments.Where(d => d.Id == id).FirstOrDefault();
        }
    }
}
