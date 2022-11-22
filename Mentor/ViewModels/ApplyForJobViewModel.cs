using Mentor.Data;
using Mentor.Models.Job;

namespace Mentor.ViewModels
{
    public class ApplyForJobViewModel
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public string Title { get; set; }
        public ApplicationUser JobPublisher { get; set; }
        public DateTime PublishDate { get; set; }
        public int Positoins { get; set; }
        public string Category { get; set; }
        public string Experience { get; set; }
        public string JobDuration { get; set; }
        public string CareerLevel { get; set; }
        public string EducationLevel { get; set; }
        public string Salary { get; set; }
        public bool Applyed { get; set; }
        public int ApplicantsCount { get; set; }

    }
}
