using Bonsal_Gardent.Model;
using Bonsal_Gardent.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Bonsal_Gardent.Controllers
{
    public class ManagersController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Gardent_BonsalContext _context;
        public ManagersController(ILogger<HomeController> logger, Gardent_BonsalContext context)
        {
            _logger = logger;
            _context = context;
        }
        //Manager
        public IActionResult Manager_View()
        {
            if (HttpContext.Session == null || HttpContext.Session.GetString("idUser") == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }


        public IActionResult Manager_Account()
        {
            if (HttpContext.Session == null || HttpContext.Session.GetString("idUser") == null)
            {
                return RedirectToAction("Login", "Home");
            }
            var admin = _context.AccManagers.Where(x => x.Type == 1).ToList();
            var staff = _context.AccManagers.Where(x => x.Type == 2).ToList();
            var customer = _context.AccCustomers.ToList();

            return View(new
            {
                admin = admin,
                staff = staff,
                customer = customer,
            });
        }

        public async Task<IActionResult> Manager_Product()
        {
            if (HttpContext.Session == null || HttpContext.Session.GetString("idUser") == null)
            {
                return RedirectToAction("Login", "Home");
            }
            var gardent_BonsalContext = _context.Products.Include(p => p.Category).Include(p => p.Type);
            return View(await gardent_BonsalContext.ToListAsync());
        }

        public async Task<IActionResult> Manager_Page()
        {
            if (HttpContext.Session == null || HttpContext.Session.GetString("idUser") == null)
            {
                return RedirectToAction("Login", "Home");
            }
            var totalUser = await _context.AccCustomers.CountAsync(); 
            var totalSell = await _context.CustomerOrders.CountAsync();
            var totalProduct = await _context.Products.CountAsync();
            var totalComment = await _context.CommentProducts.CountAsync();
            return View(new { totalUser, totalSell, totalProduct, totalComment });

            //Income
            //List_Comments
            //Num_Product
        }
        //Income
        public async Task<IActionResult> List_Bill()
        {
            if (HttpContext.Session == null || HttpContext.Session.GetString("idUser") == null)
            {
                return RedirectToAction("Login", "Home");
            }
            var orders = await _context.CustomerOrders.Include(x => x.OrderDetails).ThenInclude(x => x.Product)
                .Include(x => x.AccCustomer).OrderByDescending(x => x.CreateAtTime).ToListAsync();
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
        public async Task<IActionResult> List_Comment()
        {
            if (HttpContext.Session == null || HttpContext.Session.GetString("idUser") == null)
            {
                return RedirectToAction("Login", "Home");
            }
            var comments = await _context.CommentProducts.Include(x => x.AccCustomer).
                Include(x => x.Product).Where(x => x.AccManagerId != null).ToListAsync();
            return View(comments);
        }
        public IActionResult Product()
        {
            if (HttpContext.Session == null || HttpContext.Session.GetString("idUser") == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }
    }
}
