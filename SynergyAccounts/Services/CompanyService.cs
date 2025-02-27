using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using SynergyAccounts.Data;
using SynergyAccounts.Interface;
using SynergyAccounts.Models;
using Microsoft.AspNetCore.Mvc;

namespace SynergyAccounts.Services
{
    public class CompanyService : GenericService<Company>, ICompanyService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CompanyService(AppDbContext context, IWebHostEnvironment webHostEnvironment) : base(context)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<bool> IsCompanyNameExistsAsync(string name)
        {
            return await _context.Companies.AnyAsync(c => c.Name == name);
        }

        public async Task<bool> CreateCompanyAsync(Company company)
        {
            if (company.LogoImage != null)
            {
                company.LogoPath = await UploadFileAsync(company.LogoImage);
            }

            await AddAsync(company);
            return true;
        }

        public async Task<bool> UpdateCompanyAsync(Company company)
        {
            // Ensure the company exists
            var existingCompany = await GetByIdAsync(company.Id);
            if (existingCompany == null) return false;

            // Prevent duplicate names (except for itself)
            if (await _context.Companies.AnyAsync(c => c.Name == company.Name && c.Id != company.Id))
            {
                return false;
            }

            // Handle file upload
            if (company.LogoImage != null)
            {
                // Delete old logo if exists
                if (!string.IsNullOrEmpty(existingCompany.LogoPath))
                {
                    DeleteFile(existingCompany.LogoPath);
                }

                // Upload new logo
                existingCompany.LogoPath = await UploadFileAsync(company.LogoImage);
            }

            // Update other fields
            existingCompany.Name = company.Name;
            existingCompany.TagLine = company.TagLine;
            existingCompany.VatRegistrationNo = company.VatRegistrationNo;
            existingCompany.TinNo = company.TinNo;
            existingCompany.WebsiteLink = company.WebsiteLink;
            existingCompany.Email = company.Email;
            existingCompany.ContactNumber = company.ContactNumber;
            existingCompany.Address = company.Address;
            existingCompany.Remarks = company.Remarks;
            await UpdateAsync(existingCompany);
            return true;
        }

        private void DeleteFile(string filePath)
        {
            string fullPath = Path.Combine(_webHostEnvironment.WebRootPath, filePath.TrimStart('/'));
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }

        private async Task<string> UploadFileAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return string.Empty;

            string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            string uniqueFileName = $"{Guid.NewGuid()}_{file.FileName}";
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return $"/images/{uniqueFileName}"; // Return relative path
        }
    }
}
