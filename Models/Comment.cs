using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System;
using DevBlog5.Enums;

namespace DevBlog5.Models
{
    public class Comment
    {
        public int Id { get; set; }


        [Required]
        [StringLength(500, ErrorMessage = "The {0} must be betwen {2} and {1} characters.", MinimumLength = 2)]
        [Display(Name = "Comment")]
        public string Body { get; set; }


        [Display(Name = "Date Created")]
        public DateTime Created { get; set; }

        [Display(Name = "Date Updated")]
        public DateTime? Updated { get; set; }

        [Display(Name = "Date Moderated")]
        public DateTime? Moderated { get; set; }


        [Display(Name = "Date Deleted")]
        public DateTime? Deleted { get; set; }



        [StringLength(500, ErrorMessage = "The {0} must be betwen {2} and {1} characters.", MinimumLength = 2)]
        [Display(Name = "Moderated Comment")]
        public string ModeratedBody { get; set; }

        public ModerationType ModerationType { get; set; }

        // Navigation

        // Foreign keys for Post, BlogUser, Moderator
        public int PostId { get; set; }
        public string BlogUserId { get; set; }
        public string? ModeratorId { get; set; }

        // Inverse navigation to Post, BlogUser, Moderator
        public virtual Post Posts { get; set; }
        public virtual BlogUser BlogUsers { get; set; }
        public virtual BlogUser? Moderator { get; set; }
    }
}
