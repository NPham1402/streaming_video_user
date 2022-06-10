using Microsoft.AspNetCore.Mvc;

namespace streaming_video_user.Controllers
{
    public class playfilmController1 : Controller
    {
        public IActionResult Index(string id)
        {
            string temp = id;
            return View();
        }
    }
}
