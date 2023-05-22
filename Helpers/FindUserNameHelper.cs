using DevBlog5.Data;
using DevBlog5.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace DevBlog5.Helpers
{
    public class FindUserNameHelper : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<BlogUser> _userManager;

        public FindUserNameHelper(ApplicationDbContext context, UserManager<BlogUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult GetUserName(int blogId)
        {
            //var  userName = _userManager.Users.FirstOrDefault(u=>u.Blogs.bl)
            return View();
        }
    }
}
