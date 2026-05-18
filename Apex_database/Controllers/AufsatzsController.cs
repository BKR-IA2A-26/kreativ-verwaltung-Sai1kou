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
    public class AufsatzsController : Controller
    {
        private readonly ApexDbContext _context;

        public AufsatzsController(ApexDbContext context)
        {
            _context = context;
        }

        // GET: Aufsatzs
        public async Task<IActionResult> Index()
        {
            return View(await _context.Aufsatzs.ToListAsync());
        }

        // GET: Aufsatzs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aufsatz = await _context.Aufsatzs
                .FirstOrDefaultAsync(m => m.AufsatzId == id);
            if (aufsatz == null)
            {
                return NotFound();
            }

            return View(aufsatz);
        }

        // GET: Aufsatzs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Aufsatzs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AufsatzId,Name,Typ,Seltenheit")] Aufsatz aufsatz)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aufsatz);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(aufsatz);
        }

        // GET: Aufsatzs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aufsatz = await _context.Aufsatzs.FindAsync(id);
            if (aufsatz == null)
            {
                return NotFound();
            }
            return View(aufsatz);
        }

        // POST: Aufsatzs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AufsatzId,Name,Typ,Seltenheit")] Aufsatz aufsatz)
        {
            if (id != aufsatz.AufsatzId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aufsatz);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AufsatzExists(aufsatz.AufsatzId))
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
            return View(aufsatz);
        }

        // GET: Aufsatzs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aufsatz = await _context.Aufsatzs
                .FirstOrDefaultAsync(m => m.AufsatzId == id);
            if (aufsatz == null)
            {
                return NotFound();
            }

            return View(aufsatz);
        }

        // POST: Aufsatzs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var aufsatz = await _context.Aufsatzs.FindAsync(id);
            if (aufsatz != null)
            {
                _context.Aufsatzs.Remove(aufsatz);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AufsatzExists(int id)
        {
            return _context.Aufsatzs.Any(e => e.AufsatzId == id);
        }
    }
}
