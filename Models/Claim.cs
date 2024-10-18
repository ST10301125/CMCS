using System;
using System.ComponentModel.DataAnnotations;

namespace CMCS.Models
{
    public class Claim
    {
        public int ClaimID { get; set; }

        public int LecturerName { get; set; }
     
        public int Month { get; set; }

        public int Year { get; set; }

        public decimal TotalAmount { get; set; }

        public string Status { get; set; } 

        public int ContractID { get; set; }

        [DataType(DataType.Date)]
        public DateTime SubmittedDate { get; set; }
        
        public string SupportingDoc { get; set; }
        public int HoursWorked { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = "Hourly rate must be positive number")]
        public decimal HourlyRate { get; set; }

        public string Notes { get; set; }

        public string FileName { get; set; }

        public string FilePath { get; set; }

    }

}

