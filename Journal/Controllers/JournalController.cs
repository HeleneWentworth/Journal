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
        public async Task<IActionResult> Index(string searchString)
        {
            var journal = from m in _context.Journal
                          select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                journal = journal.Where(s => s.Symptoms.Contains(searchString));

            }

            return View(await journal.ToListAsync());
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
        public async Task<IActionResult> Create([Bind("Name,ID,Date,Day,Symptoms, Body")] JournalModel journal)
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
        public async Task<IActionResult> Edit(int id, [Bind("Name,ID,Date,Day,Symptoms,Body")] JournalModel journal)
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


//Ruan my code werk nie hier nie dis hoekom ek dit net gecomment het 

//namespace Journal.Controllers
//{
//    public class JournalController : Controller
//    {
//        private readonly ILogger<HomeController> _logger;
//        private readonly JournalContext _context;
//        private readonly UserManager<User> _userManager;

//        // 3. Dependency inject the UserManager Class
//        public JournalController(ILogger<HomeController> logger, JournalContext context, UserManager<User> userManager)
//        {
//            _logger = logger;
//            _context = context;
//            _userManager = userManager;
//        }

//        // GET: Todoes
//        public async Task<IActionResult> Index()
//        {
//            // Test to see if the user is logged in
//            if (User.Identity.IsAuthenticated)
//            {
//                // Get the id of the current loggedin user
//                string id = User.FindFirst(ClaimTypes.NameIdentifier).Value;
//                User currentUser = await _context.User
//                    .Include(u => u.JournalModel)
//                    .Include(u => u.UserJournalModel)
//                        .ThenInclude(ut => ut.Journal)
//                    .FirstOrDefaultAsync(u => u.Id == id);

//                if (currentUser == null)
//                {
//                    return NotFound();
//                }


//                return View(currentUser);
//            }
//            //await _context.Journal.ToListAsync()
//            return View();
//        }

//        // GET: Todoes/Details/5
//        public async Task<IActionResult> Details(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var journal = await _context.Journal
//                .Include(t => t.UserJournal)
//                    .ThenInclude(t => t.User)
//                .FirstOrDefaultAsync(m => m.ID == id);
//            if (journal == null)
//            {
//                return NotFound();
//            }

//            return View(journal);
//        }

//        // GET: Todoes/Create
//        public IActionResult Create()
//        {
//            List<User> allUsers = _context.Users.ToList();
//            var selectAllUsers = allUsers.Select(x => new SelectListItem() { Text = x.FirstName, Value = x.Id, Selected = true });
//            // ViewBag.Name = "Cool Title";
//            ViewData["users"] = new MultiSelectList(selectAllUsers, "Value", "Text");
//            //var todo = new TodoViewModel();
//            //todo.UserIds = new List<string>();
//            return View();
//        }

//        // POST: Todoes/Create
//        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
//        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create([Bind("Id,Title,Body,UserIds")] JournalModel journalModel) //Change todo to todoModel to make more sense
//        {
//            // Make sure that the todo is related to the user on create
//            if (ModelState.IsValid)
//            {
//                var todo = new JournalModel
//                {
//                    Day = journalModel.Day,
//                    Body = journalModel.Body,
//                    Owner = _userManager.GetUserAsync(User).Result
//                };
//                journalModel.UserJournal = new List<UserJournal>();

//                if (journalModel.UserIds != null)
//                {
//                    List<User> allUsers = _context.User.Where(user => journalModel.UserIds.Contains(user.Id)).ToList();

//                    foreach (User user in allUsers)
//                    {
//                        _logger.LogInformation(user.LastName);
//                        todo.UserJournal.Add(new UserJournal { Journal = journalModel, User = user });
//                    }
//                    _context.Journal.Add(todo);
//                    await _context.SaveChangesAsync();
//                    return RedirectToAction(nameof(Index));
//                }
//                else
//                {
//                    _context.Journal.Add(todo);
//                    await _context.SaveChangesAsync();
//                    return RedirectToAction(nameof(Index));
//                }

//            }
//            return View(journalModel);
//        }

//        // GET: Todoes/Edit/5
//        public async Task<IActionResult> Edit(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var journal = await _context.Journal.FindAsync(id);
//            if (journal == null)
//            {
//                return NotFound();
//            }
//            List<User> allUsers = _context.Users.ToList();
//            var selectAllUsers = allUsers.Select(x => new SelectListItem() { Text = x.FirstName, Value = x.Id, Selected = true });
//            ViewData["users"] = new MultiSelectList(selectAllUsers, "Value", "Text");

//            var viewModel = new JournalModel();
//            viewModel.ID = journal.ID;
//            viewModel.Day = journal.Day;
//            viewModel.Owner = _userManager.GetUserAsync(User).Result;
//            viewModel.UserJournal = journal.UserJournal;
//            viewModel.UserIds = new List<string>();
//            return View(viewModel);
//        }

//        // POST: Todoes/Edit/5
//        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
//        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Body,UserIds")] JournalModel todo)
//        {
//            if (id != journal.ID)
//            {
//                return NotFound();
//            }

//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    JournalModel newJournal = new JournalModel
//                    {
//                        ID = journal.ID,
//                        Day = journalModel.Day,
//                        Body = journalModel.Body,
//                    };
//                    newJournal.Owner = _userManager.GetUserAsync(User).Result;
//                    newJournal.UserJournal = new List<UserJournal>();

//                    if (todo.UserIds != null)
//                    {
//                        List<User> allUsers = _context.User.Where(user => todo.UserIds.Contains(user.Id)).ToList();

//                        foreach (User user in allUsers)
//                        {
//                            _logger.LogInformation(user.LastName);
//                            newJournal.UserJournal.Add(new UserJournal { Journal = newJournal, User = user });
//                        }
//                        _context.Journal.Update(newJournal);
//                        await _context.SaveChangesAsync();
//                        return RedirectToAction(nameof(Index));
//                    }
//                    else
//                    {
//                        _context.Journal.Update(newJournal);
//                        await _context.SaveChangesAsync();
//                        return RedirectToAction(nameof(Index));
//                    }


//                }
//                catch (DbUpdateConcurrencyException)
//                {
//                    if (!JournalExists(journal.Id))
//                    {
//                        return NotFound();
//                    }
//                    else
//                    {
//                        throw;
//                    }
//                }
//                //return RedirectToAction(nameof(Index));
//            }
//            return View(todo);
//        }

//        // GET: Todoes/Delete/5
//        public async Task<IActionResult> Delete(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var todo = await _context.Journal
//                .FirstOrDefaultAsync(m => m.ID == id);
//            if (todo == null)
//            {
//                return NotFound();
//            }

//            return View(todo);
//        }

//        // POST: Todoes/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteConfirmed(int id)
//        {
//            var todo = await _context.Journal.FindAsync(id);
//            _context.Journal.Remove(journal);
//            await _context.SaveChangesAsync();
//            return RedirectToAction(nameof(Index));
//        }

//        private bool JournalExists(int id)
//        {
//            return _context.Journal.Any(e => e.ID == id);
//        }
//    }
//}
