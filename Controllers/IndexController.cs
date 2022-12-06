using Microsoft.AspNetCore.Mvc;
using streaming_video_user.Models;
using System.Net;

namespace streaming_video_user.Controllers
{
    public class IndexController : Controller
    {
        FilmDatabaseContext context = new FilmDatabaseContext();
        RawSql rawSql= new RawSql();
        public IActionResult Index()
        {
            ViewBag.film = context.Films.Where(x => x.StatusDelete == false).Select(x => new { x.IdFilm, x.Name, x.UrlImg });
            ViewBag.Count = context.Films.Count();
            if (HttpContext.Session.GetString("UserEmail") != null)
            {
                ViewBag.Email = HttpContext.Session.GetString("UserEmail");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(string searchString)
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                ViewBag.film = context.Films.Select(x => new { x.IdFilm, x.Name, x.UrlImg }).Where(s => s.Name.Contains(searchString));
                ViewBag.Count = context.Films.Where(s => s.Name.Contains(searchString)).Count();
                return View();

            }
            return View();

        }
        public async Task<FileStreamResult> Get(string url)
        {

            var client = new HttpClient();
            var stream = await client.GetStreamAsync(url);
            return new FileStreamResult(stream, "application/octet-stream")
            {
                FileDownloadName = "test.mp4",
                EnableRangeProcessing = true
            };
        }
        
        public ActionResult Detail(string id)
        {
            Console.Write(id);
            Console.Write(HttpContext.Session.GetString("UserId"));
            var data = context.Users.Where(s => s.IdUser==HttpContext.Session.GetString("UserId")).FirstOrDefault();
            string statusPayment = "0";
            if (data != null)
            {
                if (data.StatusDelete == true)
                {
                    statusPayment = "1";
                    ViewBag.statusPayment = statusPayment;
                }
            }
            var dataLikeFilm = context.LikeFilms.Where(s => s.IdFilm == id && s.IdUser == HttpContext.Session.GetString("UserId")).FirstOrDefault();
            if (dataLikeFilm != null)
            {
                ViewBag.LikeFilm = "true";
            }
            if (HttpContext.Session.GetString("UserEmail") != null)
            {
                ViewBag.Email = HttpContext.Session.GetString("UserEmail");
            }
            var blogs = from d in context.Directors 
                        join dflim in context.DiretorFilms on d.Id equals dflim.Id where dflim.IdFilm==id 
                       
                        select new { d.Name, d.UrlImg,d.Id,d.Description };
            var actor = from d in context.Actors
                        join dflim in context.ActorFilms on d.IdActor equals dflim.IdActor
                        where dflim.IdFilm == id

                        select new { d.Name, d.UrlImg,d.IdActor, d.Description };
            var gerne = from d in context.Gernes
                        join dflim in context.GerneFilms on d.IdGer equals dflim.IdGer
                        where dflim.IdFilm == id

                        select new { d.Name };
            var Film = context.Films.Where(x => x.IdFilm == id).FirstOrDefault();
            ViewBag.actor = actor;
            ViewBag.Film = Film;
            ViewBag.director = blogs;
            ViewBag.gerne = gerne;
            if(id!=null)
                ViewBag.IdFilm= id;
            return View();
        }

        [HttpPost]
        public IActionResult Login(string Email,string Password)
        {
            if(Email != null || Password != null)
            {  
                var data = context.UserSecurities.Where(s => s.Email.Equals(Email) && s.Password.Equals(Password)).FirstOrDefault();
                if (data != null)
                {
                    HttpContext.Session.SetString("UserId", data.IdUser);
                    HttpContext.Session.SetString("UserEmail", data.Email);
                    ViewBag.Email=data.Email;
                    ViewBag.film = context.Films.Select(x => new { x.IdFilm, x.Name, x.UrlImg });
                    ViewBag.Count = context.Films.Count();
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }
        public IActionResult Profile()
        {
            if (HttpContext.Session.GetString("UserEmail") != null)
            {
                ViewBag.Email = HttpContext.Session.GetString("UserEmail"); 
                string FullName="aa";
                string Age="0";
                string StatusPayment = "0";
                var DataId = context.UserSecurities.Where(s => s.Email == HttpContext.Session.GetString("UserEmail")).FirstOrDefault();
                if (DataId != null)
                {
                    var UserInfo = context.Users.Where(s => s.IdUser == DataId.IdUser).FirstOrDefault();
                    if (UserInfo != null)
                    {
                        FullName = UserInfo.Name;
                        Age = UserInfo.Age.ToString();
                        if (UserInfo.StatusDelete == true)
                        {
                            StatusPayment = "1"; 
                            ViewBag.StatusPayment=StatusPayment;
                        }
                    }
                }
                ViewBag.Name = FullName;
                ViewBag.Age = Age;
                ViewBag.UserId = HttpContext.Session.GetString("UserId");
                return View() ;
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ChangeInfo(string name,string age)
        {
            int Age = Convert.ToInt32(age);
            var data = context.Users.Where(s => s.IdUser == HttpContext.Session.GetString("UserId")).FirstOrDefault();
            if (data != null)
            {
                data.Name= name;
                data.Age = (short?)Age;
                context.Entry(data).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
            }
            return RedirectToAction("Profile");
        }
        public  IActionResult FavoriteList()
        {
            if (HttpContext.Session.GetString("UserEmail") != null)
            {
                ViewBag.Email = HttpContext.Session.GetString("UserEmail");

                var dataFilm=context.LikeFilms.Where(s => s.IdUser== HttpContext.Session.GetString("UserId")).ToList();
                List<Film> FilmDetail =new List<Film>();
                for(int i = 0; i < dataFilm.Count; i++)
                {
                    Film film = context.Films.Where(s=>s.IdFilm== dataFilm[i].IdFilm).First();
                    Console.Write(dataFilm[i].IdFilm);
                    FilmDetail.Add(film);
                }

                ViewBag.LikeFilm = FilmDetail;
                return View();
            }
            return RedirectToAction("Index"); ;
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("UserId");
            HttpContext.Session.Remove("UserEmail");
            return RedirectToAction("Index");
        }
        public IActionResult RemoveFromFavorite(string id)
        {
            LikeFilm film = new LikeFilm();
            film.IdFilm = id;
            film.IdUser = HttpContext.Session.GetString("UserId");
            var dataFilm = context.LikeFilms.Where(s => s.IdFilm == id && s.IdUser==film.IdUser).First();
            context.LikeFilms.Attach(dataFilm);
            context.LikeFilms.Remove(dataFilm);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult AppendFavorite(string id)
        {
            if (HttpContext.Session.GetString("UserId") != null)
            {
                LikeFilm film = new LikeFilm();
                film.IdUserFilm = " ";
                film.IdFilm = id;
                film.IdUser = HttpContext.Session.GetString("UserId");
                context.LikeFilms.Add(film);
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Register(string Username, string Email, string Password, string ConfirmPassword)
        {
            var Exist=context.UserSecurities.Where(x => x.Email == Email).FirstOrDefault();
            if(Exist == null)
            {
                User InsertUser = new User();
                InsertUser.Name= Username;
                InsertUser.Age = 0;
                InsertUser.StatusDelete = false;
                InsertUser.UrlImg = " ";
                InsertUser.IdUser = "";
                context.Users.Add(InsertUser);
                context.SaveChanges();
                var Data=context.Users.Where(x=>x.Name==Username).FirstOrDefault();
                if(Data != null)
                {
                    UserSecurity userSecurity = new UserSecurity();
                    userSecurity.Email = Email;
                    userSecurity.Password = Password;
                    userSecurity.StatusDelete = false;
                    userSecurity.IdUser=Data.IdUser;
                    userSecurity.IdUserNavigation = Data;
                    context.UserSecurities.Add(userSecurity);
                    context.SaveChanges();
                    HttpContext.Session.SetString("UserId", Data.IdUser);
                    HttpContext.Session.SetString("UserEmail", Email); 
                    return RedirectToAction("Index");
                }
            }
            return View();
        }
        [HttpPost]
        public IActionResult ChangePassword(string OldPassword,string NewPassword)
        {
            var OldPasswordAccount = context.UserSecurities.Where(s => s.Password == OldPassword && s.IdUser==HttpContext.Session.GetString("UserId")).FirstOrDefault();
            if(OldPasswordAccount != null)
            {
                OldPasswordAccount.Password= NewPassword;
                context.Entry(OldPasswordAccount).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
                ViewBag.MessagePassword = "Change Password Success";
                return RedirectToAction("Profile");
            }
            ViewBag.MessagePasswordFailed = "Old password was wrong";
            return RedirectToAction("Profile");
        }
        [HttpPost]
        public IActionResult Payment()
        {
            {
                var data = context.Users.Where(s => s.IdUser == HttpContext.Session.GetString("UserId")).FirstOrDefault();
                if (data != null)
                {
                    data.StatusDelete= true;
                    context.Entry(data).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                    return RedirectToAction("Profile");
                }
                return RedirectToAction("Profile");
            }
        }
    }
}
