using Mentor.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mentor.Models.Job
{
    public class ApplyForJob
    {
        public int Id { get; set; }
        public string Message { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime PublishTime { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime ApplyTime { get; set; }
        public int JobId { get; set; }
        public string EmployeeCV { get; set; }
        public bool HasCV { get; set; }

        public Job Job { get; set; }

        public string EmployeeId { get; set; }
        public ApplicationUser Employee { get; set; }
    }
}
