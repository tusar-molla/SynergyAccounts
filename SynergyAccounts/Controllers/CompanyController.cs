using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IActionResult> CompanyAddUpdate()
        {
            var company = await _companyService.GetFirstCompanyAsync();
            if (company == null)
            {
                return View(new Company());
            }
            return View(company);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CompanyAddUpdate(Company company)
        {
            if (!ModelState.IsValid)
            {
                return View(company);
            }

            try
            {
                if (company.Id == 0)
                {                    
                    bool isCreated = await _companyService.CreateCompanyAsync(company);
                    if (!isCreated)
                    {
                        ModelState.AddModelError("Name", "A company already exists for this SubscriptionId.");
                        return View(company);
                    }
                }
                else
                {
                    bool isUpdated = await _companyService.UpdateCompanyAsync(company);
                    if (!isUpdated)
                    {
                        ModelState.AddModelError("Name", "Company name already exists or update failed.");
                        return View(company);
                    }
                }

                return RedirectToAction("Company");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred: " + ex.Message);
                return View(company);
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
                return View(company);
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