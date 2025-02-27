using SynergyAccounts.Models;

namespace SynergyAccounts.Interface
{
    public interface ICompanyService : IGenericService<Company>
    {
        Task<bool> CreateCompanyAsync(Company company);
        Task<bool> UpdateCompanyAsync(Company company);
    }
}
