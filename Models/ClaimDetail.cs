namespace CMCS.Models
{
    public class ClaimDetail
    {
        public int ClaimId { get; set; }
        public string ClaimNumber { get; set; }
        public string LecturerName { get; set; }
        public DateTime DateSubmitted { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string SupportingDocumentPath { get; set; }
    }
}
