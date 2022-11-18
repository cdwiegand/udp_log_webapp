using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LogWatcher.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [Route("")] // yes, I know, not required, but I prefer to be explicit
        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [Route("Logs")]
        public async Task<IActionResult> Logs()
        {
            // make call to backend UDP log server
            string backendHost = System.Environment.GetEnvironmentVariable("LOG_SERVER");
            if (string.IsNullOrEmpty(backendHost)) backendHost = "127.0.0.1:8080";
            var resp = await (new System.Net.Http.HttpClient()).GetStringAsync($"http://{backendHost}/");
            return new ContentResult() { Content = resp, ContentType = "text/plain" };
        }

    }
}