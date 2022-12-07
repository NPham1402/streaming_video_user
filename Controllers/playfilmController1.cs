using Microsoft.AspNetCore.Mvc;
using streaming_video_user.Models;
namespace streaming_video_user.Controllers
{
    public class playfilmController1 : Controller
    {
        
        public IActionResult Index(string id)
        {
           
            VideoReceiver videoReceiver = new ConcreteVideoReceiver();
            var data =videoReceiver.GetFilmObject(id);
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
    abstract class VideoReceiver
    {
        public abstract Film GetFilmObject(string id);
    }
    class ConcreteVideoReceiver:VideoReceiver
    {
        FilmDatabaseContext context = new FilmDatabaseContext();
        public override Film GetFilmObject(string id)
        {
            var data = context.Films.Where(x => x.IdFilm == id).FirstOrDefault();
            return (Film)data;  
        }

    }
}
