using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Areas.Database.Models;
using WebApplication1.Codes;

namespace WebApplication1.Areas.Database.Controllers
{
    [Area("Database")]
    [Route("[controller]/[action]")]
    [Authorize("RequireAuthenticateUser")]
    public class Login2Controller : Controller
    {
        private readonly TestDatabaseContext _context;
        private readonly HashingExample _hashingExample;

        public Login2Controller(TestDatabaseContext context, HashingExample hashingExample)
        {
            _context = context;
            _hashingExample = hashingExample;
        }

        // GET: Database/Login2/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Database/Login2/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,User,Password,Salt")] Login2 login2)
        {
            if (ModelState.IsValid)
            {
               login2.Password = _hashingExample.BcryptHash(login2.Password);

                _context.Add(login2);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                return View("Views/Home/Index.cshtml");
            }
            return View(login2);
        }

        private bool Login2Exists(int id)
        {
            return _context.Login2s.Any(e => e.Id == id);
        }
    }
}
