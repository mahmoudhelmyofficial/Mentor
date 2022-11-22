using Microsoft.AspNetCore.Identity;

namespace Mentor.Models.Job
{
    public class EmployerProfile:IdentityUser
    {
        public byte[]? ProfilePicture { get; set; }
        public byte[]? CoverPicture { get; set; }
        public string WebSite { get; set; }
        public string Location { get; set; }
        public int Founded { get; set; }
        public string Industry { get; set; }
        public int CompanySize { get; set; }
        public string Specialities { get; set; }
        public ICollection<Job> OpenVacancies { get; set; }

    }
}
