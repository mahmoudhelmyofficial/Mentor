namespace Mentor.Models.Learn
{
    public class CourseLesson
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? LessonDetails { get; set; }
        public string? LessonFileUrl { get; set; }
        public CourseUnit CourseUnit  { get; set; }
        public int CourseUnitId { get; set; }
    }
}
