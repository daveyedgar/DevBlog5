using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace DevBlog5.Models
{
    public class BlogUser : IdentityUser
    {

        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be betwen {2} and {1} characters.", MinimumLength = 2)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }


        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be betwen {2} and {1} characters.", MinimumLength = 2)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }


        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and no more than {1} characters long.", MinimumLength = 2)]
        [Display(Name = "Display Name")]
        public string? DisplayName { get; set; }


        [Display(Name = "User Image")]
        public byte[]? ImageData { get; set; }


        [Display(Name = "Image Type")]
        public string? ContentType { get; set; }



        [StringLength(100, ErrorMessage = "The {0} must be betwen {2} and {1} characters.", MinimumLength = 2)]
        [Display(Name = "Facebook")]
        public string FacebookUrl { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be betwen {2} and {1} characters.", MinimumLength = 2)]
        [Display(Name = "Twitter")]
        public string TwitterUrl { get; set; }

        [NotMapped]
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }

        // Navigation

        // Navigate to the many Posts
        public virtual ICollection<Post> Posts { get; set; } = new HashSet<Post>();

        // Navigate to the many Blogs
        public virtual ICollection<Blog> Blogs { get; set; } = new HashSet<Blog>();
    }
}
