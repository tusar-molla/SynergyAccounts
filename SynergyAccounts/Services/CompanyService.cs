using Microsoft.EntityFrameworkCore;
using SynergyAccounts.Data;
using SynergyAccounts.Interface;
using SynergyAccounts.Models;
using System.Security.Claims;


namespace SynergyAccounts.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CompanyService(AppDbContext context, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
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
            var subscriptionIdClaim = _httpContextAccessor.HttpContext?.User.FindFirst("SubscriptionId")?.Value;
            if (subscriptionIdClaim != null) {
                company.SubscriptionId = int.Parse(subscriptionIdClaim);
            }
            await _context.AddAsync(company);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateCompanyAsync(Company company)
        {
            var existingCompany = await _context.Companies.FirstOrDefaultAsync(c => c.Id == company.Id);
            if (existingCompany == null) return false;

            if (await _context.Companies.AnyAsync(c => c.Name == company.Name && c.Id != company.Id))
            {
                return false;
            }
            if (company.LogoImage != null)
            {
                if (!string.IsNullOrEmpty(existingCompany.LogoPath))
                {
                    DeleteFile(existingCompany.LogoPath);
                }
                existingCompany.LogoPath = await UploadFileAsync(company.LogoImage);
            }

            existingCompany.Name = company.Name;
            existingCompany.TagLine = company.TagLine;
            existingCompany.VatRegistrationNo = company.VatRegistrationNo;
            existingCompany.TinNo = company.TinNo;
            existingCompany.WebsiteLink = company.WebsiteLink;
            existingCompany.Email = company.Email;
            existingCompany.ContactNumber = company.ContactNumber;
            existingCompany.Address = company.Address;
            existingCompany.Remarks = company.Remarks;
            _context.Update(existingCompany);
            await _context.SaveChangesAsync();
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

            return $"/images/{uniqueFileName}";
        }
    }
}
