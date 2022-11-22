using Mentor.Data;
using Mentor.Models.Learn;

namespace Mentor.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Instructor { get; set; }
        public DateTime PublishDate { get; set; }
        public int StudentsCount { get; set; }
        public double CoursePrice { get; set; }
        public bool IfSubscribe { get; set; }
        public byte[]? CoursePoster { get; set; }
        public ICollection<CourseComments> Comments { get; set; }
        public ICollection<CourseSubscriptions> CourseSubscriptions { get; set; }
        public ICollection<CourseUnit> CourseUnits { get; set; }
        public ICollection<CourseLesson> CourseLessons { get; set; }
    }
}
