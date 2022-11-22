using Mentor.Data;
using System.ComponentModel.DataAnnotations;

namespace Mentor.Models.Job
{
    public class Job
    {
        public int JobId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public byte[]? JobImage { get; set; }

        public string Experience { get; set; }
        public string CareerLevel { get; set; }
        public string EducationLevel { get; set; }
        public string Salary { get; set; }
        public ApplicationUser JobPublisher { get; set; }
        public int CategoryId { get; set; }
        public int Positions { get; set; }
        public string JobDuration { get; set; }
        public JobCategory Category { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime DateTime { get; set; }

    }
}
