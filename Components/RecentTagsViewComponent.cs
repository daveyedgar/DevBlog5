using DevBlog5.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace DevBlog5.Components
{
    public class RecentTagsViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public RecentTagsViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            var recentTags = (_context.Tags
                .Include(p => p.Posts)
                .OrderByDescending(p => p.Posts.Created)
                .Take(7))
                .GroupBy(x => x.Text)
                .Select(x => x.First())
                    .ToList();

            return View(recentTags);
        }
    }
}
