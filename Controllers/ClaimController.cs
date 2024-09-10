using Microsoft.AspNetCore.Mvc;
using CMCS.Data;
using CMCS.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace CMCS.Controllers
{
    public class ClaimController : Controller
    {
        private readonly Context _context;

        public ClaimController(Context context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var claims = _context.Claims.ToList();
            return View(claims);
        }

        public IActionResult Submit()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Submit(Claim claim)
        {
            if (ModelState.IsValid)
            {
                claim.Status = "Pending";
                _context.Claims.Add(claim);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(claim);
        }

        // Details
        public async Task<IActionResult> Details(int Id)
        {
            var claim = await _context.Claims.FindAsync(Id);
            if (claim == null)
            {
                return NotFound();
            }

            var documents = _context.SupportingDocs.Where(d => d.ClaimID == Id).ToList();
            ViewBag.Documents = documents;

            return View(claim);
        }

        // Approve
        public async Task<IActionResult> Approve(int id)
        {
            var claim = await _context.Claims.FindAsync(id);
            if (claim == null)
            {
                return NotFound();
            }

            claim.Status = "Approved";
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Rejected
        public async Task<IActionResult> Rejected(int id)
        {
            var claim = await _context.Claims.FindAsync(id);
            if (claim == null)
            {
                return NotFound();
            }

            claim.Status = "Rejected";
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // UploadDocument
        [HttpPost]
        public async Task<IActionResult> UploadDocument(int claimId, IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                var filePath = $"wwwroot/uploads/{file.FileName}";

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                var document = new SupportingDocs
                {
                    FileName = file.FileName,
                    FilePath = filePath,
                    ClaimID = claimId
                };

                _context.SupportingDocs.Add(document);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Details), new { id = claimId });
        }
    }
}

        
        
        
