using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mentor.Data;
using Mentor.Models.Job;

namespace Mentor.Controllers
{
    public class JobCategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JobCategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
              return _context.JobCategories != null ? 
                          View(await _context.JobCategories.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.JobCategories'  is null.");
        }

        public async Task<IActionResult> Details(int? id)
        {

            var jobCategory = await _context.JobCategories
                .FirstOrDefaultAsync(m => m.Id == id);

            return View(jobCategory);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(JobCategory jobCategory)
        {
            _context.Add(jobCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Edit(int? id)
        {
            var jobCategory = await _context.JobCategories.FindAsync(id);

            return View(jobCategory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,JobCategory jobCategory)
        {
            _context.Update(jobCategory);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {

            var jobCategory = await _context.JobCategories
                .FirstOrDefaultAsync(m => m.Id == id);

            return View(jobCategory);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.JobCategories == null)
            {
                return Problem("Entity set 'ApplicationDbContext.JobCategories'  is null.");
            }
            var jobCategory = await _context.JobCategories.FindAsync(id);
            if (jobCategory != null)
            {
                _context.JobCategories.Remove(jobCategory);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JobCategoryExists(int id)
        {
          return (_context.JobCategories?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
