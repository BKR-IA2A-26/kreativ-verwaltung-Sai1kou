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
    public class FaehigkeitController : Controller
    {
        private readonly ApexDbContext _context;

        public FaehigkeitController(ApexDbContext context)
        {
            _context = context;
        }

        // GET: Faehigkeit
        public async Task<IActionResult> Index()
        {
            var apexDbContext = _context.Faehigkeits.Include(f => f.Charakter);
            return View(await apexDbContext.ToListAsync());
        }

        // GET: Faehigkeit/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var faehigkeit = await _context.Faehigkeits
                .Include(f => f.Charakter)
                .FirstOrDefaultAsync(m => m.FaehigkeitId == id);
            if (faehigkeit == null)
            {
                return NotFound();
            }

            return View(faehigkeit);
        }

        // GET: Faehigkeit/Create
        public IActionResult Create()
        {
            ViewData["CharakterId"] = new SelectList(_context.Charakters, "CharakterId", "CharakterId");
            return View();
        }

        // POST: Faehigkeit/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FaehigkeitId,CharakterId,Name,Typ,CooldownSek")] Faehigkeit faehigkeit)
        {
            if (ModelState.IsValid)
            {
                _context.Add(faehigkeit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CharakterId"] = new SelectList(_context.Charakters, "CharakterId", "CharakterId", faehigkeit.CharakterId);
            return View(faehigkeit);
        }

        // GET: Faehigkeit/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var faehigkeit = await _context.Faehigkeits.FindAsync(id);
            if (faehigkeit == null)
            {
                return NotFound();
            }
            ViewData["CharakterId"] = new SelectList(_context.Charakters, "CharakterId", "CharakterId", faehigkeit.CharakterId);
            return View(faehigkeit);
        }

        // POST: Faehigkeit/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FaehigkeitId,CharakterId,Name,Typ,CooldownSek")] Faehigkeit faehigkeit)
        {
            if (id != faehigkeit.FaehigkeitId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(faehigkeit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FaehigkeitExists(faehigkeit.FaehigkeitId))
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
            ViewData["CharakterId"] = new SelectList(_context.Charakters, "CharakterId", "CharakterId", faehigkeit.CharakterId);
            return View(faehigkeit);
        }

        // GET: Faehigkeit/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var faehigkeit = await _context.Faehigkeits
                .Include(f => f.Charakter)
                .FirstOrDefaultAsync(m => m.FaehigkeitId == id);
            if (faehigkeit == null)
            {
                return NotFound();
            }

            return View(faehigkeit);
        }

        // POST: Faehigkeit/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var faehigkeit = await _context.Faehigkeits.FindAsync(id);
            if (faehigkeit != null)
            {
                _context.Faehigkeits.Remove(faehigkeit);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FaehigkeitExists(int id)
        {
            return _context.Faehigkeits.Any(e => e.FaehigkeitId == id);
        }
    }
}
