using Microsoft.AspNetCore.Mvc;
using NewsAPI;
using NewsAPI.Models;
using NewsAPI.Constants;

namespace streaming_video_user.Controllers
{
    public class NewsController : Controller
    {
        public IActionResult Index()
        {
            var newsApiClient = new NewsApiClient("3a2b177358fd4577b507c7923486b29f");
            try {
                var articlesResponse = newsApiClient.GetEverything(new EverythingRequest
                {
                    Q = "film",
                    SortBy = SortBys.Popularity,
                });
                ViewBag.data = articlesResponse.Articles;
            } 
            catch (Exception e) 
            { 
                Console.WriteLine(e); 
            };
            
         
            return View();
            }
    }
}
