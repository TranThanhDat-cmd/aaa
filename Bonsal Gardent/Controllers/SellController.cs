using Bonsal_Gardent.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bonsal_Gardent.Controllers
{
    public class SellController : Controller
    {
		//Order

		private readonly Gardent_BonsalContext _context;

		public SellController(Gardent_BonsalContext context)
		{
			_context = context;
		}
		public IActionResult Order(int amount, int productId)
        {
			if (HttpContext.Session == null || HttpContext.Session.GetString("idUser") == null)
			{
				return RedirectToAction("Login", "Home");
			}

			var accId = int.Parse(HttpContext.Session!.GetString("idUser").ToString());
			var cart = _context.Carts.FirstOrDefault(x => x.AccCustomerId == accId && x.ProductId == productId);

            if (cart != null)
			{
				cart.Amount += amount;

			}
			else
			{
                _context.Carts.Add(new Cart()
                {
                    Amount = amount,
                    ProductId = productId,
                    AccCustomerId = accId
                });
            }
			

			_context.SaveChanges();
            var carts = _context.Carts.Where(x => x.AccCustomerId == accId).Include(x=>x.Product).ToList();

            return View(carts);
		}

        public IActionResult Checkout()
        {
            return View();
        }

        public IActionResult Bill_Detail()
        {
            return View();
        }

        //Customer
        public IActionResult User_Bill()
        {
            return View();

        }
    }
}
