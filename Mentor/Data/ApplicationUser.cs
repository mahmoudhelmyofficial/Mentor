using Mentor.Models.Job;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Mentor.Data
{
    public class ApplicationUser:IdentityUser
    {

        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? Birthdate { get; set; }
        public string? Gender { get; set; }
        public string? MaritalStatus { get; set; }
        public string? Nationality { get; set; }
        public byte[]? ProfilePicture { get; set; }
        public byte[]? CoverPicture { get; set; }
        public string? EmployeeCV { get; set; }
        public string? WebSite { get; set; }
        public string? Location { get; set; }
        public int? Founded { get; set; }
        public string? Industry { get; set; }
        public int? CompanySize { get; set; }
        public string? Specialities { get; set; }
        public string? AlternativeNumber { get; set; }

        public int? JobId { get; set; }
        public IEnumerable<Job>? OpenVacancies { get; set; }
    }
}
