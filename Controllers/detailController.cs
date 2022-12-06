using Microsoft.AspNetCore.Mvc;
using streaming_video_user.Models;

namespace streaming_video_user.Controllers
{
    public class detailController : Controller
    {
        FilmDatabaseContext context = new FilmDatabaseContext();
        public IActionResult Index(string id)
        {
            if (id.StartsWith("A"))
            {
                var Film = from d in context.Films
                           join dflim in context.ActorFilms on d.IdFilm equals dflim.IdFilm
                           where dflim.IdActor == id
                           select new { d.Name, d.UrlImg, d.IdFilm, d.YearPublic };
                var actor = context.Actors.Where(x => x.IdActor == id).FirstOrDefault();
                ViewBag.data = actor;
                ViewBag.Film = Film;
                ViewData["nghenghiep"] = "Actor";
            }
            else
            {
                var Film = from d in context.Films
                           join dflim in context.DiretorFilms on d.IdFilm equals dflim.IdFilm
                           where dflim.Id == id
                           select new { d.Name, d.UrlImg, d.IdFilm, d.YearPublic };
                var director = context.Directors.Where(x => x.Id == id).FirstOrDefault();
                ViewBag.data = director;
                ViewBag.Film = Film;
                ViewData["nghenghiep"] = "Director"  ;
            }
            return View();
        }
    }
}
