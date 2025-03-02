using SynergyAccounts.Models;

namespace SynergyAccounts.Interface
{
    public interface ICompanyService
    {
        Task<bool> CreateCompanyAsync(Company company);
        Task<bool> UpdateCompanyAsync(Company company);
        Task<Company> GetByIdAsync(int Id); 
    }
}
