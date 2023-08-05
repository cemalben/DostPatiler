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
using System.Security.Claims;

namespace DostPatiler.Controllers
{
    [Authorize(Roles = "User")]
    public class BasvuruController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly IWebHostEnvironment _hostingEnvironment;
        public BasvuruController(ApplicationDbContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Sahiplen()
        {
            return View(_context.Hayvanlar.ToList());
        }

        public IActionResult Sahiplendir()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Sahiplendir([Bind("HayvanId,HayvanCins,HayvanCinsiyet,HayvanTur,HayvanYas,HayvanImage")] Hayvan hayvan)
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
                hayvan.UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                hayvan.Durum = Surec.Beklemede;
                hayvan.Sahiplendirme = true;

                _context.Add(hayvan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Sahiplendir));
            }
            return View("Sahiplendir");
        }
    }
}
