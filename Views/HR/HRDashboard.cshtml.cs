using CMCS.Data;
using CMCS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

public class HRModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public HRModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public List<Lecturer> Lecturers { get; set; }
    [BindProperty]
    public string ReportType { get; set; }
    public string GenerateReport { get; set; }

    public void OnGet()
    {
        Lecturers = _context.Lecturer.ToList();
    }

    public IActionResult OnPostGenerateReport()
    {
        // Logic to generate a report based on the selected type
        GenerateReport = $"Report for {ReportType} generated at {DateTime.Now}";
        return Page();
    }
}
