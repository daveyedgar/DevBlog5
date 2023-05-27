using Microsoft.AspNetCore.Mvc;
using DevBlog5.ViewModels;
using System.Collections.Generic;
using DevBlog5.Models;
using MimeKit.Cryptography;
using DevBlog5.Data;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DevBlog5.Controllers
{
    public class MasterController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<BlogUser> _userManager;

        public MasterController(ApplicationDbContext context, UserManager<BlogUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        //private List<BlogUser> users = new List<BlogUser>();
        //private List<Blog> blogs = new List<Blog>();
        //private List<Post> posts = new List<Post>();


        public IActionResult IndexMaster()
        {
            MasterViewModel vm = new MasterViewModel();
            
            vm.BlogUsers = _userManager.Users.ToList();
            vm.Blogs = _context.Blogs.ToList();
            vm.Posts = _context.Posts
                .OrderByDescending(p => p.Updated != null)
                .ThenByDescending(p => p.Updated)
                .ThenByDescending(p => p.Created)
                .ToList();

            vm.Comments = _context.Comments
                .OrderByDescending(p => p.Updated != null)
                .ThenByDescending(p => p.Updated)
                .ThenByDescending(p => p.Created)
                .ToList();

            vm.Tags = _context.Tags
                .OrderBy(p => p.Text)
                .ToList();

            var blogPosts = _context.Blogs
                .Include(b => b.BlogUsers)
                .Include(p => p.Posts)
                .ToList();

            //ViewData["BlogList"] = blogPosts;

            return View(vm);
        }
    }
}
