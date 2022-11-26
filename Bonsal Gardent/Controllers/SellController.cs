using Bonsal_Gardent.Model;
using Bonsal_Gardent.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
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
            var carts = _context.Carts.Where(x => x.AccCustomerId == accId).Include(x => x.Product).ToList();

            return View(new
            {
                carts = carts,
                sum = carts.Sum(x => x.Amount * double.Parse(x.Product.Price))
            });
        }

        public IActionResult HandleOrder()
        {
            if (HttpContext.Session == null || HttpContext.Session.GetString("idUser") == null)
            {
                return RedirectToAction("Login", "Home");
            }
            var accId = int.Parse(HttpContext.Session!.GetString("idUser").ToString());
            var acc = _context.AccCustomers.FirstOrDefault(x => x.Id == accId);
            var carts = _context.Carts.Where(x => x.AccCustomerId == accId).Include(x => x.Product).ToList();

            _context.CustomerOrders.Add(new CustomerOrder()
            {
                AccCustomerId = accId,
                CreateAtTime = DateTime.Now,
                OrderDetails = carts.Select(x => new OrderDetail()
                {
                    Amount = x.Amount,
                    ProductId = x.ProductId,
                }).ToList()
            });
            _context.SaveChanges();

            return RedirectToAction("Bill_Detail");
        }

        public IActionResult Checkout()
        {
            if (HttpContext.Session == null || HttpContext.Session.GetString("idUser") == null)
            {
                return RedirectToAction("Login", "Home");
            }

            var accId = int.Parse(HttpContext.Session!.GetString("idUser").ToString());
            var acc = _context.AccCustomers.FirstOrDefault(x => x.Id == accId);
            var carts = _context.Carts.Where(x => x.AccCustomerId == accId).Include(x => x.Product).ToList();
            return View(new
            {
                acc = acc,
                carts = carts,
                totalAmount = carts.Count(),
                totalMoney = carts.Sum(x => x.Amount * double.Parse(x.Product.Price))
            });
        }

        public IActionResult Bill_Detail()
        {
            if (HttpContext.Session == null || HttpContext.Session.GetString("idUser") == null)
            {
                return RedirectToAction("Login", "Home");
            }
            if (HttpContext.Session == null || HttpContext.Session.GetString("idUser") == null)
            {
                return RedirectToAction("Login", "Home");
            }

            var accId = int.Parse(HttpContext.Session!.GetString("idUser").ToString());
            var orders = _context.CustomerOrders.Where(x => x.AccCustomerId == accId).Include(x => x.OrderDetails).ThenInclude(x => x.Product)
                .Include(x => x.AccCustomer).OrderByDescending(x => x.CreateAtTime).ToList();
            var result = new List<OrderViewModel>();
            foreach (var order in orders)
            {
                result.Add(new OrderViewModel
                {
                    Id = order.Id,
                    AccCustomerId = order.AccCustomerId,
                    CustomerName = order.AccCustomer.Name,
                    CreateAtTime = order.CreateAtTime,
                    TotalAmount = (long)order.OrderDetails.Sum(x => x.Amount),
                    TotalMoney = (long)order.OrderDetails.Sum(x => x.Amount * Convert.ToInt64(x.Product.Price)),
                    Items = order.OrderDetails
                });
            }
            return View(result);
        }

        //Customer
        public IActionResult User_Bill()
        {
            if (HttpContext.Session == null || HttpContext.Session.GetString("idUser") == null)
            {
                return RedirectToAction("Login", "Home");
            }

            var accId = int.Parse(HttpContext.Session!.GetString("idUser").ToString());
            var carts = _context.Carts.Where(x => x.AccCustomerId == accId).Include(x => x.Product).ToList();

            return View(new
            {
                carts = carts,
                sum = carts.Sum(x => x.Amount * double.Parse(x.Product.Price))
            });

        }
    }
}
