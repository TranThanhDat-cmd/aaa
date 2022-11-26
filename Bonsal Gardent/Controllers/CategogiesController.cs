using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
 
using Bonsal_Gardent.Models;

namespace Bonsal_Gardent.Controllers
{
    public class CategogiesController : Controller
    {
        private readonly Gardent_BonsalContext _context;

        public CategogiesController(Gardent_BonsalContext context)
        {
            _context = context;
        }

        // GET: Categogies
        public async Task<IActionResult> Index()
        {
            return View(await _context.Categogies.ToListAsync());
        }

        // GET: Categogies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Categogies == null)
            {
                return NotFound();
            }

            var categogy = await _context.Categogies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categogy == null)
            {
                return NotFound();
            }

            return View(categogy);
        }

        // GET: Categogies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categogies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Categogy categogy)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categogy);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categogy);
        }

        // GET: Categogies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Categogies == null)
            {
                return NotFound();
            }

            var categogy = await _context.Categogies.FindAsync(id);
            if (categogy == null)
            {
                return NotFound();
            }
            return View(categogy);
        }

        // POST: Categogies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Categogy categogy)
        {
            if (id != categogy.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categogy);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategogyExists(categogy.Id))
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
            return View(categogy);
        }

        // GET: Categogies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Categogies == null)
            {
                return NotFound();
            }

            var categogy = await _context.Categogies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categogy == null)
            {
                return NotFound();
            }

            return View(categogy);
        }

        // POST: Categogies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Categogies == null)
            {
                return Problem("Entity set 'Gardent_BonsalContext.Categogies'  is null.");
            }
            var categogy = await _context.Categogies.FindAsync(id);
            if (categogy != null)
            {
                _context.Categogies.Remove(categogy);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategogyExists(int id)
        {
            return _context.Categogies.Any(e => e.Id == id);
        }
    }
}
