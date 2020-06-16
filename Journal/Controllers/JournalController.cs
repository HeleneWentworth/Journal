using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Journal.Models;
   
namespace Journal.Controllers
{
    public class JournalController : Controller
    {
        private readonly JournalContext _context;

        public JournalController(JournalContext context)
        {
            _context = context;
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
        //public async Task<IActionResult> Create([Bind("Name,Surname,ID,ReleaseDate,Title,PostNumber")] Journal journal)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(journal);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(journal);
        //}

        //// GET: Journal/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var journal = await _context.Journal.FindAsync(id);
        //    if (journal == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(journal);
        //}

        // POST: Journal/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        //       // public async Task<IActionResult> Edit(int id, [Bind("Name,Surname,ID,ReleaseDate,Title,PostNumber")] Journal journal)
        //        {
        //            if (id != journal.ID)
        //            {
        //                return NotFound();
        //    }

        //            if (ModelState.IsValid)
        //            {
        //                try
        //                {
        //                    _context.Update(journal);
        //                    await _context.SaveChangesAsync();
        //}
        //                catch (DbUpdateConcurrencyException)
        //                {
        //                    if (!JournalExists(journal.ID))
        //                    {
        //                        return NotFound();
        //                    }
        //                    else
        //                    {
        //                        throw;
        //                    }
        //                }
        //                return RedirectToAction(nameof(Index));
        //            }
        //            return View(journal);
        //        }

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
