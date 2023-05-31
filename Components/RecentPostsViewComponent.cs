using DevBlog5.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace DevBlog5.Components
{
    public class RecentPostsViewComponent : ViewComponent
    {

        private readonly ApplicationDbContext _context;

        public RecentPostsViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var recentPosts =  _context.Posts
                .OrderByDescending(p => p.Updated != null)
                .ThenByDescending(p => p.Updated)
                .ThenByDescending(p => p.Created)
                .Take(7)
                .ToList();

            return View(recentPosts);
        }
    }
}
