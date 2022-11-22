using Mentor.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Mentor.Controllers.Job
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUserStore<ApplicationUser> userStore;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IWebHostEnvironment hosting;

        public EmployeeController(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            IUserStore<ApplicationUser> userStore,
            SignInManager<ApplicationUser> signInManager,
            IWebHostEnvironment hosting)
        {
            this.context = context;
            this.userManager = userManager;
            this.userStore = userStore;
            this.signInManager = signInManager;
            this.hosting = hosting;
        }

        public async Task<IActionResult> Profile(string username)
        {
            var employee = await userManager.FindByNameAsync(username);
            
            return View(employee);
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

            prof.FirstName = user.FirstName;
            prof.LastName = user.LastName;
            prof.MiddleName = user.MiddleName;
            prof.UserName = user.UserName;
            prof.Email = user.Email;
            prof.Location = user.Location;
            prof.Birthdate = user.Birthdate ;
            prof.Nationality = user.Nationality;
            prof.MaritalStatus = user.MaritalStatus;
            prof.PhoneNumber = user.PhoneNumber;
            prof.AlternativeNumber = user.AlternativeNumber;
            prof.Gender = user.Gender;

            await userManager.UpdateAsync(prof);

            using var dataStream = new MemoryStream();

            if (prof.ProfilePicture != dataStream.ToArray())
            {
                var file = Request.Form.Files;
                var poster = file.FirstOrDefault();
                if (file.Any())
                    await poster.CopyToAsync(dataStream);
                prof.ProfilePicture = dataStream.ToArray();

                await signInManager.RefreshSignInAsync(prof);
                await userManager.UpdateAsync(prof);
            }


            var cVfile = Request.Form.Files.LastOrDefault();
            var filename = string.Empty;

            if (cVfile != null && ".pdf".Contains(Path.GetExtension(cVfile.FileName).ToLower().Trim())) 
            {
                string uplod = Path.Combine(hosting.WebRootPath, "CVs");
                filename = cVfile.FileName;
                string fullPath = Path.Combine(uplod, filename);
                cVfile.CopyTo(new FileStream(fullPath,FileMode.Create));

                prof.EmployeeCV = filename;
                await signInManager.RefreshSignInAsync(prof);
                await userManager.UpdateAsync(prof);
            }
            

            else
            {
                await signInManager.RefreshSignInAsync(prof);
            }
            
            return RedirectToAction(nameof(Index),"Jobs");
        }
    }
}
