using System;
using System.ComponentModel.DataAnnotations;

namespace CMCS.Models
{
    public class SupportingDocs
    {
        public int DocumentID { get; set; }

        public string FileName { get; set; }

        public string FilePath { get; set; }

        public int ClaimID { get; set; }

    }
}
