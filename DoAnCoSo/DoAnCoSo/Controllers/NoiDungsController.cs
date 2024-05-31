using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DoAnCoSo.Models;

namespace DoAnCoSo.Controllers
{
    public class NoiDungsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NoiDungsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: NoiDungs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbNoiDung = await _context.tbNoiDung
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tbNoiDung == null)
            {
                return NotFound();
            }

            return View(tbNoiDung);
        }
    }
}
