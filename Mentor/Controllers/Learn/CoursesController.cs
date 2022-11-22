using Mentor.Data;
using Mentor.Models;
using Mentor.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Mentor.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;

        public CoursesController(ApplicationDbContext context,UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public ActionResult Index() => View(context.Courses.ToList());


        public async Task<IActionResult> Search(string term)
        {

            var res = context.Courses.Where(j => j.Title.Contains(term) || j.Instructor.Contains(term)).ToList();

            return View(nameof(Index), res);
        }

        public async Task<ActionResult> Details(int id)
        {

            var course = await context.Courses.Include(j=>j.CourseSubscriptions).FirstOrDefaultAsync(c=>c.Id==id);

            var subscriber = await context.Subscriptions.FirstOrDefaultAsync(s => s.StudentUser == User.Identity.Name);

            if (subscriber!=null)
            {
                course.IfSubscribe = true;
            }

            var list = await context.Subscriptions.Where(c => c.CourseId == course.Id).ToListAsync();

            var count = list.Count;

            var comments =context.Comments.Where(c=>c.CourseId==id).ToList();

            CourseDetailsViewModel courseDetailsVM = new CourseDetailsViewModel();

            courseDetailsVM.CourseDetails = new Course
            {
                Id=course.Id,
                Title = course.Title,
                Description = course.Description,
                CoursePoster = course.CoursePoster,
                CoursePrice = course.CoursePrice,
                Instructor = course.Instructor,
                PublishDate = course.PublishDate,
                StudentsCount = count,
                Comments=comments,
            };


            return View(courseDetailsVM);
        }

        [Authorize]
        public ActionResult _CreateComment() => View();


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> _CreateComment(CourseComments courseComments)
        {
            //var url = Url.RequestContext.RouteData.Values["id"];
            var writer = User.Identity.Name;
            var Comment = new CourseComments
            {
                Comment = courseComments.Comment,
                CommentWriter = writer,
                CourseId=courseComments.CourseId
            };
            
            await context.AddAsync(Comment);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        [Authorize]
        public ActionResult Create() => View();


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Course course)
        {


            var newCourse = new Course
            {
                CoursePrice = course.CoursePrice,
                Description = course.Description,
                Title = course.Title,
                Instructor = User.Identity.Name,
                PublishDate = DateTime.Now
            };

            context.Courses.Add(newCourse);
            context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Subscribe(int id)
        {
            var course = context.Courses.Find(id);

            return View(course);
        }



        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Subscribe(Course course)
        {
            var user = await userManager.FindByNameAsync(User.Identity.Name);

            var instructor = await userManager.FindByNameAsync(course.Instructor);

            var sub = new CourseSubscriptions
            {
                CourseId = course.Id,
                InstructorId = instructor.Id,
                StudentUser = user.UserName,
                SubDate = DateTime.Now,
            };
            
            context.Subscriptions.Add(sub);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CoursesController/Delete/5
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
