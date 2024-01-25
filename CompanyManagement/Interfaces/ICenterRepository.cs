﻿using CompanyManagement.Models;

namespace CompanyManagement.Interfaces
{
    public interface ICenterRepository
    {
        ICollection<Center> GetCenter();
        Center GetCenter(int id);
        ICollection<Company> GetCompanyByCenter(int companyId);
        bool CenterExists(int id);
    }
}
