using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using major_project.Data;
using major_project.Models;
using Microsoft.AspNetCore.Authorization;

namespace major_project.Controllers
{
    public class attiresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public attiresController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: attires
        public async Task<IActionResult> Index()
        {
            return View(await _context.attires.ToListAsync());
        }

        // GET: attires/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attires = await _context.attires
                .FirstOrDefaultAsync(m => m.ID == id);
            if (attires == null)
            {
                return NotFound();
            }

            return View(attires);
        }

        // GET: attires/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: attires/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,attire,availability")] attires attires)
        {
            if (ModelState.IsValid)
            {
                _context.Add(attires);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(attires);
        }

        // GET: attires/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attires = await _context.attires.FindAsync(id);
            if (attires == null)
            {
                return NotFound();
            }
            return View(attires);
        }

        // POST: attires/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int id, [Bind("ID,attire,availability")] attires attires)
        {
            if (id != attires.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(attires);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!attiresExists(attires.ID))
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
            return View(attires);
        }
        public async Task<IActionResult> ShowSearchForm()
        {
            return View();
        }
        public async Task<IActionResult> ShowSearchResults(string SearchPhrase)
        {

            return View("Index", await _context.attires.Where(a => a.attire.Contains(SearchPhrase)).ToListAsync());
        }
        public async Task<IActionResult> returngears()
        {
            return View(); 
        }
        public async Task<IActionResult> gearreturn(string SearchPhrase)
        {

            return View("Index", await _context.attires.Where(a => a.attire.Contains(SearchPhrase)).ToListAsync());
        }

        // GET: attires/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attires = await _context.attires
                .FirstOrDefaultAsync(m => m.ID == id);
            if (attires == null)
            {
                return NotFound();
            }

            return View(attires);
        }

        // POST: attires/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var attires = await _context.attires.FindAsync(id);
            _context.attires.Remove(attires);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool attiresExists(int id)
        {
            return _context.attires.Any(e => e.ID == id);
        }
    }
}
