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
    public class AccCustomersController : Controller
    {
        private readonly Gardent_BonsalContext _context;

        public AccCustomersController(Gardent_BonsalContext context)
        {
            _context = context;
        }

        // GET: AccCustomers
        public async Task<IActionResult> Index()
        {
            return View(await _context.AccCustomers.ToListAsync());
        }

        // GET: AccCustomers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AccCustomers == null)
            {
                return NotFound();
            }

            var accCustomer = await _context.AccCustomers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (accCustomer == null)
            {
                return NotFound();
            }

            return View(accCustomer);
        }

        // GET: AccCustomers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AccCustomers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Password,Email,Phone,Address")] AccCustomer accCustomer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(accCustomer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(accCustomer);
        }

        // GET: AccCustomers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AccCustomers == null)
            {
                return NotFound();
            }

            var accCustomer = await _context.AccCustomers.FindAsync(id);
            if (accCustomer == null)
            {
                return NotFound();
            }
            return View(accCustomer);
        }

        // POST: AccCustomers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Password,Email,Phone,Address")] AccCustomer accCustomer)
        {
            if (id != accCustomer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(accCustomer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccCustomerExists(accCustomer.Id))
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
            return View(accCustomer);
        }

        // GET: AccCustomers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AccCustomers == null)
            {
                return NotFound();
            }

            var accCustomer = await _context.AccCustomers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (accCustomer == null)
            {
                return NotFound();
            }

            return View(accCustomer);
        }

        // POST: AccCustomers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AccCustomers == null)
            {
                return Problem("Entity set 'Gardent_BonsalContext.AccCustomers'  is null.");
            }
            var accCustomer = await _context.AccCustomers.FindAsync(id);
            if (accCustomer != null)
            {
                _context.AccCustomers.Remove(accCustomer);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccCustomerExists(int id)
        {
            return _context.AccCustomers.Any(e => e.Id == id);
        }


        //Prolife

        public async Task<IActionResult> Edit_for_Customer(int? id)
        {
            if (id == null || _context.AccCustomers == null)
            {
                return NotFound();
            }

            var accCustomer = await _context.AccCustomers.FindAsync(id);
            if (accCustomer == null)
            {
                return NotFound();
            }
            return View(accCustomer);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_for_Customer(int id, [Bind("Id,Name,Password,Email,Phone,Address")] AccCustomer accCustomer)
        {
            if (id != accCustomer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(accCustomer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccCustomerExists(accCustomer.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Profile", "Home");
            }
            return RedirectToAction("Profile", "Home");
        }

        public async Task<IActionResult> Delete_for_Customer(int? id)
        {
            var accCustomer = await _context.AccCustomers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (accCustomer != null)
            {
                _context.Remove(accCustomer);
                _context.SaveChanges();
            }

            return RedirectToAction("Login", "Home");
        }

        //// POST: AccCustomers/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Delete_for_Customer(int id)
        //{
        //    if (_context.AccCustomers == null)
        //    {
        //        return Problem("Entity set 'Gardent_BonsalContext.AccCustomers'  is null.");
        //    }
        //    var accCustomer = await _context.AccCustomers.FindAsync(id);
        //    if (accCustomer != null)
        //    {
        //        _context.AccCustomers.Remove(accCustomer);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool AccCustomerExists(int id)
        //{
        //    return _context.AccCustomers.Any(e => e.Id == id);
        //}
        //*/
    }
}
