using Microsoft.AspNetCore.Http;

namespace DevBlog5.Helpers
{
    public static  class RequestExtensions
    {
        public static string GetReferrer(this HttpRequest request)
        {
            return request.GetTypedHeaders().Referer.ToString();
        }
    }
}
