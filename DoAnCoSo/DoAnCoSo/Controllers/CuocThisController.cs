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
using Microsoft.AspNetCore.Identity;

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

        //public async Task<IActionResult> Index()
        //{
        //    var applicationDbContext = _context.tbCuocThi.Include(t => t.MonThi);
        //    return View(await applicationDbContext.ToListAsync());
        //}

        //Action tìm kiếm cuộc thi theo ngày, địa điểm, môn thi
        public async Task<IActionResult> Index(DateTime? start_date, DateTime? end_date,string? diaDiem, int? monthiId )
        {
            var result = from c in _context.tbCuocThi
                         select c;

            if (start_date != null || end_date != null || !string.IsNullOrEmpty(diaDiem) || monthiId != null)
            {
                if (start_date != null && end_date != null && start_date > end_date)
                {
                    TempData["ErrorMessage"] = "Thời gian không hợp lệ!";
                    return RedirectToAction("Index", "CuocThis");
                }

                result = result.Where(c =>
                    ((start_date != null && c.NgayThi >= start_date) &&
                    (end_date != null && c.NgayThi <= end_date)) &&
                    (!string.IsNullOrEmpty(diaDiem) && c.DiaDiem.Contains(diaDiem)) &&
                    (monthiId != null && c.MonThiId == monthiId)
                );
            }

            List<tbCuocThi> cuocThiList = await result.ToListAsync();
            return View(cuocThiList);
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
