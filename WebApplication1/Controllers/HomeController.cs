using Microsoft.AspNetCore.DataProtection;
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
        private readonly IDataProtector _protector;

        public HomeController(ILogger<HomeController> logger, HashingExample hashingExample, IDataProtectionProvider protector)
        {
            _logger = logger;
            _hashingExample = hashingExample;
            _protector = protector.CreateProtector("WebApplication1.HomeController.AlexanderRoikier");
        }

        public IActionResult Index(string myPassword, string myEncryption)
        {
            IndexModel? indexModel = null;

            //if (myUsername != null)
            //{
            //    string HashedValueAsString = _hashingExample.MD5Hash(myUsername);
            //    indexModel = new IndexModel() { HashedValueAsString = HashedValueAsString, OriginalText = myUsername };
            //}
            if (myPassword != null)
            {
                //Running hash
                //Fast hash
                //string HashedValueAsString = _hashingExample.MD5Hash(myPassword);

                //slow hash
                string HashedValueAsString = _hashingExample.BcryptHash(myPassword);
                indexModel = new IndexModel() { HashedValueAsString = HashedValueAsString, OriginalText = myPassword };
            }

            //Running encryption
            if (myEncryption != null)
            {
                //string payload = "Alexander";
                string protectedPayload = _protector.Protect(myEncryption);

                string unprotectedPayload = _protector.Unprotect(protectedPayload);

                indexModel = new IndexModel() { OriginalEncryptionText = unprotectedPayload, EncryptedValueAsString = protectedPayload };
            }
            return View(model: indexModel);
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