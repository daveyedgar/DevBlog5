using DevBlog5.Models;
using System.Collections.Generic;

namespace DevBlog5.ViewModels
{
    public class MasterViewModel
    {
        public List<BlogUser> BlogUsers { get; set; }

        public List<Blog> Blogs { get; set; }

        public List<Post> Posts { get; set; }

        public List<Comment> Comments { get; set; }

        public List<Tag> Tags { get; set; }
    }
}
