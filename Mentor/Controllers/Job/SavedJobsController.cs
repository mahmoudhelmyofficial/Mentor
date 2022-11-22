using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mentor.Data;
using Mentor.Models.Job;

namespace Mentor.Controllers.Job
{
    public class SavedJobsController : Controller
    {
        private readonly ApplicationDbContext context;

        public SavedJobsController(ApplicationDbContext context)
        {
            context = context;
        }

        // GET: SavedJobs
        public IActionResult Index()
        {
            var jobs = context.SavedJobs.Include(s => s.UserName).Include(j=>j.Job.Category)
                .Where(j=>j.UserName==User.Identity.Name).ToList();

            return View(jobs);
        }

        [HttpPost]
        public async Task<IActionResult> Save(int id)
        {
            var job = await context.Jobs.FindAsync(id);

            SavedJob savedJob = new SavedJob
            {
                Job = job,
                JobId = job.JobId,
                UserName = User.Identity.Name
            };

            await context.SavedJobs.AddAsync(savedJob);
            context.SaveChanges();

            return View(Index);
        } 







    }
}
