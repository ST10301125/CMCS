using CMCS.Models;
using System.Collections.Generic;

namespace CMCS.Models
{
    public class Manager
    {
        public int ManagerID { get; set; }  
        public string FirstName { get; set; } 
        public string LastName { get; set; }  
        public string Email { get; set; }    
        public string Password { get; set; }
        public string Department { get; set; }

        public List<Claim> ClaimsReview { get; set; }

        // Constructor
        public Manager()
        {
            ClaimsReview = new List<Claim>();
        }

        // Method to approve a claim
        public void ApproveClaim(Claim claim)
        {
            if (claim != null && claim.Status == "Pending")
            {
                claim.Status = "Approved";
                claim.ApprovedDate = DateTime.Now;
            }
        }

        // Method to reject a claim
        public void RejectClaim(Claim claim)
        {
            if (claim != null && claim.Status == "Pending")
            {
                claim.Status = "Rejected";
                claim.RejectedDate = DateTime.Now;
            }
        }

        // Method to retrieve claims that are pending for approval
        public List<Claim> GetPendingClaims()
        {
            return ClaimsReview.Where(c => c.Status == "Pending").ToList();
        }
    }

}

