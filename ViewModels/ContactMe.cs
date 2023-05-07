using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;
using System.ComponentModel.DataAnnotations;

namespace DevBlog5.ViewModels
{
    public class ContactMe
    {
        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be betwen {2} and {1} characters.", MinimumLength = 2)]
        public string Name { get; set; }

        [EmailAddress]
        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be betwen {2} and {1} characters.", MinimumLength = 2)]
        [Display(Name = "Comment")]
        public string  Email { get; set; }

        [Phone]
        public string? Phone { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be betwen {2} and {1} characters.", MinimumLength = 2)]
        public string Subject { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "The {0} must be betwen {2} and {1} characters.", MinimumLength = 2)]
        public string Message { get; set; }

    }
}
