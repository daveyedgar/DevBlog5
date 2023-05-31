using DevBlog5.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace DevBlog5.Components
{
    public class RecentCategoriesViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public RecentCategoriesViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            var recentBlogs = _context.Blogs
                .OrderByDescending(p => p.Updated != null)
                .ThenByDescending(p => p.Updated)
                .ThenByDescending(p => p.Created)
                .Take(7)
                .ToList();

            return View(recentBlogs);
        }
    }
}
