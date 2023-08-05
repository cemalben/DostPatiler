using DostPatiler.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DostPatiler.Controllers
{
    public class HayvanlarController : Controller
    {
        private readonly ApplicationDbContext db;

        public HayvanlarController(ApplicationDbContext _db)
        {
            db = _db;
        }

        public IActionResult Index()
        {
            return View(db.Hayvanlar.ToList());
        }

        public async Task<IActionResult> MakeAnApplicationAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Hayvan hayvan = db.Hayvanlar.FirstOrDefault(x => x.HayvanId == id);
            if (hayvan == null)
            {
                return NotFound();
            }

            hayvan.Durum = Surec.Beklemede;
            hayvan.UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Hayvan(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hayvan = db.Hayvanlar.FirstOrDefault(x => x.HayvanId == id);
            if (hayvan == null)
            {
                return NotFound();
            }

            return View(hayvan);
        }
    }
}
