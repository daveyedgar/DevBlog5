using DevBlog5.Data;
using DevBlog5.Enums;
using DevBlog5.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DevBlog5.Services
{
    public class BlogSearchService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<BlogUser> _userManager;

        public BlogSearchService(ApplicationDbContext context, UserManager<BlogUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IQueryable<Post> Search(string searchTerm)
        {

            var posts = _context.Posts.Where(p => p.ReadyStatus == ReadyStatus.ProductionReady).AsQueryable();

            if (searchTerm != null)
            {
                searchTerm = searchTerm.ToLower();

                posts = posts.Where(
                    p => p.Title.ToLower().Contains(searchTerm) ||
                    p.Abstract.ToLower().Contains(searchTerm) ||
                    p.Content.ToLower().Contains(searchTerm) ||
                    p.Comments.Any(c => c.Body.ToLower().Contains(searchTerm) ||
                                        c.ModeratedBody.ToLower().Contains(searchTerm) ||
                                        c.BlogUsers.FirstName.ToLower().Contains(searchTerm) ||
                                        c.BlogUsers.LastName.ToLower().Contains(searchTerm) ||
                                        c.BlogUsers.Email.ToLower().Contains(searchTerm)))

                .Include(p => p.BlogUser);
            }

            return posts = posts
                .OrderByDescending(p => p.Updated != null)
                .ThenByDescending(p => p.Updated)
                .ThenByDescending(p => p.Created); ;
        }
    }
}
