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
    public class CharakterController : Controller
    {
        private readonly ApexDbContext _context;

        public CharakterController(ApexDbContext context)
        {
            _context = context;
        }

        // GET: Charakter
        public async Task<IActionResult> Index()
        {
            return View(await _context.Charakters.ToListAsync());
        }

        // GET: Charakter/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var charakter = await _context.Charakters
                .FirstOrDefaultAsync(m => m.CharakterId == id);
            if (charakter == null)
            {
                return NotFound();
            }

            return View(charakter);
        }

        // GET: Charakter/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Charakter/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CharakterId,Name,Klasse")] Charakter charakter)
        {
            if (ModelState.IsValid)
            {
                _context.Add(charakter);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(charakter);
        }

        // GET: Charakter/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var charakter = await _context.Charakters.FindAsync(id);
            if (charakter == null)
            {
                return NotFound();
            }
            return View(charakter);
        }

        // POST: Charakter/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CharakterId,Name,Klasse")] Charakter charakter)
        {
            if (id != charakter.CharakterId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(charakter);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CharakterExists(charakter.CharakterId))
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
            return View(charakter);
        }

        // GET: Charakter/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var charakter = await _context.Charakters
                .FirstOrDefaultAsync(m => m.CharakterId == id);
            if (charakter == null)
            {
                return NotFound();
            }

            return View(charakter);
        }

        // POST: Charakter/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var charakter = await _context.Charakters.FindAsync(id);
            if (charakter != null)
            {
                _context.Charakters.Remove(charakter);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CharakterExists(int id)
        {
            return _context.Charakters.Any(e => e.CharakterId == id);
        }
    }
}
