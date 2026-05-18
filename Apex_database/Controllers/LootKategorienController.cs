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
    public class LootKategorienController : Controller
    {
        private readonly ApexDbContext _context;

        public LootKategorienController(ApexDbContext context)
        {
            _context = context;
        }

        // GET: LootKategorien
        public async Task<IActionResult> Index()
        {
            return View(await _context.LootKategories.ToListAsync());
        }

        // GET: LootKategorien/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lootKategorie = await _context.LootKategories
                .FirstOrDefaultAsync(m => m.KategorieId == id);
            if (lootKategorie == null)
            {
                return NotFound();
            }

            return View(lootKategorie);
        }

        // GET: LootKategorien/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LootKategorien/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("KategorieId,Name")] LootKategorie lootKategorie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lootKategorie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lootKategorie);
        }

        // GET: LootKategorien/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lootKategorie = await _context.LootKategories.FindAsync(id);
            if (lootKategorie == null)
            {
                return NotFound();
            }
            return View(lootKategorie);
        }

        // POST: LootKategorien/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("KategorieId,Name")] LootKategorie lootKategorie)
        {
            if (id != lootKategorie.KategorieId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lootKategorie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LootKategorieExists(lootKategorie.KategorieId))
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
            return View(lootKategorie);
        }

        // GET: LootKategorien/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lootKategorie = await _context.LootKategories
                .FirstOrDefaultAsync(m => m.KategorieId == id);
            if (lootKategorie == null)
            {
                return NotFound();
            }

            return View(lootKategorie);
        }

        // POST: LootKategorien/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lootKategorie = await _context.LootKategories.FindAsync(id);
            if (lootKategorie != null)
            {
                _context.LootKategories.Remove(lootKategorie);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LootKategorieExists(int id)
        {
            return _context.LootKategories.Any(e => e.KategorieId == id);
        }
    }
}
