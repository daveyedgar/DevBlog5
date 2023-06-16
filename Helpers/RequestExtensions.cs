using Microsoft.AspNetCore.Http;

namespace DevBlog5.Helpers
{
    public static class RequestExtensions
    {
        //This method will get the previous url that was visited before the current page
        public static string GetReferrer(this HttpRequest request)
        {
            return request.GetTypedHeaders().Referer.ToString();
        }
    }
}
