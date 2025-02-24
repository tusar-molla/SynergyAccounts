using Microsoft.AspNetCore.Mvc;
using SynergyAccounts.Models;

namespace SynergyAccounts.Controllers
{
    public class CompanyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateCompany()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCompany(Company company)
        {
            if (ModelState.IsValid)
            {
             return RedirectToAction("Index");
            }
            return View(company);
        }
    }
}
