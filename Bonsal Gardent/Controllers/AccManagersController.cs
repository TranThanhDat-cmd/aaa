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
    public class AccManagersController : Controller
    {
        private readonly Gardent_BonsalContext _context;

        public AccManagersController(Gardent_BonsalContext context)
        {
            _context = context;
        }

        // GET: AccManagers
        public async Task<IActionResult> Index()
        {
            return View(await _context.AccManagers.ToListAsync());
        }

        // GET: AccManagers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AccManagers == null)
            {
                return NotFound();
            }

            var accManager = await _context.AccManagers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (accManager == null)
            {
                return NotFound();
            }

            return View(accManager);
        }

        // GET: AccManagers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AccManagers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Password,Email,Phone,Address,Type")] AccManager accManager)
        {
            if (ModelState.IsValid)
            {
                _context.Add(accManager);
                await _context.SaveChangesAsync();
                return RedirectToAction("Manager_Account", "Home"/*nameof(Index)*/);
            }
            return View(accManager);
        }

        // GET: AccManagers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AccManagers == null)
            {
                return NotFound();
            }

            var accManager = await _context.AccManagers.FindAsync(id);
            if (accManager == null)
            {
                return NotFound();
            }
            return View(accManager);
        }

        // POST: AccManagers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Password,Email,Phone,Address,Type")] AccManager accManager)
        {
            if (id != accManager.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(accManager);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccManagerExists(accManager.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Manager_Account", "Home"/*nameof(Index)*/);
            }
            return View(accManager);
        }

        // GET: AccManagers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AccManagers == null)
            {
                return NotFound();
            }

            var accManager = await _context.AccManagers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (accManager == null)
            {
                return NotFound();
            }

            return View(accManager);
        }

        // POST: AccManagers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AccManagers == null)
            {
                return Problem("Entity set 'Gardent_BonsalContext.AccManagers'  is null.");
            }
            var accManager = await _context.AccManagers.FindAsync(id);
            if (accManager != null)
            {
                _context.AccManagers.Remove(accManager);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Manager_Account", "Home"/*nameof(Index)*/);
        }

        private bool AccManagerExists(int id)
        {
            return _context.AccManagers.Any(e => e.Id == id);
        }
    }
}
