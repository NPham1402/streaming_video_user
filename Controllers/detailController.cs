using Microsoft.AspNetCore.Mvc;
using streaming_video_user.Models;

namespace streaming_video_user.Controllers
{
    public class FilmCustom
    {
        public String? Name;
        public String? UrlImg;
        public String? IdFilm;
        public String? YearPublic;

        public FilmCustom(string? name, string? urlImg, string? idFilm, string? yearPublic)
        {
            Name = name;
            UrlImg = urlImg;
            IdFilm = idFilm;
            YearPublic = yearPublic;
        }
    }
    public class detailController : Controller
    {
        FilmDatabaseContext context = new FilmDatabaseContext();
        public IActionResult Index(string id)
        {
            ReceiverInfo receiverInfo = new ReceiverInfo();
            Command command = new ConcreteCommand(receiverInfo);
            Invoker invoker = new Invoker();  
            invoker.SetCommand(command);
            ViewBag.Film = invoker.FilmExcuteCommand(id);
            if (id.StartsWith("A"))
            {
                var actor = invoker.ActorExcuteCommand(id);
                ViewBag.data = actor;
                ViewData["nghenghiep"] = "Actor";
            }
            else
            {
                var director = invoker.DirectorExcuteCommand(id);
                ViewBag.data = director;
                ViewData["nghenghiep"] = "Director";
            }
            return View();
        }
    }
    public class ReceiverInfo
    {
        FilmDatabaseContext context = new FilmDatabaseContext();
        public Actor ActorInfo(string id)
        {
                var actor = context.Actors.Where(x => x.IdActor == id).FirstOrDefault();
                return actor;
        }
        public List<FilmCustom> FilmList(string id)
        {
            var Film = from d in context.Films
                       join dflim in context.DiretorFilms on d.IdFilm equals dflim.IdFilm
                       where dflim.Id == id
                       select new { d.Name, d.UrlImg, d.IdFilm, d.YearPublic };
            List<FilmCustom> filmList = new List<FilmCustom>();
            foreach(var film in Film)
            {
                filmList.Add(new FilmCustom(film.Name,film.UrlImg,film.IdFilm,film.YearPublic));
            }
            return filmList;
        }
        public Director DirectorInfo(string id)
        {
            var director = context.Directors.Where(x => x.Id == id).FirstOrDefault();
            return director;
        }
    }
    public abstract class Command
    {
        protected ReceiverInfo receiverInfo;
        public Command(ReceiverInfo receiverInfo)
        {
            this.receiverInfo = receiverInfo;
        }
        public abstract Actor ActorExecute(string id);
        public abstract Director DirectorExecute(string id);
        public abstract List<FilmCustom> FilmExcute(string id);
    }
    public class ConcreteCommand : Command
    {
        public ConcreteCommand(ReceiverInfo receiver):base(receiver)
        {

        }
        public override Actor ActorExecute(string id)
        {
            return receiverInfo.ActorInfo(id);
        }
        public override Director DirectorExecute(string id)
        {
            return receiverInfo.DirectorInfo(id);
        }
        public override List<FilmCustom> FilmExcute(string id)
        {
            return receiverInfo.FilmList(id);
        }
    }
    public class Invoker
    {
        Command? command;
        
        public void SetCommand(Command command)
        {
            this.command = command;
        }
        public Actor ActorExcuteCommand(string id)
        {
             return command.ActorExecute(id);
        }
        public Director DirectorExcuteCommand(string id)
        {
            return command.DirectorExecute(id);
        }
        public List<FilmCustom> FilmExcuteCommand(string id)
        {
            return command.FilmExcute(id);
        }
    }
}
