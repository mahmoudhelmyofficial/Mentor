using Mentor.Models;
using Mentor.Models.Learn;

namespace Mentor.ViewModels
{
    public class CourseDetailsViewModel
    {
        public Course? CourseDetails { get; set; }   
        public List<CourseComments>? CourseComments { get; set; }
        public List<CourseUnit>? CourseUnits { get; set; }
        public List<CourseLesson>? CourseLessons { get; set; }
    }
}
