using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Areas.TodoList.Models;

namespace WebApplication1.Areas.TodoList.Controllers
{
    [Area("TodoList")]
    [Route("[controller]/[action]")]
    [Authorize("RequireAuthenticateUser")]
    public class ToDoListsController : Controller
    {
        private readonly TestDatabaseContext _context;
        private readonly IDataProtector _protector;

        public ToDoListsController(TestDatabaseContext context, IDataProtectionProvider protector)
        {
            _context = context;
            _protector = protector.CreateProtector("WebApplication1.HomeController.AlexanderRoikier");
        }

        // GET: TodoList/ToDoLists
        public async Task<IActionResult> Index()
        {
            string? user = User.Identity.Name;
            var model = await _context.ToDoLists.Where(x => x.Username == user).ToListAsync();
            bool isNotEmpty = model.Count > 0;

            if (isNotEmpty)
            {
                foreach (ToDoList item in model)
                {
                    if (item.Description != null)
                    {
                        item.Description = _protector.Unprotect(item.Description);
                    }
                    item.Titel = _protector.Unprotect(item.Titel);
                }
                return View(model);
            }
            else
            {
                return View(new List<ToDoList>());
            }
        }

        // GET: TodoList/ToDoLists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toDoList = await _context.ToDoLists
                .FirstOrDefaultAsync(m => m.Id == id);
            if (toDoList == null)
            {
                return NotFound();
            }

            return View(toDoList);
        }

        // GET: TodoList/ToDoLists/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TodoList/ToDoLists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titel,Description,Username")] ToDoList toDoList)
        {
            if (ModelState.IsValid)
            {
                toDoList.Titel = _protector.Protect(toDoList.Titel);
                if (toDoList.Description != null)
                {
                    toDoList.Description = _protector.Protect(toDoList.Description);
                }
                _context.Add(toDoList);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(toDoList);
        }

        // GET: TodoList/ToDoLists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toDoList = await _context.ToDoLists.FindAsync(id);
            if (toDoList == null)
            {
                return NotFound();
            }
            return View(toDoList);
        }

        // POST: TodoList/ToDoLists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titel,Description,Username")] ToDoList toDoList)
        {
            if (id != toDoList.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(toDoList);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ToDoListExists(toDoList.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(toDoList);
        }

        // GET: TodoList/ToDoLists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toDoList = await _context.ToDoLists
                .FirstOrDefaultAsync(m => m.Id == id);
            if (toDoList == null)
            {
                return NotFound();
            }

            return View(toDoList);
        }

        // POST: TodoList/ToDoLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var toDoList = await _context.ToDoLists.FindAsync(id);
            _context.ToDoLists.Remove(toDoList);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ToDoListExists(int id)
        {
            return _context.ToDoLists.Any(e => e.Id == id);
        }
    }
}
