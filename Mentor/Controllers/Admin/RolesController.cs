using Mentor.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Mentor.Controllers
{
    [Authorize]
    public class RolesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public RolesController(
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)

        {
            this.roleManager = roleManager;
            _context = context;
            this.userManager = userManager;
        }
        public ActionResult Index() => View(roleManager.Roles.ToList());


        public ActionResult Create()=> View();
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(IdentityRole role)
        {
            await roleManager.CreateAsync(role);
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> ManagePermessions(string id)
        {
            return View();
        }

        public IActionResult Delete(string id,int? none)
        {
            var role = _context.Roles.Find(id);
            return View(role);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            if (role != null)
            {
                await roleManager.DeleteAsync(role);
            }

            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
