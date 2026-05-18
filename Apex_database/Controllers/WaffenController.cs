using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Apex_database.Data;
using Apex_database.Models;

namespace Apex_database.Controllers
{
    public class WaffenController : Controller
    {
        private readonly ApexDbContext _context;

        public WaffenController(ApexDbContext context)
        {
            _context = context;
        }

        // GET: Waffen
        public async Task<IActionResult> Index()
        {
            var apexDbContext = _context.Waffes.Include(w => w.Munition);
            return View(await apexDbContext.ToListAsync());
        }

        // GET: Waffen/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var waffe = await _context.Waffes
                .Include(w => w.Munition)
                .FirstOrDefaultAsync(m => m.WaffeId == id);
            if (waffe == null)
            {
                return NotFound();
            }

            return View(waffe);
        }

        // GET: Waffen/Create
        public IActionResult Create()
        {
            ViewData["MunitionId"] = new SelectList(_context.Munitionstyps, "MunitionId", "MunitionId");
            return View();
        }

        // POST: Waffen/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WaffeId,MunitionId,Name,WaffenTyp,BasisSchaden")] Waffe waffe)
        {
            if (ModelState.IsValid)
            {
                _context.Add(waffe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MunitionId"] = new SelectList(_context.Munitionstyps, "MunitionId", "MunitionId", waffe.MunitionId);
            return View(waffe);
        }

        // GET: Waffen/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var waffe = await _context.Waffes.FindAsync(id);
            if (waffe == null)
            {
                return NotFound();
            }
            ViewData["MunitionId"] = new SelectList(_context.Munitionstyps, "MunitionId", "MunitionId", waffe.MunitionId);
            return View(waffe);
        }

        // POST: Waffen/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WaffeId,MunitionId,Name,WaffenTyp,BasisSchaden")] Waffe waffe)
        {
            if (id != waffe.WaffeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(waffe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WaffeExists(waffe.WaffeId))
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
            ViewData["MunitionId"] = new SelectList(_context.Munitionstyps, "MunitionId", "MunitionId", waffe.MunitionId);
            return View(waffe);
        }

        // GET: Waffen/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var waffe = await _context.Waffes
                .Include(w => w.Munition)
                .FirstOrDefaultAsync(m => m.WaffeId == id);
            if (waffe == null)
            {
                return NotFound();
            }

            return View(waffe);
        }

        // POST: Waffen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var waffe = await _context.Waffes.FindAsync(id);
            if (waffe != null)
            {
                _context.Waffes.Remove(waffe);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WaffeExists(int id)
        {
            return _context.Waffes.Any(e => e.WaffeId == id);
        }
    }
}
