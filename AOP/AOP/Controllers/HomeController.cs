using AOP.Services;
using Castle.DynamicProxy;
using DynamicProxyLog;
using System.Web.Mvc;

namespace AOP.Controllers
{
    public class HomeController : Controller
    {
        private IGreetingService _greetingService;

        public HomeController(IGreetingService greetingService)
        {
            _greetingService = greetingService;
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string name)
        {
            return View(_greetingService.SayHello(name));
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}