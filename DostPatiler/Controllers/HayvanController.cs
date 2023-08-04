using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DostPatiler.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using static System.Net.WebRequestMethods;

namespace DostPatiler.Controllers
{
    [Authorize(Roles = "Admin")]
    public class HayvanController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly IWebHostEnvironment _hostingEnvironment;
        public HayvanController(ApplicationDbContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: Hayvanlar
        public async Task<IActionResult> Index()
        {
            return View(await _context.Hayvanlar.ToListAsync());
        }

        // GET: Hayvanlar/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hayvan = await _context.Hayvanlar
                .FirstOrDefaultAsync(m => m.HayvanId == id);
            if (hayvan == null)
            {
                return NotFound();
            }

            return View(hayvan);
        }

        // GET: Hayvanlar/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Hayvanlar/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HayvanId,HayvanCins,HayvanCinsiyet,HayvanTur,HayvanYas,HayvanImage")] Hayvan hayvan)
        {
            if (ModelState.IsValid)
            {
                string webRootPath = _hostingEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;


                string fileName = Guid.NewGuid().ToString();
                var uploads = Path.Combine(webRootPath, @"img\hayvanlar");
                var extension = Path.GetExtension(files[0].FileName);

                using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                }
                hayvan.HayvanImage = @"\img\hayvanlar\" + fileName + extension;
                hayvan.CurrentHayvanImage = hayvan.HayvanImage;

                _context.Add(hayvan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hayvan);
        }

        // GET: Hayvanlar/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hayvan = await _context.Hayvanlar.FindAsync(id);
            if (hayvan == null)
            {
                return NotFound();
            }
            return View(hayvan);
        }

        // POST: Hayvanlar/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HayvanId,HayvanCinsiyet,HayvanTur,HayvanCins,HayvanYas,CurrentHayvanImage,HayvanImage")] Hayvan hayvan)
        {
            if (id != hayvan.HayvanId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string webRootPath = _hostingEnvironment.WebRootPath;
                    var files = HttpContext.Request.Form.Files;
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRootPath, @"img\hayvanlar");
                    if (files.Count > 0)
                    {

                        var extension = Path.GetExtension(files[0].FileName);

                        using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                        {
                            files[0].CopyTo(fileStream);
                        }
                        hayvan.HayvanImage = @"\img\hayvanlar\" + fileName + extension;
                        hayvan.CurrentHayvanImage = hayvan.HayvanImage;
                    }
                    else
                    {
                        hayvan.HayvanImage = hayvan.CurrentHayvanImage;
                    }

                    _context.Update(hayvan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HayvanExists(hayvan.HayvanId))
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
            return View(hayvan);
        }

        // GET: Hayvanlar/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hayvan = await _context.Hayvanlar
                .FirstOrDefaultAsync(m => m.HayvanId == id);
            if (hayvan == null)
            {
                return NotFound();
            }

            return View(hayvan);
        }

        // POST: Hayvanlar/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hayvan = await _context.Hayvanlar.FindAsync(id);
            _context.Hayvanlar.Remove(hayvan);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HayvanExists(int id)
        {
            return _context.Hayvanlar.Any(e => e.HayvanId == id);
        }
    }
}
