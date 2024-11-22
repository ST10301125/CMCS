using CMCS.Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
  
namespace CMCS.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public LoginController(ILogger<LoginController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        private static readonly List<User> Users = new List<User>
        {
            new User { Email = "manager@gmail.com", Password = "manager123", Role = "Manager" },
            new User { Email = "hr@gmail.com", Password = "hr123", Role = "HR" },
            new User { Email = "lecturer@gmail.com", Password = "lecturer123", Role = "Lecturer" }
        };


       /* public IActionResult RoleSelection()
        {
            // Redirect to login page if the role has already been selected
            if (TempData["SelectedRole"] != null)
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        // Post method to handle role selection
        [HttpPost]
        public IActionResult RoleSelection(string role)
        {
            // Store the selected role in TempData for the next action
            TempData["SelectedRole"] = role;

            // Redirect to the login page after role selection
            return RedirectToAction("Login");
        }
       */

        [HttpGet]
        public IActionResult Login()
        {
            // If no role has been selected yet, redirect to the role selection page
            if (TempData["SelectedRole"] == null)
            {
                return RedirectToAction("RoleSelection");
            }

            return View();
        }

        [HttpPost]
        public IActionResult Login(User model)
        {
            if (ModelState.IsValid)
            {
                var user = Users.FirstOrDefault(u =>
                    u.Email == model.Email &&
                    u.Password == model.Password);

                if (user != null)
                {
                    HttpContext.Session.SetString("UserRole", user.Role);

                    if (user.Role == "Lecturer")
                    {
                        return RedirectToAction("Submit", "Claim");
                    }
                    else if (user.Role == "Manager")
                    {
                        return RedirectToAction("Pending", "Claim");
                    }
                    else if (user.Role == "HR")
                    {
                        return RedirectToAction("Index", "HR");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid login attempt.");
                    ViewData["ErrorMessage"] = "Invalid login attempt.";
                }


                return View(model);
            }
        }
    }
}

