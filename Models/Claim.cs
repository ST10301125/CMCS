using System;
using System.ComponentModel.DataAnnotations;

namespace CMCS.Models
{
    public class Claim
    {
        public int ClaimID { get; set; }
        public int UserID { get; set; }
        public string Status { get; set; }
        public string LecturerName { get; set; }

        [DataType(DataType.Date)]
        public DateTime SubmittedDate { get; set; }
        public string SupportingDoc { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Hours worked must be a positive number.")]
        public int HoursWorked { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Hourly rate must be a positive number.")]
        public decimal HourlyRate { get; set; }
        public string Description { get; set; }  // Add this property if missing
        public int ManagerID { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public decimal TotalAmount => HoursWorked * HourlyRate;
        public string Notes { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }

        public DateTime? ApprovedDate { get; set; }
        public DateTime? RejectedDate { get; set; }
    }
}

/*
public int HoursWorked { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = "Hourly rate must be positive number")]
        public decimal HourlyRate { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public decimal TotalAmount { get; set; }
        public string Notes { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public DateTime? ApprovedDate { get; set; } // Date of approval, if approved
        public DateTime? RejectedDate { get; set; } // Date of rejection, if rejected
        public string Description { get; set; }

        public int? ManagerID { get; set; }

    }

}

*/