using Bonsal_Gardent.Model;
using Bonsal_Gardent.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Bonsal_Gardent.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Gardent_BonsalContext _context;
        public HomeController(ILogger<HomeController> logger, Gardent_BonsalContext context)
        {
            _logger = logger;
            _context = context;
        }

        //Home
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        //Login
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Logout()
        {

            HttpContext.Session.Remove("idUser");
            HttpContext.Session.Remove("name");
            HttpContext.Session.Remove("role");
            return RedirectToAction("Login", "Home");
        }

        public IActionResult Sign_up(string? errorMessage = null)
        {
            ViewData["mes"] = errorMessage;
            return View();
        }

        public IActionResult Register(IFormCollection request)
        {
            
            var regex = new Regex(@"^[A-Za-z][A-Za-z0-9!@#$%^&*]*$");
            if (!regex.IsMatch(request["password"].ToString()) 
                || request["password"].ToString().Length < 6
                || request["password"].ToString().Length > 12
                || request["password"].ToString() != request["re-password"].ToString())
            {
                return RedirectToAction("Sign_up", new { errorMessage = "Password Invalid" });
            }

            var cus = new AccCustomer()
            {
                Name = request["name"],
                Address = request["address"],
                Email = request["email"],
                Password = request["password"],
                Phone = request["phone"],
            };
            _context.AccCustomers.Add(cus);
            _context.SaveChanges();
            return RedirectToAction("Login");
        }

        public IActionResult SignIn(IFormCollection request)
        {
            string email = request["email"].ToString().Trim();
            string pass = request["password"].ToString().Trim();
            var Managers = _context.AccManagers.Where(x => x.Email == email && x.Password == pass).FirstOrDefault();
            var customer = _context.AccCustomers.Where(x => x.Email == email && x.Password == pass).FirstOrDefault();
            if (Managers != null)
            {
                HttpContext.Session.SetString("idUser", Managers.Id.ToString());
                HttpContext.Session.SetString("name", Managers.Name.ToString());
                HttpContext.Session.SetString("role", Managers.Type == 1 ? "Admin" : "Staff"); 
                return RedirectToAction("Index", "Home");
            }
            else if (customer != null)
            {
                HttpContext.Session.SetString("idUser", customer.Id.ToString());
                HttpContext.Session.SetString("name", customer.Name.ToString());
                HttpContext.Session.SetString("role", "Customer");
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Login");

        }

        public IActionResult Profile()
        {
            var role = HttpContext.Session.GetString("role");
            var userId = HttpContext.Session.GetString("idUser");

            if(userId is null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (role != null && role.ToString()== "Customer")
            {
                var customer = _context.AccCustomers.Where(x => x.Id == int.Parse(userId)).FirstOrDefault();
                return View(customer);
            }
                var managers = _context.AccManagers.Where(x => x.Id == int.Parse(userId)).FirstOrDefault();
            return View(managers);
        }
        

        //Product
        public IActionResult Product()
        {
            return View();
        }
        public IActionResult Tool(int? id = null)
        {
            var product = _context.Products.Where(x=>(id == null || x.CategoryId == id) && x.TypeId == 1).Include(x=>x.Pictures).ToList();
            var categories = _context.Categogies;

            return View(new
            {
                product = product,
                categories = categories
            });
        }
        public IActionResult Tree()
        {
            var product = _context.Products.Where(x => x.TypeId == 2).Include(x => x.Pictures).ToList();
            var categories = _context.Categogies;

            return View(new
            {
                product = product,
                categories = categories
            });
        }
        public IActionResult Details(int id)
        {
			var product = _context.Products.Where(x => x.Id == id).Include(x => x.Pictures).FirstOrDefault();
			return View(product);
		}


        //Error
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}