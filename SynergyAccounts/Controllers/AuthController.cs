using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SynergyAccounts.Data;
using System.Security.Claims;
using SynergyAccounts.DTOS.AuthDto;
using SynergyAccounts.Models;
using SynergyAccounts.Services;

namespace SynergyAccounts.Controllers
{
    public class AuthController : Controller
    {
        private AppDbContext _context;
        private HashedPassword _hashedPassword;
        public AuthController(AppDbContext context, HashedPassword hashPassword)
        {
            _context = context;
            _hashedPassword = hashPassword;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]

        public IActionResult Register()
        {
            return View();
        }

        // POST Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterDto rdto)
        {
            if (ModelState.IsValid)
            {
                // Check if the user already exists
                var existingUser = _context.Users.FirstOrDefault(u => u.Email == rdto.Email);
                if (existingUser != null)
                {
                    ModelState.AddModelError(string.Empty, "Email already in use.");
                    return View(rdto);
                }
                var user = new User
                {
                    UserName = rdto.UserName,
                    FullName = rdto.FullName,
                    Email = rdto.Email,
                    PasswordHash = _hashedPassword.HashPassword(rdto.PasswordHash),
                    ContactNumber = rdto.ContactNumber,
                    Address = rdto.Address,
                    RoleId = rdto.RoleId
                };

                // Add the user to the database
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction("Login", "Auth");
            }

            return View(rdto);
        }


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginDto ldto)
        {
            if (ModelState.IsValid)
            {
                // Check if the user exists
                var user = _context.Users.FirstOrDefault(u => u.Email == ldto.Email);

                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(ldto);
                }

                // Declare variables before the conditions
                List<Claim> claims;
                ClaimsIdentity claimsIdentity;
                ClaimsPrincipal claimsPrincipal;

                // Handle plain text password for admin users
                if (user.PasswordHash == "123456")
                {
                    // Check if the entered password is the plain text one used for admin
                    if (ldto.PasswordHash == "123456")
                    {
                        // Admin login successful
                        claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim("FullName", user.FullName),
                    new Claim("Email", user.Email),
                    new Claim(ClaimTypes.Role, user.Role?.Name ?? "SuperAdmin")
                };
                    }
                    else
                    {
                        // Incorrect password for admin
                        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                        return View(ldto);
                    }
                }
                else
                {
                    // Handle hashed password for regular users
                    var Passhash = _hashedPassword.HashPassword(ldto.PasswordHash);

                    if (user.PasswordHash != Passhash)
                    {
                        // Password does not match
                        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                        return View(ldto);
                    }

                    // User login successful (hashed password scenario)
                    claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim("FullName", user.FullName),
                new Claim("Email", user.Email),
                //new Claim(ClaimTypes.Role, user.Role?.Name ?? "User")
            };
                }

                // Initialize claimsIdentity and claimsPrincipal after claims are set
                claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                // Sign in the user
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

                return RedirectToAction("Dashboard", "Home");
            }

            return View(ldto);
        }

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
