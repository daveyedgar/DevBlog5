using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System;
using DevBlog5.Enums;

namespace DevBlog5.Models
{
    public class Post
    {
        public int Id { get; set; }

        [Required]
        [StringLength(75, ErrorMessage = "The {0} must be betwen {2} and {1} characters.", MinimumLength = 2)]
        public string Title { get; set; }


        [Required]
        [StringLength(200, ErrorMessage = "The {0} must be betwen {2} and {1} characters.", MinimumLength = 2)]
        public string Abstract { get; set; }

        [Required]
        public string Content { get; set; }



        [Display(Name = "Date Created")]
        public DateTime? Created { get; set; }


        [Display(Name = "Date Updated")]
        public DateTime? Updated { get; set; }

        //public bool IsReady { get; set; }
        public ReadyStatus ReadyStatus { get; set; }

        public string? Slug { get; set; }


        [Display(Name = "Post Image")]
        public byte[]? ImageData { get; set; }

        [Display(Name = "Image Type")]
        public string? ContentType { get; set; }

        [NotMapped]
        [Display(Name = "Image")]
        [DataType(DataType.Upload)]
        public IFormFile? Image { get; set; }

        //Navigation

        // Foreign key for Blog
        [Display(Name = "Blog Name")]
        public int BlogId { get; set; }

        // Foreign key for BlogUser
        public string? BlogUserId { get; set; }

        // Inverse navigation to Blog
        public virtual Blog? Blog { get; set; }

        // Inverse navigation to BlogUser
        public virtual BlogUser? BlogUser { get; set; }

        // Navigate to many tags
        public ICollection<Tag> Tags { get; set; } = new HashSet<Tag>();

        // Navigate to many Comments
        public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
    }
}
