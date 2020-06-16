using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Journal.Data;
using Journal.Models;


namespace Journal.Controllers
{
    public class JournalController : Controller
    {
        private readonly JournalContext _context;
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<User> _userManager;


        // 3. Dependency inject the UserManager Class
        public JournalController(ILogger<HomeController> logger, JournalContext context, UserManager<User> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }


        // GET: Journal
        public async Task<IActionResult> Index()
        {
            return View(await _context.Journal.ToListAsync());
        }

        // GET: Journal/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var journal = await _context.Journal
                .FirstOrDefaultAsync(m => m.ID == id);
            if (journal == null)
            {
                return NotFound();
            }

            return View(journal);
        }

        // GET: Journal/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Journal/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Surname,ID,ReleaseDate,Title,PostNumber")] JournalModel journal)
        {
            if (ModelState.IsValid)
            {
                _context.Add(journal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(journal);
        }

        // GET: Journal/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var journal = await _context.Journal.FindAsync(id);
            if (journal == null)
            {
                return NotFound();
            }
            return View(journal);
        }

        // POST: Journal/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
              public async Task<IActionResult> Edit(int id, [Bind("Name,Surname,ID,ReleaseDate,Title,PostNumber")] JournalModel journal)
                {
                    if (id != journal.ID)
                    {
                        return NotFound();
            }

                    if (ModelState.IsValid)
                    {
                        try
                        {
                            _context.Update(journal);
                            await _context.SaveChangesAsync();
        }
                        catch (DbUpdateConcurrencyException)
                        {
                            if (!JournalExists(journal.ID))
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
                    return View(journal);
                }

        // GET: Journal/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var journal = await _context.Journal
                .FirstOrDefaultAsync(m => m.ID == id);
            if (journal == null)
            {
                return NotFound();
            }

            return View(journal);
        }

        // POST: Journal/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var journal = await _context.Journal.FindAsync(id);
            _context.Journal.Remove(journal);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JournalExists(int id)
        {
            return _context.Journal.Any(e => e.ID == id);
        }
    }
}
