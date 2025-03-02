using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SynergyAccounts.Interface;
using SynergyAccounts.Models;

namespace SynergyAccounts.Controllers
{
    [Authorize]
    public class CompanyController : Controller
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService ?? throw new ArgumentNullException(nameof(companyService));
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var companies = await _companyService.GetAllAsync();
                return View(companies);
            }
            catch (Exception ex)
            {
                // Log exception here in production
                return StatusCode(500, "An error occurred while retrieving companies."+ex.Message);
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var company = await _companyService.GetByIdAsync(id);
                return View(company);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                // Log exception here in production
                return StatusCode(500, "An error occurred while retrieving company details."+ex.Message);
            }
        }

        public IActionResult CreateCompany() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCompany(Company company)
        {
            if (!ModelState.IsValid)
            {
                return View(company);
            }

            try
            {
                bool isCreated = await _companyService.CreateCompanyAsync(company);
                if (!isCreated)
                {
                    ModelState.AddModelError("Name", "Company name already exists.");
                    return View(company);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Log exception here in production
                ModelState.AddModelError(string.Empty, "An error occurred while creating the company."+ex.Message);
                return View(company);
            }
        }

        public async Task<IActionResult> EditCompany(int id)
        {
            try
            {
                var company = await _companyService.GetByIdAsync(id);
                return View(company);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                // Log exception here in production
                return StatusCode(500, "An error occurred while retrieving company details."+ex.Message);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCompany(Company company)
        {
            if (!ModelState.IsValid)
            {
                return View(company);
            }

            try
            {
                bool isUpdated = await _companyService.UpdateCompanyAsync(company);
                if (!isUpdated)
                {
                    ModelState.AddModelError("Name", "Company name already exists or update failed.");
                    return View(company);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Log exception here in production
                ModelState.AddModelError(string.Empty, "An error occurred while updating the company."+ex.Message);
                return View(company);
            }
        }
    }
}