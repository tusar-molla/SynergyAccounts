using Microsoft.EntityFrameworkCore;
using SynergyAccounts.Data;
using SynergyAccounts.Interface;
using SynergyAccounts.Models;

namespace SynergyAccounts.Services
{
    public class CompanyService : GenericService<Company>, ICompanyService
    {
        private readonly AppDbContext _context;

        public CompanyService(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> IsCompanyNameExistsAsync(string name)
        {
            return await _context.Companies.AnyAsync(c => c.Name == name);
        }

        public async Task<bool> CreateCompanyAsync(Company company)
        {
            // Business Logic: Check if name already exists
            if (await IsCompanyNameExistsAsync(company.Name!))
            {
                return false; // Name already exists
            }

            await AddAsync(company);
            return true;
        }

        public async Task<bool> UpdateCompanyAsync(Company company)
        {
            //  Business Logic: Ensure the company exists
            var existingCompany = await GetByIdAsync(company.Id);
            if (existingCompany == null) return false;

            //  Business Logic: Prevent duplicate names (except for itself)
            if (await _context.Companies.AnyAsync(c => c.Name == company.Name && c.Id != company.Id))
            {
                return false;
            }

            await UpdateAsync(company);
            return true;
        }

        //will be use in future
        //public async Task<bool> DeleteCompanyAsync(int id)
        //{
        //    var existingCompany = await GetByIdAsync(id);
        //    if (existingCompany == null) return false;

        //    await DeleteAsync(id);
        //    return true;
        //}
    }
}
