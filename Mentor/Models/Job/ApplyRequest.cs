using Mentor.Data;

namespace Mentor.Models.Job
{
    public class ApplyRequest
    {
        public int Id { get; set; }
        public string Message { get; set; }


       // public int JobId { get; set; }

        //[ForeignKey(nameof(JobId))]
        //public Job Job { get; set; }

        public string EmployeeId { get; set; }
        public ApplicationUser Employee { get; set; }
    }
}
