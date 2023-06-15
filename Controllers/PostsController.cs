using DevBlog5.Data;
using DevBlog5.Enums;
using DevBlog5.Helpers;
using DevBlog5.Models;
using DevBlog5.Services;
using DevBlog5.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;


namespace DevBlog5.Controllers
{
    public class PostsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ISlugService _slugService;
        private readonly IImageService _imageService;
        private readonly UserManager<BlogUser> _userManager;
        private readonly BlogSearchService _blogSearchService;

        public PostsController(ApplicationDbContext context, ISlugService slugService, IImageService imageService, UserManager<BlogUser> userManager, BlogSearchService blogSearchService)
        {
            _context = context;
            _slugService = slugService;
            _imageService = imageService;
            _userManager = userManager;
            _blogSearchService = blogSearchService;
        }

        // Search
        public async Task<IActionResult> SearchIndex(int? page, string searchTerm)
        {
            ViewData["SearchTerm"] = searchTerm;
            var pageNumber = page ?? 1;
            var pageSize = 2;

            var posts = _blogSearchService.Search(searchTerm);

            return View(await posts.ToPagedListAsync(pageNumber, pageSize));
        }

        // GET: Posts
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Posts
                .Include(p => p.Blog)
                .Include(p => p.BlogUser)
                .OrderByDescending(p => p.Updated != null)
                .ThenByDescending(p => p.Updated)
                .ThenByDescending(p => p.Created);

            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> IndexFilter(string? readyStatus)
        {

            var posts = new List<Post>();
            if (readyStatus is null)
            {
                posts = await _context.Posts
                .Include(p => p.Blog)
                .Include(p => p.BlogUser)
                .OrderByDescending(p => p.Updated != null)
                .ThenByDescending(p => p.Updated)
                .ThenByDescending(p => p.Created)
                .ToListAsync();
            }
            else
            {
                int readyStatusInt = ((int)Enum.Parse(typeof(ReadyStatus), readyStatus));
                // will need if readyStatus not found alert
                posts = await _context.Posts?
                .Where(p => p.ReadyStatus.Equals(readyStatusInt))
                //.Where(p => p.ReadyStatus = readyStatusInt)
                .Include(p => p.Blog)
                .Include(p => p.BlogUser)
                .OrderByDescending(p => p.Updated != null)
                .ThenByDescending(p => p.Updated)
                .ThenByDescending(p => p.Created)
                .ToListAsync();
            }

            System.Diagnostics.Debug.WriteLine("hello");
            ViewData["ReadyStatus"] = new SelectList(Enum.GetValues(typeof(ReadyStatus)).Cast<ReadyStatus>().ToList());
            return View(posts);
        }

        public IActionResult PostsEmpty(int? id)
        {
            var blog = _context.Blogs
                .Where(p => p.Id == id)
                .ToList();

            ViewData["BlogId"] = id;
            ViewData["BlogUserId"] = new SelectList(_context.Users, "Id", "Id");

            return View();
        }

        // GET: BlogPostIndex
        public async Task<IActionResult> BlogPostIndex(int? id, int? page)
        {
            if (id is null)
            {
                return NotFound();
            }

            var pageNumber = page ?? 1;  // null coelescing operator
            var pageSize = 6;  // amount per page

            var blogId = id;

            var blogPosts = await _context.Posts
                .Where(p => p.BlogId == id && p.ReadyStatus == ReadyStatus.ProductionReady)
                .Include(p => p.Blog)
                .Include(u => u.BlogUser)
                .OrderByDescending(p => p.Created)
                .ToPagedListAsync(pageNumber, pageSize);

            if (blogPosts.Count < 1)
            {
                return RedirectToAction("PostsEmpty", new { id = blogId });
            }

            ViewData["BlogName"] = blogPosts[0].Blog.Name;  // will get null error if no posts
            ViewData["BlogId"] = blogPosts[0].BlogId; // will get null error if no posts
            ViewData["PageCount"] = pageNumber;
            ViewData["UserName"] = blogPosts[0].BlogUser.FullName;

            ViewData["BlogImageData"] = blogPosts[0].Blog.ImageData;
            ViewData["BlogImageType"] = blogPosts[0].Blog.ContentType;


            TempData["CurrentPage"] = page;

            return View(blogPosts);

        }

        // GET: TagPostIndex
        public async Task<IActionResult> TagPostIndex(string? tag, int? page)
        {
            if (tag is null)
            {
                return NotFound();
            }

            TempData["ReturnUrl"] = Request.GetReferrer();

            var pageNumber = page ?? 1;  // null coelescing operator
            var pageSize = 2;  // amount per page

            var tagPosts = await _context.Tags
                .Where(p => p.Text == tag)
                .Include(p => p.Posts)
                .OrderByDescending(p => p.Posts.Created)
                .ToPagedListAsync(pageNumber, pageSize);

            if (tagPosts.Count < 1)
            {
                return RedirectToAction("PostsEmpty");
            }

            ViewData["PostImageData"] = tagPosts[0].Posts.ImageData;
            ViewData["PostImageType"] = tagPosts[0].Posts.ContentType;


            ViewData["TagText"] = tagPosts[0].Text;

            TempData["CurrentPage"] = page;

            return View(tagPosts);

        }


        // GET: Posts/Details/5
        public async Task<IActionResult> Details(string slug)
        {
            ViewData["Title"] = "Post Details Page";
            if (string.IsNullOrEmpty(slug)) return NotFound();

            var post = await _context.Posts
                .Include(p => p.BlogUser)
                .Include(p => p.Blog)    /*added to try to access blog id for back to list link*/
                .Include(p => p.Tags)
                .Include(p => p.Comments)
                .ThenInclude(c => c.BlogUsers)
                .Include(p => p.Comments)
                .ThenInclude(c => c.Moderator)
                .FirstOrDefaultAsync(m => m.Slug == slug);

            if (post == null) return NotFound();

            var dataVM = new PostDetailViewModel()
            {
                Post = post,
                Tags = _context.Tags
                        .Select(t => t.Text.ToLower())
                        .Distinct().ToList()
            };

            ViewData["HeaderImage"] = _imageService.DecodeImage(post.ImageData, post.ContentType);
            ViewData["MainText"] = post.Title;
            ViewData["SubText"] = post.Abstract;

            ViewData["BlogId"] = post.Blog.Name;

            ViewData["BlogImageData"] = post.Blog.ImageData;
            ViewData["BlogImageType"] = post.Blog.ContentType;

            return View(dataVM);
        }


        // GET: Posts/Create
        [Authorize(Roles = "Administrator")]
        public IActionResult Create(int? id, string? blogName)
        {
            TempData["ReturnUrl"] = Request.GetReferrer();
            var blogId = id;

            ViewData["BlogIdNum"] = blogId;
            ViewData["BlogName"] = blogName;
            ViewData["BlogId"] = new SelectList(_context.Blogs, "Id", "Name", id);
            ViewData["BlogUserId"] = new SelectList(_context.Users, "Id", "FullName");

            return View();
        }


        // POST: Posts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Create([Bind("Title,Abstract,Content,ReadyStatus,Image,BlogId")] Post post, List<string> tagValues)
        {

            if (ModelState.IsValid)
            {

                post.Created = DateTime.Now;

                var authorId = _userManager.GetUserId(User);
                post.BlogUserId = authorId;

                post.ImageData = await _imageService.EncodeImageAsync(post.Image);
                post.ContentType = _imageService.ContentType(post.Image);


                var slug = _slugService.UrlFriendly(post.Title);

                var validationError = false;

                if (string.IsNullOrEmpty(slug))
                {
                    validationError = true;
                    ModelState.AddModelError("", "Please provide a title.");
                }

                else if (!_slugService.IsUnique(slug))
                {
                    validationError = true;
                    ModelState.AddModelError("Title", "This title already exists.");
                }

                if (validationError)
                {
                    ViewData["TagValues"] = string.Join(", ", tagValues);
                    return View(post);
                }

                post.Slug = slug;

                _context.Add(post);
                await _context.SaveChangesAsync();


                foreach (var tagText in tagValues)
                {
                    _context.Add(new Tag()
                    {
                        PostId = post.Id,
                        BlogUserId = authorId,
                        Text = tagText
                    });
                }

                await _context.SaveChangesAsync();


                TempData["SuccessMessage"] = "Post created.";

                return RedirectToAction("Details", new { slug = post.Slug });
            }
            else
            {
                var errors = ModelState.Select(x => x.Value.Errors)
                                       .Where(y => y.Count > 0)
                                       .ToList();
            }
            ViewData["BlogId"] = new SelectList(_context.Blogs, "Id", "Description", post.BlogId);
            ViewData["BlogUserId"] = new SelectList(_context.Users, "Id", "Id", post.BlogUserId);
            return View(post);
        }

        // GET: Posts/Edit/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(string slug, int? blogId)
        {
            TempData["ReturnUrl"] = Request.GetReferrer();

            if (slug == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .Include(p => p.Tags)
                .Include(p => p.Blog)
                .FirstOrDefaultAsync(p => p.Slug == slug);
            if (post == null)
            {
                return NotFound();
            }

            var bId = blogId;

            ViewData["BlogId"] = bId;

            ViewData["BlogList"] = new SelectList(_context.Blogs, "Id", "Name", post.BlogId);
            ViewData["TagValues"] = string.Join(",", post.Tags.Select(t => t.Text));
            ViewData["BlogName"] = post.Blog.Name;
            return View(post);
        }

        // POST: Posts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Abstract,Content,ReadyStatus,BlogId")] Post post, IFormFile newImage, List<string> tagValues)
        {
            var newSlug = "";
            if (id != post.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // the original post put into the newPost variable
                    var newPost = await _context.Posts.Include(p => p.Tags).FirstOrDefaultAsync(p => p.Id == post.Id);

                    newPost.Title = post.Title;
                    newPost.Abstract = post.Abstract;
                    newPost.Content = post.Content;
                    newPost.ReadyStatus = post.ReadyStatus;

                    newPost.Updated = DateTime.Now;

                    // this is the new slug
                    newSlug = _slugService.UrlFriendly(post.Title);
                    if (newSlug != newPost.Slug)
                    {
                        if (_slugService.IsUnique(newSlug))
                        {
                            newPost.Title = post.Title;
                            newPost.Slug = newSlug;

                        }
                        else
                        {
                            ModelState.AddModelError("Title", "This title already exists.");
                            ViewData["TagValues"] = string.Join(", ", tagValues);
                            return View(post);
                        }
                    }


                    if (newImage != null)
                    {
                        newPost.ImageData = await _imageService.EncodeImageAsync(newImage);
                        newPost.ContentType = _imageService.ContentType(newImage);
                    }


                    _context.Tags.RemoveRange(newPost.Tags);

                    foreach (var tagText in tagValues)
                    {
                        _context.Add(new Tag()
                        {
                            PostId = post.Id,
                            BlogUserId = newPost.BlogUserId,
                            Text = tagText
                        });

                    }
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                TempData["SuccessMessage"] = "Changes have been saved.";

                int currentPage = 1;
                if (TempData["CurrentPage"] != null)
                {
                    currentPage = (int)TempData["CurrentPage"];
                }

                var blogId = id;
                return RedirectToAction("Details", new { slug = newSlug });
            }
            ViewData["BlogId"] = new SelectList(_context.Blogs, "Id", "Description", post.BlogId);
            ViewData["BlogUserId"] = new SelectList(_context.Users, "Id", "Id", post.BlogUserId);
            return View(post);
        }

        // GET: Posts/Delete/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(string slug)
        {
            TempData["ReturnUrl"] = Request.GetReferrer();

            if (slug == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .Include(p => p.Blog)
                .Include(p => p.BlogUser)
                .FirstOrDefaultAsync(p => p.Slug == slug);
            if (post == null)
            {
                return NotFound();
            }


            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Posts == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Posts'  is null.");
            }
            var post = await _context.Posts.FindAsync(id);
            if (post != null)
            {
                _context.Posts.Remove(post);
            }

            await _context.SaveChangesAsync();


            TempData["SuccessMessage"] = "Post deleted.";

            int currentPage = 1;
            if (TempData["CurrentPage"] != null)
            {
                currentPage = (int)TempData["CurrentPage"];
            }

            var returnUrl = TempData["ReturnUrl"].ToString();
            return Redirect(returnUrl);
        }

        private bool PostExists(int id)
        {
            return _context.Posts.Any(e => e.Id == id);
        }
    }
}
