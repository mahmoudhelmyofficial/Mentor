using Mentor.Data;
using Mentor.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace Mentor.Controllers
{
    public class EmployersController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUserStore<ApplicationUser> userStore;
        private readonly SignInManager<ApplicationUser> signInManager;

        //private readonly IUserEmailStore<ApplicationUser> emailStore;

        public EmployersController( 
            UserManager<ApplicationUser> userManager,
            IUserStore<ApplicationUser> userStore,
            SignInManager<ApplicationUser> signInManager
            )
            //IUserEmailStore<ApplicationUser> emailStore)
        {
            this.userManager = userManager;
            this.userStore = userStore;
            this.signInManager = signInManager;
            //this.emailStore = emailStore;
        }


        public ActionResult Register()=> View();


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(EmployersViewModel employers)
        {
            var employer = new ApplicationUser
            {
                FirstName = employers.FirstName,
                LastName = employers.LastName,
                Email=employers.Email
            };

            await userStore.SetUserNameAsync(employer, employers.UserName, CancellationToken.None);
            //await emailStore.SetEmailAsync(employer, employers.Email, CancellationToken.None);

            var result = await userManager.CreateAsync(employer, employers.Password);

            await userManager.AddToRoleAsync(employer, "Employer");

            return RedirectToAction(nameof(Index),"Home");
        }

        public ActionResult Index()
        {
            return View();
        }

        // GET: EmployersController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: EmployersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmployersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployersController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EmployersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployersController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EmployersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
