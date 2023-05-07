using DevBlog5.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevBlog5.Data
{
    public class ApplicationDbContext : IdentityDbContext<BlogUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Tag> Tags { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    // blog to post
        //    base.OnModelCreating(modelBuilder);
        //    modelBuilder.Entity<Blog>()
        //        .HasMany(p => p.Posts)
        //        .WithOne(t => t.Blog)
        //        .OnDelete(DeleteBehavior.ClientCascade);

        //    // post to comment
        //    base.OnModelCreating(modelBuilder);
        //    modelBuilder.Entity<Post>()
        //        .HasMany(p => p.Comments)
        //        .WithOne(t => t.Posts)
        //        .OnDelete(DeleteBehavior.ClientCascade);

        //    // post to tag
        //    base.OnModelCreating(modelBuilder);
        //    modelBuilder.Entity<Post>()
        //        .HasMany(p => p.Tags)
        //        .WithOne(t => t.Posts)
        //        .OnDelete(DeleteBehavior.ClientCascade);

        //}
    }
}
