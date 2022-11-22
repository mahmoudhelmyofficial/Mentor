using Mentor.Models.Job;

namespace Mentor.ViewModels
{
    public class JobViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public byte[]? JobImage { get; set; }
        public int CategoryId { get; set; }
        public int Positions { get; set; }
        public string Experience { get; set; }
        public string CareerLevel { get; set; }
        public bool IsSelected { get; set; }
        public string JobDuration { get; set; }
        public string EducationLevel { get; set; }
        public string Salary { get; set; }
        public IEnumerable<JobCategory> Category { get; set; }
    }
}
