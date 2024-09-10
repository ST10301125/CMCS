using System;
using System.ComponentModel.DataAnnotations;

namespace CMCS.Models
{
    public class Claim
    {
        public int ClaimID { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }

        public int HoursWorked { get; set; }

        public decimal TotalAmount { get; set; }

        public string Status { get; set; } 

        public int ContractID { get; set; }

    }
}
