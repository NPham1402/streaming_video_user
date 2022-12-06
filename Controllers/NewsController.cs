using Microsoft.AspNetCore.Mvc;
using NewsAPI;
using NewsAPI.Models;
using NewsAPI.Constants;
using streaming_video_user.Models;
namespace streaming_video_user.Controllers
{
    public class NewsController : Controller
    {
        FilmDatabaseContext context = new FilmDatabaseContext();
        public IActionResult Index()
        {
            var DataId = context.UserSecurities.Where(s => s.Email == HttpContext.Session.GetString("UserEmail")).FirstOrDefault();
            string StatusPayment = "0";
            if (DataId != null)
            {
                var UserInfo = context.Users.Where(s => s.IdUser == DataId.IdUser).FirstOrDefault();
                if (UserInfo != null)
                {
                    if (UserInfo.StatusDelete == true)
                    {
                        StatusPayment = "1";
                        ViewBag.StatusPayment = StatusPayment;
                    }
                }
            }
            var newsApiClient = new NewsApiClient("3a2b177358fd4577b507c7923486b29f");
            try
            {
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
            if (HttpContext.Session.GetString("UserEmail") != null)
            {
                ViewBag.Email = HttpContext.Session.GetString("UserEmail");     
            } 
            return View();
        }
    }
}
