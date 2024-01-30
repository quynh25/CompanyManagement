using CompanyManagement.Models;

namespace CompanyManagement.Interfaces
{
    public interface IDerpartmentRepository
    {
        ICollection<Derpartment> GetDerpartments();
        Derpartment GetDerpartmentsById(int id);
        ICollection<Center> GetCenterByDerpartment(int centerId);
        bool DerpartmentExits(int id);
        bool CreateDeparment(Derpartment department);
        bool Save();

    }
}
