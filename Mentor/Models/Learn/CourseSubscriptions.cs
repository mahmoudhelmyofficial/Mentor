using Mentor.Data;
using Mentor.Models.Learn;

namespace Mentor.Models
{
    public class CourseSubscriptions
    {
        public int Id { get; set; }
        public string StudentUser { get; set; }
        public DateTime SubDate { get; set; }
        public Course Course { get; set; }
        public int CourseId { get; set; }
        public ApplicationUser Student { get; set; }
        public Instructor Instructor { get; set; }
        public string InstructorId { get; set; }


    }
}
