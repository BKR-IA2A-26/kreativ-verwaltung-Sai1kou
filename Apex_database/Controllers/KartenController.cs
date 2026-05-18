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
    public class KartenController : Controller
    {
        private readonly ApexDbContext _context;

        public KartenController(ApexDbContext context)
        {
            _context = context;
        }

        // GET: Karten
        public async Task<IActionResult> Index()
        {
            return View(await _context.Kartes.ToListAsync());
        }

        // GET: Karten/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var karte = await _context.Kartes
                .FirstOrDefaultAsync(m => m.KarteId == id);
            if (karte == null)
            {
                return NotFound();
            }

            return View(karte);
        }

        // GET: Karten/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Karten/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("KarteId,Name")] Karte karte)
        {
            if (ModelState.IsValid)
            {
                _context.Add(karte);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(karte);
        }

        // GET: Karten/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var karte = await _context.Kartes.FindAsync(id);
            if (karte == null)
            {
                return NotFound();
            }
            return View(karte);
        }

        // POST: Karten/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("KarteId,Name")] Karte karte)
        {
            if (id != karte.KarteId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(karte);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KarteExists(karte.KarteId))
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
            return View(karte);
        }

        // GET: Karten/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var karte = await _context.Kartes
                .FirstOrDefaultAsync(m => m.KarteId == id);
            if (karte == null)
            {
                return NotFound();
            }

            return View(karte);
        }

        // POST: Karten/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var karte = await _context.Kartes.FindAsync(id);
            if (karte != null)
            {
                _context.Kartes.Remove(karte);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KarteExists(int id)
        {
            return _context.Kartes.Any(e => e.KarteId == id);
        }
    }
}
