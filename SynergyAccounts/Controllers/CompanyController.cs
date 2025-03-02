﻿using Microsoft.AspNetCore.Authorization;
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
            _companyService = companyService;
        }
        [HttpGet]
        public async Task<IActionResult> Company()
        {
            var company = await _companyService.GetCompanyBySubscriptionId();
            if (company == null)
            {
                return View(new Company());
            }
            return View(company);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Company(Company company)
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
                        TempData["ErrorMessage"] = "A company already exists for this SubscriptionId.";
                        return View(company);
                    }
                }
                else
                {
                    bool isUpdated = await _companyService.UpdateCompanyAsync(company);
                    if (!isUpdated)
                    {
                        TempData["ErrorMessage"] = "Company name already exists or update failed";
                        return View(company);
                    }
                }
                TempData["SuccessMessage"] = "Company Created Successfully";
                return RedirectToAction("Company");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred: " + ex.Message);
                return View(company);
            }
        }
    }
}