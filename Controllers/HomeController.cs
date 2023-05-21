using DevBlog5.Data;
using DevBlog5.Models;
using DevBlog5.Services;
using DevBlog5.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace DevBlog5.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBlogEmailSender _emailSender;
        private readonly ApplicationDbContext _context;


        public HomeController(ILogger<HomeController> logger, IBlogEmailSender emailSender, ApplicationDbContext context)
        {
            _logger = logger;
            _emailSender = emailSender;
            _context = context;
        }

        public async Task<IActionResult> Index(int? page)
        {
            var pageNumber = page ?? 1;
            var pageSize = 2;

            var blogs = _context.Blogs
                .Include(b => b.BlogUsers)
                .OrderByDescending(b => b.Created)
                .ToPagedListAsync(pageNumber, pageSize);

            return View(await blogs);

        }

        // Get Contact view
        public IActionResult Contact(string swalMessage = null)
        {
            ViewData["SwalMessage"] = swalMessage;
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Contact(ContactMe model)
        {
            try
            {
                model.Message = $"{model.Message} <hr /> Phone: {model.Phone}";
                await _emailSender.SendContactEmailAsync(model.Name, model.Email, model.Phone, model.Subject, model.Message);
                //return RedirectToAction("Index");
                return RedirectToAction("Contact", "Home", new { swalMessage = "Success: Email Sent." });
            }
            catch (System.Exception)
            {
                return RedirectToAction("Contact", "Home", new { swalMessage = "Error: Email Send Failed." });
                throw;
            }
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
