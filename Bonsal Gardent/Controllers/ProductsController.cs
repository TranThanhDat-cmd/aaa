using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bonsal_Gardent.Models;
using Bonsal_Gardent.Model;
using Microsoft.CodeAnalysis;
using Bonsal_Gardent.Services;

namespace Bonsal_Gardent.Controllers
{
    public class ProductsController : Controller
    {
        private readonly Gardent_BonsalContext _context;

        public ProductsController(Gardent_BonsalContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var gardent_BonsalContext = _context.Products.Include(p => p.Category).Include(p => p.Type);
            return View(await gardent_BonsalContext.ToListAsync());
        }

        public async Task<IActionResult> DeletePicture(int id)
        {
            var pic = await _context.Pictures.FindAsync(id);
            var productId = pic!.ProductId;
            _context.Pictures.Remove(pic);
            _context.SaveChanges();
            return RedirectToAction("Edit","Products", new {id = productId});
        }



        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Type)
                .Include(x=>x.Pictures)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categogies, "Id", "Id");
            ViewData["TypeId"] = new SelectList(_context.Types, "Id", "Id");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateProductDto req)
        {
            FileStorageService fileStorageService = new();
            var product = new Product()
            {
                Name = req.Name,
                Info = req.Info,
                Place = req.Place,
                Address = req.Address,
                TypeId = req.TypeId,
                CategoryId = req.CategoryId,
                Price = req.Price,
                Amount = req.Amount,
                Pictures = req.Images?.Select(x => new Picture()
                {
                    Path = fileStorageService.SaveFileAsync(x)
                }).ToList()
            };

                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
 
            ViewData["CategoryId"] = new SelectList(_context.Categogies, "Id", "Id", product.CategoryId);
            ViewData["TypeId"] = new SelectList(_context.Types, "Id", "Id", product.TypeId);
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.Where(x=>x.Id == id).Include(x=>x.Pictures).FirstOrDefaultAsync();
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categogies, "Id", "Id", product.CategoryId);
            ViewData["TypeId"] = new SelectList(_context.Types, "Id", "Id", product.TypeId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Info,Place,Address,Price,Amount,TypeId,CategoryId")] Product product)
        {
            _context.Update(product);
            await _context.SaveChangesAsync();


            ViewData["CategoryId"] = new SelectList(_context.Categogies, "Id", "Id", product.CategoryId);
            ViewData["TypeId"] = new SelectList(_context.Types, "Id", "Id", product.TypeId);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Type)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'Gardent_BonsalContext.Products'  is null.");
            }
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Manager_Product", "Home");
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
