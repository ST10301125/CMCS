namespace CMCS.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public int? LecturerID { get; set; }    // Foreign key to Lecturer model, nullable if the user is not a lecturer
        public Lecturer Lecturer { get; set; }

    } 
}