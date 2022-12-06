using Microsoft.AspNetCore.Mvc;
using streaming_video_user.Models;
namespace streaming_video_user.Controllers
{
    public class playfilmController1 : Controller
    {
        FilmDatabaseContext context = new FilmDatabaseContext();
        public IActionResult Index(string id)
        {
            var data=context.Films.Where(x => x.IdFilm == id).FirstOrDefault();
            if (data != null)
            {
                ViewBag.MovieName = data.Name;
                
            }
            if (HttpContext.Session.GetString("UserEmail") != null)
            {
                ViewBag.Email = HttpContext.Session.GetString("UserEmail");
            }
            return View();
        }
    }
}
