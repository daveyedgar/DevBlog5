using DevBlog5.Data;
using DevBlog5.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace DevBlog5.Components
{
    public class RecentCommentsViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<BlogUser> _userManager;

        public RecentCommentsViewComponent(ApplicationDbContext context, UserManager<BlogUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var recentComments = _context.Comments
                .Where(c=>c.Moderated == null)
                .Include(p=>p.Posts)
                .Include(u=>u.BlogUsers)
                .OrderByDescending(p => p.Updated != null)
                .ThenByDescending(p => p.Updated)
                .ThenByDescending(p => p.Created)
                .Take(7)
                .ToList();


            return View(recentComments);
        }
    }
}
