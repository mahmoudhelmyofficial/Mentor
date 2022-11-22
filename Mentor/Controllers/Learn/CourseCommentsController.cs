using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mentor.Data;
using Mentor.Models;
using Microsoft.AspNetCore.Authorization;

namespace Mentor.Controllers
{
    public class CourseCommentsController : Controller
    {
        private readonly ApplicationDbContext context;

        public CourseCommentsController(ApplicationDbContext context)
        {
            context = context;
        }

        // GET: CourseComments
        public IActionResult Index() => View(context.Comments.ToList());
        

        // GET: CourseComments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || context.Comments == null)
            {
                return NotFound();
            }

            var courseComments = await context.Comments
                .Include(c => c.Course)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (courseComments == null)
            {
                return NotFound();
            }

            return View(courseComments);
        }

        // GET: CourseComments/Create
        [Authorize]
        public IActionResult Create(int id)
        {
            var courseId = new CourseComments { CourseId = id };

            return View(courseId);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseComments courseComments)
        {

            var writer =User.Identity.Name;
            var Comment = new CourseComments
            {
                Comment = courseComments.Comment,
                CommentWriter =writer,
                CourseId = courseComments.CourseId
            };

            await context.AddAsync(Comment);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: CourseComments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || context.Comments == null)
            {
                return NotFound();
            }

            var courseComments = await context.Comments.FindAsync(id);
            if (courseComments == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(context.Courses, "Id", "Id", courseComments.CourseId);
            return View(courseComments);
        }

        // POST: CourseComments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Comment,CourseId,CommentWriter")] CourseComments courseComments)
        {
            if (id != courseComments.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(courseComments);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseCommentsExists(courseComments.Id))
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
            ViewData["CourseId"] = new SelectList(context.Courses, "Id", "Id", courseComments.CourseId);
            return View(courseComments);
        }

        // GET: CourseComments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || context.Comments == null)
            {
                return NotFound();
            }

            var courseComments = await context.Comments
                .Include(c => c.Course)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (courseComments == null)
            {
                return NotFound();
            }

            return View(courseComments);
        }

        // POST: CourseComments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (context.Comments == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Comments'  is null.");
            }
            var courseComments = await context.Comments.FindAsync(id);
            if (courseComments != null)
            {
                context.Comments.Remove(courseComments);
            }
            
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseCommentsExists(int id)
        {
          return (context.Comments?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
