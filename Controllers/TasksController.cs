using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SoftlineTest.Models;
using SoftlineTest.Models.Repository;

namespace SoftlineTest.Controllers
{
    public class TasksController : Controller
    {
        private readonly AppDbContext _context;

        public TasksController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ModelTasks
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Tasks.Include(m => m.Status);
            return View(await appDbContext.ToListAsync());
        }

        // GET: ModelTasks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modelTasks = await _context.Tasks
                .Include(m => m.Status)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (modelTasks == null)
            {
                return NotFound();
            }

            return View(modelTasks);
        }

        // GET: ModelTasks/Create
        public IActionResult Create()
        {
            ViewData["StatusID"] = new SelectList(_context.Statuses, "ID", "ID");
            return View();
        }

        // POST: ModelTasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Description,StatusID")] ModelTasks modelTasks)
        {
            if (ModelState.IsValid)
            {
                _context.Add(modelTasks);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StatusID"] = new SelectList(_context.Statuses, "ID", "ID", modelTasks.StatusID);
            return View(modelTasks);
        }

        // GET: ModelTasks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modelTasks = await _context.Tasks.FindAsync(id);
            if (modelTasks == null)
            {
                return NotFound();
            }
            ViewData["StatusID"] = new SelectList(_context.Statuses, "ID", "ID", modelTasks.StatusID);
            return View(modelTasks);
        }

        // POST: ModelTasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Description,StatusID")] ModelTasks modelTasks)
        {
            if (id != modelTasks.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(modelTasks);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ModelTasksExists(modelTasks.ID))
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
            ViewData["StatusID"] = new SelectList(_context.Statuses, "ID", "ID", modelTasks.StatusID);
            return View(modelTasks);
        }

        // GET: ModelTasks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modelTasks = await _context.Tasks
                .Include(m => m.Status)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (modelTasks == null)
            {
                return NotFound();
            }

            return View(modelTasks);
        }

        // POST: ModelTasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var modelTasks = await _context.Tasks.FindAsync(id);
            _context.Tasks.Remove(modelTasks);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ModelTasksExists(int id)
        {
            return _context.Tasks.Any(e => e.ID == id);
        }
    }
}
