using Microsoft.AspNetCore.Mvc;
using streaming_video_user.Models;
namespace streaming_video_user.Controllers
{
    public class IndexController : Controller
    {
        FilmDatabaseContext context = new FilmDatabaseContext();
        public IActionResult Index()
        {
            ViewBag.film = context.Films.Select(x => new { x.IdFilm, x.Name, x.UrlImg });
            ViewBag.Count = context.Films.Count(); 
            return View();
        }
        public ActionResult Detail(string id)
        {
            var blogs = from d in context.Directors 
                        join dflim in context.DiretorFilms on d.Id equals dflim.Id where dflim.IdFilm==id 
                       
                        select new { d.Name, d.UrlImg };
            var actor = from d in context.Actors
                        join dflim in context.ActorFilms on d.IdActor equals dflim.IdActor
                        where dflim.IdFilm == id

                        select new { d.NameActor, d.UrlImg };
            var gerne = from d in context.Gernes
                        join dflim in context.GerneFilms on d.IdGer equals dflim.IdGer
                        where dflim.IdFilm == id

                        select new { d.Name };
            var Film = context.Films.Where(x => x.IdFilm == id).FirstOrDefault();
            ViewBag.Film = Film;
            ViewBag.director = blogs;
            ViewBag.actor = actor;
            ViewBag.gerne = gerne;
            return View();
        }
    }
}
