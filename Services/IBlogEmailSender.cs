using Microsoft.AspNetCore.Identity.UI.Services;
using System.Threading.Tasks;

namespace DevBlog5.Services
{
    public interface IBlogEmailSender : IEmailSender
    {
        Task SendContactEmailAsync(string name, string emailFrom, string? phone, string subject, string htmlMessage);
    }
}
