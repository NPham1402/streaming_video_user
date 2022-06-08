using Microsoft.AspNetCore.Mvc;

namespace streaming_video_user.Controllers
{
    public class FaverController1 : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
