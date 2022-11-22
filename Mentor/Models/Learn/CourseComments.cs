namespace Mentor.Models
{
    public class CourseComments
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public Course Course { get; set; }
        public int CourseId { get; set; }
        public string CommentWriter { get; set; }
    }
}
