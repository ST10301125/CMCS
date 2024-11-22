using Microsoft.AspNetCore.Mvc;
using CMCS.Data;
using CMCS.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Data;

namespace CMCS.Controllers
{
    public class ClaimController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;
        public ClaimController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        public IActionResult Index()
        {
            // Retrieve Role from session
            var role = HttpContext.Session.GetString("UserRole");
            
            if (string.IsNullOrEmpty(role))
            {
                return RedirectToAction("Login", "Login");
            }

            switch (role)
            {
                case "Lecturer":
                    return RedirectToAction("Submit", "Claim");
                case "Manager":
                    return RedirectToAction("Pending", "Claim");
                case "HR":
                    return RedirectToAction("Index", "HR");
                default:
                    // If the role is unrecognized, redirect to the login page
                    return RedirectToAction("Login", "Login");
            }
        }

        [HttpGet]
        public IActionResult Submit()
        {
            // Return the Submit view
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Submit(Claim model, IFormFile SupportingDoc)
        {
            if (ModelState.IsValid)
            {
                if (SupportingDoc != null)
                {
                    if (SupportingDoc.Length > 5 * 1024 * 1024)
                    {
                        ModelState.AddModelError("FileSize", "File size should not exceed 5MB.");
                        return View(model);
                    }

                    var allowedExtensions = new[] { ".pdf", ".docx", ".xlsx" };
                    var fileExtension = Path.GetExtension(SupportingDoc.FileName).ToLower();
                    if (!allowedExtensions.Contains(fileExtension))
                    {
                        ModelState.AddModelError("FileType", "Only .pdf, .docx, and .xlsx files are allowed.");
                        return View(model);
                    }

                    var uploads = Path.Combine(_environment.WebRootPath, "uploads");
                    if (!Directory.Exists(uploads)) Directory.CreateDirectory(uploads);

                    var fileName = $"{Guid.NewGuid()}{fileExtension}";
                    var filePath = Path.Combine(uploads, fileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await SupportingDoc.CopyToAsync(fileStream);
                    }

                    model.FileName = SupportingDoc.FileName;
                    model.FilePath = fileName;
                }

                model.Status = "Pending";
                model.SubmittedDate = DateTime.Now;

                // Save to database
                _context.Claims.Add(model);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(model);
        }

        public IActionResult Pending()
        {
            var pendingClaims = _context.Claims.Where(c => c.Status == "Pending").ToList();
            return View(pendingClaims);
        }
        [HttpPost]
        public async Task<IActionResult> ApproveClaim(int id)
        {
            var claim = await _context.Claims.FindAsync(id);
            if (claim != null)
            {
                claim.Status = "Approved";
                claim.ApprovedDate = DateTime.Now;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Pending");
        }

        [HttpPost]
        public async Task<IActionResult> RejectClaim(int id)
        {
            var claim = await _context.Claims.FindAsync(id);
            if (claim != null)
            {
                claim.Status = "Rejected";
                claim.RejectedDate = DateTime.Now;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Pending");
        }
    }
}







/*var claim = new Claim
                {
                    HoursWorked = model.HoursWorked,
                    HourlyRate = model.HourlyRate,
                    Notes = model.Notes,
                    SubmittedDate = DateTime.Now,
                    Status = "Pending"
                };

                _context.Claims.Add(claim);
                await _context.SaveChangesAsync();

                return RedirectToAction("SubmissionReceived");
            }
            return View(model);
        }

        // Pending
        public IActionResult Pending()
        {
            var pendingClaims = _context.Claims.Where(c => c.Status == "Pending").ToList();
            return View(pendingClaims);
        }

        // Approve
        [HttpPost]
        public async Task<IActionResult> Approve(int ClaimID)
        {
            var claim = await _context.Claims.FindAsync(ClaimID);
            if (claim! == null)
            {
                claim.Status = "Approved";
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        // Rejected
        [HttpPost]
        public async Task<IActionResult> Rejected(int ClaimID)
        {
            var claim = await _context.Claims.FindAsync(ClaimID);
            if (claim! == null)
            {
                claim.Status = "Rejected";
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        // Upload Document 
        [HttpPost]
        public async Task<IActionResult> SubmitClaim(Claim claim, IFormFile SupportingDoc)
        {
            if (ModelState.IsValid)
            {
                // Handle file upload (if there is a file uploaded)
                if (SupportingDoc != null)
                {
                    if (SupportingDoc.Length > 5 * 1024 * 1024)  // File size limit of 5MB
                    {
                        ModelState.AddModelError("FileSize", "File size should not exceed 5MB.");
                        return View(claim);
                    }

                    // Restrict file types
                    var allowedExtensions = new[] { ".pdf", ".docx", ".xlsx" };
                    var fileExtension = Path.GetExtension(SupportingDoc.FileName).ToLower();
                    if (!allowedExtensions.Contains(fileExtension))
                    {
                        ModelState.AddModelError("FileType", "Only .pdf, .docx, and .xlsx files are allowed.");
                        return View(claim);
                    }

                    // Save the file to the server
                    var uploads = Path.Combine(_environment.WebRootPath, "uploads");
                    if (!Directory.Exists(uploads)) Directory.CreateDirectory(uploads);  // Ensure the uploads directory exists

                    var fileName = $"{Guid.NewGuid()}{fileExtension}";  // Use a consistent naming convention
                    var filePath = Path.Combine(uploads, fileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await SupportingDoc.CopyToAsync(fileStream);
                    }

                    // Update claim with file details
                    claim.FileName = SupportingDoc.FileName;
                    claim.FilePath = fileName;
                }

                // Set additional claim data
                claim.Status = "Pending";
                claim.SubmittedDate = DateTime.Now;

                // Save claim to database
                _context.Claims.Add(claim);
                await _context.SaveChangesAsync();

                return RedirectToAction("PendingClaim");
            }

            // If model state is invalid, return the form with errors
            return View(claim);
        }
    }
    }

 */       
        
        
