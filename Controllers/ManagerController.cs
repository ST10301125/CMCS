using CMCS.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Microsoft.AspNetCore.Http;

[Authorize(Roles = "Manager")]
public class ManagerController : Controller
{
    private readonly ApplicationDbContext _context;

    public ManagerController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Login(string Email, string Password)
    {
        const string DemoEmail = "manager@gmail.com";
        const string DemoPassword = "manager123";

        Console.WriteLine($"Email: {Email}, Password: {Password}");

        if (Email.Trim().ToLower() == DemoEmail && Password == DemoPassword)
        {
            // Simulating successful login
            HttpContext.Session.SetInt32("ManagerID", 1); // Assign a dummy manager ID
            return RedirectToAction("Dashboard");
        }
        else
        {
            ModelState.AddModelError("", "Invalid login attempt.");
            return View();
        }
    }

    [Authorize(Roles = "Manager")]
    public IActionResult Dashboard()
    {
        var managerId = HttpContext.Session.GetInt32("ManagerID");

        if (managerId == null)
        {
            return RedirectToAction("Login");
        }

        return View();
    }

    [Authorize(Roles = "Manager")]
    public IActionResult ManageClaims()
    {
        var managerId = HttpContext.Session.GetInt32("ManagerID");

        if (managerId == null)
        {
            return RedirectToAction("Login");
        }

        var claims = _context.Claims.Where(c => c.ManagerID == managerId).ToList();
        return View(claims);
    }


    // Show pending claims for the manager
    [Authorize(Roles = "Manager")]
    public IActionResult PendingClaims()
    {
        var managerId = HttpContext.Session.GetInt32("ManagerID");

        if (managerId == null)
        {
            return RedirectToAction("Login");
        }
        
        var pendingClaims = _context.Claims
            .Where(c => c.Status == "Pending" && c.ManagerID == managerId)
            .ToList();
        return View(pendingClaims);
    }

    // Approve a claim
    [HttpPost]
    [Authorize(Roles = "Manager")]
    public async Task<IActionResult> ApproveClaim(int claimId)
    {
        var claim = await _context.Claims.FindAsync(claimId);
        if (claim != null && claim.Status == "Pending")
        {
            claim.Status = "Approved";
            claim.ApprovedDate = DateTime.Now;
            await _context.SaveChangesAsync();
        }
        return RedirectToAction("PendingClaims");
    }

    // Reject a claim with reason
    [HttpPost]
    public async Task<IActionResult> RejectClaim(int claimId)
    {
        var claim = await _context.Claims.FindAsync(claimId);
        if (claim != null && claim.Status == "Pending")
        {
            claim.Status = "Rejected";
            claim.RejectedDate = DateTime.Now;
            await _context.SaveChangesAsync();
        }
        return RedirectToAction("PendingClaims");
    }
}
