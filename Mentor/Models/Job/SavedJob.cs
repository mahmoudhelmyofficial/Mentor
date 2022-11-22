using Mentor.Data;

namespace Mentor.Models.Job
{
    public class SavedJob
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public Job Job { get; set; }
        public string UserName { get; set; }
    }
}
