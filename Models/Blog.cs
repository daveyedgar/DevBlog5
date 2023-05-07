using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System;

namespace DevBlog5.Models
{
    public class Blog
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Blog Name")]
        [StringLength(100, ErrorMessage = "The {0} must be betwen {2} and {1} characters.", MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "The {0} must be betwen {2} and {1} characters.", MinimumLength = 5)]
        public string Description { get; set; }

        [Display(Name = "Date Created")]
        public DateTime? Created { get; set; }

        [Display(Name = "Date Updated")]
        public DateTime? Updated { get; set; }


        [Display(Name = "Blog Image")]
        public byte[]? ImageData { get; set; }

        [Display(Name = "Image Type")]
        public string? ContentType { get; set; }

        [NotMapped]
        [Display(Name = "Image")]
        [DataType(DataType.Upload)]
        public IFormFile? Image { get; set; }

        //Navigation

        //Foreign Key
        public string BlogUserId { get; set; }

        //Inverse navigation to BlogUser
        [Display(Name = "Author")]
        public virtual BlogUser? BlogUsers { get; set; }

        // Navigate to many Posts
        public virtual ICollection<Post>? Posts { get; set; } = new HashSet<Post>();

    }
}
