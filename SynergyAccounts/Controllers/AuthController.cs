using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SynergyAccounts.Data;
using System.Security.Claims;
using SynergyAccounts.DTOS.AuthDto;
using SynergyAccounts.Models;
using SynergyAccounts.Services;
using SynergyAccounts.Interface;
using Microsoft.AspNetCore.Http.HttpResults;

namespace SynergyAccounts.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
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
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                return View(registerDto);
            }

            try
            {
                var user = await _authService.RegisterAsync(registerDto);
                TempData["SuccessMessage"] = "Registration successful! Please log in.";

                return RedirectToAction("Login", "Auth");
            }
            catch (Exception ex)
            {
                
                TempData["ErrorMessage"] = "Registration Unsuccessful!"+ex.Message;

                // if (ex.Message == "Email already exists")
                // {
                //     ModelState.AddModelError("Email", "This email is already registered");
                // }
                // else
                // {
                //     ModelState.AddModelError("", "An error occurred during registration: " + ex.Message);
                // }
                return View(registerDto);
            }
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return View(loginDto);
            }
            try
            {
                var user = await _authService.LoginAsync(loginDto.Email, loginDto.Password);

                // Create claims for the user

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.FullName!),
                    new Claim(ClaimTypes.Email, user.Email!),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.RoleId == 1 ? "SuperAdmin" : user.RoleId == 2 ? "Admin" : "Operator"),
                    // Assuming RoleId 1 is SuperAdmin 2 is Admin 3 is Operator
                    new Claim("SubscriptionId", user.SubscriptionId.ToString()) // Add SubscriptionId as a custom claim
                };

                // Create identity and principal
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                // Sign in the user with cookie authentication
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    claimsPrincipal,
                    new AuthenticationProperties
                    {
                        IsPersistent = true, // Keeps the cookie across browser sessions
                        ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30), // Matches your 5-minute expiration
                        AllowRefresh = true // Enables sliding expiration
                    });

                TempData["SuccessMessage"] = $"Welcome back, {user.FullName}!";
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(loginDto);
            }
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Dashboard", "Home");
        }

        public IActionResult AccessDenied()
        {
            return View("AccessDenied");
        }
    }
}
