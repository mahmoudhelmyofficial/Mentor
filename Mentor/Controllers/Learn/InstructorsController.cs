using Mentor.Data;
using Mentor.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Mentor.Controllers.Learn
{
    public class InstructorsController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUserStore<ApplicationUser> userStore;

        public InstructorsController(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            IUserStore<ApplicationUser> userStore)
        {
            this.context = context;
            this.userManager = userManager;
            this.userStore = userStore;
        }


        public ActionResult Register() => View();


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(InstructorsViewModel instructors)
        {
            var instructor = new ApplicationUser
            {
                FirstName = instructors.FirstName,
                LastName = instructors.LastName,
                Email = instructors.Email
            };

            await userStore.SetUserNameAsync
                (instructor, instructors.UserName, CancellationToken.None);

            var result = await userManager.CreateAsync(instructor, instructors.Password);

            await userManager.AddToRoleAsync(instructor, "Instructor");

            return RedirectToAction(nameof(Index), "Home");
        }
    }
}
