using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mentor.Data;
using Mentor.Models.Job;
using Mentor.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Mentor.Controllers
{

    public class JobsController : Controller
    {   
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> userManager;

        public JobsController(ApplicationDbContext context,UserManager<ApplicationUser> userManager)
        {
            _context = context;
            this.userManager = userManager;
        }

        // GET: Jobs
        public async Task<IActionResult> Index()
        {
            var jobs = await _context.Jobs.Include(j=>j.Category)
                .Include(p=>p.JobPublisher)
                .OrderBy(c=>c.Category.Name).ToListAsync();

            return View(jobs);
        }

        public async Task<IActionResult> Search(string term)
        {
            var res=_context.Jobs.Where(j => j.Title.Contains(term)||j.JobPublisher.UserName
                .Contains(term)).Include(j=>j.Category).ToList();

            return View(nameof(Index), res);
        }



        // GET: Jobs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Jobs == null)
            {
                return NotFound();
            }

            var job = await _context.Jobs
                .FirstOrDefaultAsync(m => m.JobId == id);
            if (job == null)
            {
                return NotFound();
            }

            return View(job);
        }


        [Authorize(Roles = "Employer")]
        public IActionResult Create()
        {
            var categories=_context.JobCategories.ToList();

            var vm = new JobViewModel{Category = categories};

            return View(vm);
        }


        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(JobViewModel job)
        {
            var currentUser = userManager.FindByNameAsync(User.Identity.Name);

            string jobDuration;

            if (job.IsSelected==true)
            {
                jobDuration = "FullTime";
                
            }
            else
            {
                jobDuration = "PartTime";
            }

            var newJob = new Mentor.Models.Job.Job
            {
                Title = job.Title,
                Description = job.Description,
                CategoryId = job.CategoryId,
                JobPublisher = currentUser.Result,
                DateTime=DateTime.Now,
                Positions=job.Positions,
                CareerLevel=job.CareerLevel,
                EducationLevel=job.EducationLevel,
                Experience=job.Experience,
                Salary=job.Salary,
                JobDuration= jobDuration
            };

            _context.Add(newJob);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Jobs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Jobs == null)
            {
                return NotFound();
            }

            var job = await _context.Jobs.FindAsync(id);
            if (job == null)
            {
                return NotFound();
            }
            return View(job);
        }

        // POST: Jobs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Mentor.Models.Job.Job job)
        {
            if (id != job.JobId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(job);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobExists(job.JobId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(job);
        }

        //// GET: Jobs/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null || _context.Jobs == null)
        //    {
        //        return NotFound();
        //    }

        //    var job = await _context.Jobs
        //        .FirstOrDefaultAsync(m => m.JobId == id);
        //    if (job == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(job);
        //}

        // POST: Jobs/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            if (_context.Jobs == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Jobs'  is null.");
            }
            var job = await _context.Jobs.FindAsync(id);
            if (job != null)
            {
                _context.Jobs.Remove(job);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JobExists(int id)
        {
          return (_context.Jobs?.Any(e => e.JobId == id)).GetValueOrDefault();
        }
    }
}
