using Bonsal_Gardent.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bonsal_Gardent.Controllers
{
	public class CartController : Controller
	{
		private readonly Gardent_BonsalContext _context;

		public CartController(Gardent_BonsalContext context)
		{
			_context = context;
			
		}
		public IActionResult Add(int amount, int productId)
		{
			if (HttpContext.Session.GetString("idUser") == null)
			{
				RedirectToAction("Login", "Homr");
			}

			var accId = int.Parse(HttpContext.Session.GetString("idUser").ToString());
			_context.Carts.Add(new Cart()
			{
				Amount = amount,
				ProductId = productId,
				AccCustomerId = accId
			});


			return View(_context.Carts.Where(x=>x.AccCustomerId == accId).Include(x=>x.Product));
		}
	}
}
