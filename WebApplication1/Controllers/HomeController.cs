using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication1.Codes;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HashingExample _hashingExample;

        public HomeController(ILogger<HomeController> logger, HashingExample hashingExample)
        {
            _logger = logger;
            _hashingExample = hashingExample;

        }

        public IActionResult Index(string myPassword, string myUsername)
        {
            IndexModel? indexModel = null;

            //if (myUsername != null)
            //{
            //    string HashedValueAsString = _hashingExample.MD5Hash(myUsername);
            //    indexModel = new IndexModel() { HashedValueAsString = HashedValueAsString, OriginalText = myUsername };
            //}
            if (myPassword != null)
            {
                //Fast hash
                //string HashedValueAsString = _hashingExample.MD5Hash(myPassword);

                //slow hash
                string HashedValueAsString = _hashingExample.BcryptHash(myPassword);
                indexModel = new IndexModel() { HashedValueAsString = HashedValueAsString, OriginalText = myPassword, Username = myUsername};
            }

            return View(model : indexModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}