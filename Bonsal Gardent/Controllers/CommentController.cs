using Bonsal_Gardent.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bonsal_Gardent.Controllers
{
    public class CommentController : Controller
    {
        private readonly Gardent_BonsalContext _context;

        public CommentController(Gardent_BonsalContext context)
        {
            _context = context;
        }
        public IActionResult Add(string content, int id)
        {
            if (HttpContext.Session == null || HttpContext.Session.GetString("idUser") == null)
            {
                return RedirectToAction("Login", "Home");
            }

            var accId = int.Parse(HttpContext.Session!.GetString("idUser").ToString());
            _context.CommentProducts.Add(new CommentProduct()
            {
                AccCustomerId = accId,
                Content = content,
                IsApprove = false,
                ProductId = id,
                CreatedAt = DateTime.Now,
            });
            _context.SaveChanges();
            return RedirectToAction("Details", "Home", new
            {
                id = id,
            });
        }
    }
}
