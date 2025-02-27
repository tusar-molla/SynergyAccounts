using Microsoft.AspNetCore.Mvc;
using SynergyAccounts.Interface;
using SynergyAccounts.Models;

namespace SynergyAccounts.Controllers
{
    public class CompanyController : Controller
    {

        private readonly ICompanyService _companyService;
        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }
        public async Task<IActionResult> Index()
        {
            var companies = await _companyService.GetAllAsync();
            return View(companies);
        }

        public async Task<IActionResult> Details(int id)
        {
            var company = await _companyService.GetByIdAsync(id);
            if (company == null) return NotFound();
            return View(company);
        }

        public IActionResult CreateCompany() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCompany(Company company)
        {
            if (ModelState.IsValid)
            {
                bool isCreated = await _companyService.CreateCompanyAsync(company);

                if (!isCreated)
                {
                    ModelState.AddModelError("Name", "Company name already exists.");
                    return View(company);
                }

                return RedirectToAction("Index");
            }
            return View(company);
        }

        public async Task<IActionResult> EditCompany(int id)
        {
            var company = await _companyService.GetByIdAsync(id);
            if (company == null) return NotFound();
            return View(company);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCompany(Company company)
        {
            if (ModelState.IsValid)
            {
                bool isUpdated = await _companyService.UpdateCompanyAsync(company);

                if (!isUpdated)
                {
                    ModelState.AddModelError("Name", "Company name already exists or update failed.");
                    return View(company);
                }

                return RedirectToAction("Index");
            }
            return View(company);
        }
    }
}
