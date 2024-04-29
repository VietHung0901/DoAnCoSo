using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DoAnCoSo.Models;
using DoAnCoSo.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

namespace DoAnCoSo.Controllers
{
    public class CuocThisController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICuocThiRepository _cuocThiRepository;
        private readonly IMonThiRepository _monThiRepository;


        public CuocThisController(ApplicationDbContext context, ICuocThiRepository cuocThiRepository, IMonThiRepository monThiRepository)
        {
            _context = context;
            _cuocThiRepository = cuocThiRepository;
            _monThiRepository = monThiRepository;
        }

        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.tbCuocThi.Include(t => t.MonThi);
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbCuocThi = await _context.tbCuocThi
                .Include(t => t.MonThi)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tbCuocThi == null)
            {
                return NotFound();
            }

            return View(tbCuocThi);
        }

        private bool tbCuocThiExists(int id)
        {
            return _context.tbCuocThi.Any(e => e.Id == id);
        }
        private int KiemTraSoLuong(int cuocThiId)
        {
            var result = from c in _context.tbPhieuDangKy
                         where c.CuocThiId == cuocThiId
                         select c;
            return result.Count();
        }
    }
}
