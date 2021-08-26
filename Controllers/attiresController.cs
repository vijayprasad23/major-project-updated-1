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
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace major_project.Controllers
{
    public class attiresController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public attiresController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
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
       public async Task<IActionResult> Create([Bind("ID,attire,availability,damaged,ImageName,AttireImage")] attires attires)
        {          
            
            if (ModelState.IsValid)
            {
                //saving image to the images folder in the wwwroot

                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(attires.AttireImage.FileName);
                string extension = Path.GetExtension(attires.AttireImage.FileName);
                attires.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine(wwwRootPath + "/Images/" + fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await attires.AttireImage.CopyToAsync(fileStream);
                }

                //inserting record to the database
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

        public async Task<IActionResult> Edit(int id, [Bind("ID,attire,availability,damaged,ImageName,AttireImage")] attires attires)
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
        public async Task<IActionResult> BorrowGears()
        {
            return View();
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

            //Delete the image from the wwwroot/images folder

            var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "Images", attires.ImageName);
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
            
            //Delete the record from the database
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
