using Mentor.Data;
using Microsoft.AspNetCore.Identity;

namespace Mentor.Models.Learn
{
    public class Instructor:IdentityUser
    {
        public byte[]? ProfilePicture { get; set; }
        public byte[]? CoverPicture { get; set; }
        public string? WebSite { get; set; }
        public int Rating { get; set; }
        public int Reviews { get; set; }
        public int Students { get; set; }
        public int CoursesCount { get; set; }
        public ICollection<Course> AllCourses { get; set; }

        
    }
}
