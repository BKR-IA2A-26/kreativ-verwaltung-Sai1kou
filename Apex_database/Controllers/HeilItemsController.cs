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
    public class HeilItemsController : Controller
    {
        private readonly ApexDbContext _context;

        public HeilItemsController(ApexDbContext context)
        {
            _context = context;
        }

        // GET: HeilItems
        public async Task<IActionResult> Index()
        {
            return View(await _context.HeilItems.ToListAsync());
        }

        // GET: HeilItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var heilItem = await _context.HeilItems
                .FirstOrDefaultAsync(m => m.HeilId == id);
            if (heilItem == null)
            {
                return NotFound();
            }

            return View(heilItem);
        }

        // GET: HeilItems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HeilItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HeilId,Name,HeilungHp,HeilungSchild,AnwendungsdauerSek")] HeilItem heilItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(heilItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(heilItem);
        }

        // GET: HeilItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var heilItem = await _context.HeilItems.FindAsync(id);
            if (heilItem == null)
            {
                return NotFound();
            }
            return View(heilItem);
        }

        // POST: HeilItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HeilId,Name,HeilungHp,HeilungSchild,AnwendungsdauerSek")] HeilItem heilItem)
        {
            if (id != heilItem.HeilId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(heilItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HeilItemExists(heilItem.HeilId))
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
            return View(heilItem);
        }

        // GET: HeilItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var heilItem = await _context.HeilItems
                .FirstOrDefaultAsync(m => m.HeilId == id);
            if (heilItem == null)
            {
                return NotFound();
            }

            return View(heilItem);
        }

        // POST: HeilItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var heilItem = await _context.HeilItems.FindAsync(id);
            if (heilItem != null)
            {
                _context.HeilItems.Remove(heilItem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HeilItemExists(int id)
        {
            return _context.HeilItems.Any(e => e.HeilId == id);
        }
    }
}
