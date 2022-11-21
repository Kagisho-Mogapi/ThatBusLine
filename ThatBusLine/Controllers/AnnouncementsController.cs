using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ThatBusLine.Areas.Identity.Data;
using ThatBusLine.Models;

namespace ThatBusLine.Controllers
{
    public class AnnouncementsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private ApplicationUser? _user;

        public AnnouncementsController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager,ApplicationDbContext context)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // GET: Announcements
        public async Task<IActionResult> Index()
        {
            if (_signInManager.IsSignedIn(User))
            {
                _user = await _userManager.GetUserAsync(User);
                return _context.Announcement != null ?
                          View(_context.Announcement.ToList()) :
                          Problem("Entity set 'ApplicationDbContext.Announcement'  is null.");
            }
            return View();
        }

        // GET: Announcements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Announcement == null)
            {
                return NotFound();
            }

            var announcement = await _context.Announcement
                .FirstOrDefaultAsync(m => m.ID == id);
            if (announcement == null)
            {
                return NotFound();
            }

            return View(announcement);
        }

        // GET: Announcements/Create
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Announcements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Create([Bind("ID,Title,Message,MessageTarget")] Announcement announcement)
        {
            if (ModelState.IsValid)
            {
                _context.Add(announcement);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(announcement);
        }

        // GET: Announcements/Edit/5
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Announcement == null)
            {
                return NotFound();
            }

            var announcement = await _context.Announcement.FindAsync(id);
            if (announcement == null)
            {
                return NotFound();
            }
            return View(announcement);
        }

        // POST: Announcements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Title,Message,MessageTarget")] Announcement announcement)
        {
            if (id != announcement.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(announcement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnnouncementExists(announcement.ID))
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
            return View(announcement);
        }

        // GET: Announcements/Delete/5
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Announcement == null)
            {
                return NotFound();
            }

            var announcement = await _context.Announcement
                .FirstOrDefaultAsync(m => m.ID == id);
            if (announcement == null)
            {
                return NotFound();
            }

            return View(announcement);
        }

        // POST: Announcements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Announcement == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Announcement'  is null.");
            }
            var announcement = await _context.Announcement.FindAsync(id);
            if (announcement != null)
            {
                _context.Announcement.Remove(announcement);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Administrator")]
        private bool AnnouncementExists(int id)
        {
          return (_context.Announcement?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
