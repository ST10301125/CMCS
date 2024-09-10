namespace CMCS.Models
{
    public class ClaimStatus
    {
        public int ClaimId { get; set; }
        public string ClaimNumber { get; set; }
        public DateTime DateSubmitted { get; set; }
        public string Status { get; set; }
        public string SupportingDocumentPath { get; set; }
    }
}
