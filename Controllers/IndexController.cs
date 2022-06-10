using Microsoft.AspNetCore.Mvc;
using streaming_video_user.Models;
namespace streaming_video_user.Controllers
{
    public class IndexController : Controller
    {
        FilmDatabaseContext context = new FilmDatabaseContext();
        RawSql rawSql= new RawSql();
        public IActionResult Index()
        {
            ViewBag.film = context.Films.Select(x => new { x.IdFilm, x.Name, x.UrlImg });
            ViewBag.Count = context.Films.Count();

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
            return View("Detail");
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
                    return View("Index",new UserSecurity { Email=HttpContext.Session.GetString("UserEmail")});
                }
            }
            return RedirectToAction("Index");
        }
        public IActionResult Profile()
        {
            Console.WriteLine(HttpContext.Session.GetString("UserEmail"));
            if (HttpContext.Session.GetString("UserEmail") != null)
            {
                ViewBag.Email = HttpContext.Session.GetString("UserEmail"); 
                string FullName="aa";
                string Age="0";
               
                var DataId = context.UserSecurities.Where(s => s.Email == HttpContext.Session.GetString("UserEmail")).FirstOrDefault();
               
                if (DataId != null)
                {
                    var UserInfo = context.Users.Where(s => s.IdUser == DataId.IdUser).FirstOrDefault();
                    if (UserInfo != null)
                    {
                        FullName = UserInfo.Name;
                        Age = UserInfo.Age.ToString();
                    }
                }
                ViewBag.Name = FullName;
                ViewBag.Age = Age;
                return View() ;
            }
            return RedirectToAction("Index");
        }
        //public ActionResult Profile()
        //{
        //    return View();
        //}
        public  IActionResult FavoriteList()
        {
            if (HttpContext.Session.GetString("UserEmail") != null)
            {
                ViewBag.Email = HttpContext.Session.GetString("UserEmail");
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
    }
}
