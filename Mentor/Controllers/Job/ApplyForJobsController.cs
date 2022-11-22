using Mentor.Data;
using Mentor.Models.Job;
using Mentor.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Mentor.Controllers
{
    [Authorize]
    public class ApplyForJobsController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;

        public ApplyForJobsController(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager )
        {
            this.context = context;
            this.userManager = userManager;
        }

        [Authorize(Roles ="Employer")]
        public async Task<ActionResult> AllRequestes()
        {
            var currentUser = userManager.FindByNameAsync(User.Identity.Name);

            var jobs = context.ApplyForJobs
                .Where(j => j.Job.JobPublisher.UserName == currentUser.Result.UserName)
                .Include(j => j.Employee).Include(r=>r.Job).ToList();

            return View(jobs);
        }




        [Authorize(Roles = "Employer")]
        public async Task<ActionResult> Requestes(int id)
        {
            var currentUser = userManager.FindByNameAsync(User.Identity.Name);

            var jobs = context.ApplyForJobs
                .Where(j => j.Job.JobPublisher.UserName == currentUser.Result.UserName && j.Job.JobId==id)
                .Include(j => j.Employee).Include(r => r.Job).ToList();

            return View(jobs);
        }


        public async Task<ActionResult> Index()
        {
            var currentUser = userManager.FindByNameAsync(User.Identity.Name);

            var jobs= context.ApplyForJobs
                .Where(j=>j.Employee.Id==currentUser.Result.Id)
                .Include(j=>j.Job).ToList();

            return View(jobs);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> opportunityDetials(int id)
        {
            var job =await context.Jobs.Include(j=>j.JobPublisher).Include(j=>j.Category)
                .FirstOrDefaultAsync(i=>i.JobId==id);

            var publisher =await userManager.FindByNameAsync(job.JobPublisher.UserName);

            var applicants = context.ApplyForJobs.Where(j => j.JobId == job.JobId).ToList().Count;

            var currentUser = userManager.FindByNameAsync(User.Identity?.Name);

            OpportunitiesDetailsViewModel jobVm = new OpportunitiesDetailsViewModel();

            jobVm.Opportunity = new ApplyForJobViewModel()
            {
                JobId=job.JobId,
                Title = job.Title,
                JobPublisher = publisher,
                PublishDate = job.DateTime,
                ApplicantsCount = applicants,
                Positoins = job.Positions,
                CareerLevel = job.CareerLevel,
                EducationLevel = job.EducationLevel,
                Experience = job.Experience,
                Salary = job.Salary,
                Category = job.Category.Name,
                JobDuration = job.JobDuration,
            };

            var applyed =context.ApplyForJobs
                .FirstOrDefault(j => j.Job.JobId == job.JobId && j.EmployeeId == currentUser.Result.Id);

            if (applyed!=null)
            {
                jobVm.Opportunity.Applyed = true;
            }

            //RELEVANT JOBS

            var res = context.Jobs.Where(j => j.Category.Name==job.Category.Name && j.JobId!=job.JobId)
                    .Include(j => j.Category).Include(j=>j.JobPublisher).ToList();

            jobVm.RelevantsJob = res;


            return View(jobVm);
        }


        [HttpGet]
        //[Authorize(Roles = "Employee")]
        public async Task<IActionResult> Apply(int id)
        {
            var job = await context.Jobs.FindAsync(id);

            var currentUser = userManager.FindByNameAsync(User.Identity?.Name);

            var applyjob = new ApplyForJob { Job = job };

            if (currentUser.Result.EmployeeCV != null)
            {
                applyjob.HasCV = true;
            }
            

            return View(applyjob);
        }
        

        [HttpPost]
        //[AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Apply(ApplyForJob jobApplyed)
        {
            var currentUser = userManager.FindByNameAsync(User.Identity?.Name);

            var job = new ApplyForJob
            {
                JobId = jobApplyed.Job.JobId,
                Message = jobApplyed.Message,
                EmployeeId = currentUser.Result.Id,
                ApplyTime=DateTime.Now,
                PublishTime=jobApplyed.PublishTime,
                EmployeeCV=currentUser.Result.EmployeeCV
            };

            await context.ApplyForJobs.AddAsync(job);
            context.SaveChanges();

            return RedirectToAction(nameof(Index), "Jobs");
        }



        // POST: ApplyForJobs/Delete/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {

            var job =await context.Jobs.FindAsync(id);

            var currentUser =await userManager.FindByNameAsync(User.Identity.Name);

            var apply = await context.ApplyForJobs
                .FirstOrDefaultAsync(a => a.JobId == job.JobId && a.EmployeeId == currentUser.Id);


            if (apply != null)
            {
                context.ApplyForJobs.Remove(apply);
            }

            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
