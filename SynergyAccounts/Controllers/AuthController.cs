using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SynergyAccounts.Data;
using System.Security.Claims;
using SynergyAccounts.DTOS.AuthDto;

namespace SynergyAccounts.Controllers
{
    public class AuthController : Controller
    {
        private AppDbContext _context;
        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == dto.Email && u.PasswordHash == dto.PasswordHash);
            if (user == null)
            {
                ViewBag.Error = "Invalid credentials";
                return View();
            }

            // Get user roles
            var roles = _context.UserRoles
                .Where(ur => ur.UserId == user.Id)
                .Select(ur => _context.Roles.FirstOrDefault(r => r.Id == ur.RoleId).Name)
                .ToList();

            // Create claims for authentication
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            // Add role claims
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Auth");
        }

        public IActionResult AccessDenied()
        {
            return View("AccessDenied");
        }
    }
}
