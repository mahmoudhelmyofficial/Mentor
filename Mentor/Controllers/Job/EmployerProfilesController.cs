using Mentor.Data;
using Mentor.Models.Job;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Mentor.Controllers.Job
{
    public class EmployerProfilesController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUserStore<ApplicationUser> userStore;

        public EmployerProfilesController(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            IUserStore<ApplicationUser> userStore)
        {
            this.context = context;
            this.userManager = userManager;
            this.userStore = userStore;
        }

        public async Task<IActionResult> Index(string publisher)
        {
            var employer=await userManager.FindByNameAsync(publisher);

            var jobs = await context.Jobs.Include(p => p.JobPublisher).Where(j => j.JobPublisher.UserName == publisher).ToListAsync();

            var vacansies = new ApplicationUser
            {
                UserName = publisher,
                Location = employer.Location,
                Industry=employer.Industry,
                CompanySize=employer.CompanySize,
                Specialities=employer.Specialities,
                OpenVacancies=jobs
            };

            return View(vacansies);
        }

        public async Task<IActionResult> Update(string user)
        {
            var porfile = await userManager.FindByNameAsync(user);
            return View(porfile);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(ApplicationUser user)
        {
            var prof = await userManager.FindByNameAsync(User.Identity.Name);

            prof.Industry = user.Industry;
            prof.Location = user.Location;
            prof.CompanySize = user.CompanySize;
            prof.Specialities = user.Specialities;

            await userStore.SetUserNameAsync(prof, user.UserName, CancellationToken.None);
            await userManager.UpdateAsync(prof);

            return RedirectToAction(nameof(Index), "Jobs");

        }
    }
}
