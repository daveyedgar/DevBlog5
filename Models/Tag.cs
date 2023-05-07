using System.ComponentModel.DataAnnotations;

namespace DevBlog5.Models
{
    public class Tag
    {
        public int Id { get; set; }

        [Required]
        [StringLength(25, ErrorMessage = "The {0} must be betwen {2} and {1} characters.", MinimumLength = 2)]
        public string Text { get; set; }

        // Navigation

        // Foreign keys for Post and BlogUser
        public int PostId { get; set; }
        public string BlogUserId { get; set; }

        // Inverse navigation to Post and BlogUser
        public virtual Post Posts { get; set; }
        public virtual BlogUser BlogUsers { get; set; }
    }
}
