using DostPatiler.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DostPatiler.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private UserManager<IdentityUser> userManager;
        private RoleManager<IdentityRole> roleManager;
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context, RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this._context = context;
        }

        public IActionResult Roles()
        {
            var roles = roleManager.Roles.ToList();
            return View(roles);
        }

        public IActionResult Create()
        {
            return View(new IdentityRole());
        }

        [HttpPost]
        public async Task<IActionResult> Create(IdentityRole role)
        {
            await roleManager.CreateAsync(role);
            return RedirectToAction("Roles");
        }

        public IActionResult UserList()
        {
            var users = userManager.Users.ToList();
            return View(users);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SahiplendirmeOnay(int id)
        {
            var hayvan = await _context.Hayvanlar.FindAsync(id);
            hayvan.Durum = Surec.Onaylandı;
            hayvan.Sahiplendirme = false;
            hayvan.UserId = string.Empty;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(SahiplendirmeBasvurulari));
        }
        public IActionResult SahiplendirmeBasvurulari()
        {
            return View(_context.Hayvanlar.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SahiplenmeOnay(int id)
        {
            var hayvan = await _context.Hayvanlar.FindAsync(id);
            hayvan.Durum = Surec.Onaylandı;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(SahiplenmeBasvurulari));
        }
        public IActionResult SahiplenmeBasvurulari()
        {
            return View(_context.Hayvanlar.ToList());
        }
    }
}
