using DevBlog5.Data;
using DevBlog5.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevBlog5.Models;

namespace DevBlog5.Services
{
    public class BlogSearchService
    {
        private readonly ApplicationDbContext _context;

        public BlogSearchService(ApplicationDbContext context)
        {
            _context = context;
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
                                        c.BlogUsers.Email.ToLower().Contains(searchTerm)));
            }

        return posts = posts.OrderByDescending(p => p.Created);
        }
    }
}
