namespace Mentor.Models.Learn
{
    public class CourseUnit
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int LessonsNumber { get; set; }
        public List<CourseLesson> CourseLessons { get; set; }
        public Course Course { get; set; }
        public int CourseId { get; set; }
    }
}
