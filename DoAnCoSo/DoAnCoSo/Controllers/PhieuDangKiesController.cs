using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DoAnCoSo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace DoAnCoSo.Controllers
{
    public class PhieuDangKiesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public PhieuDangKiesController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _context = context;
            _userManager = userManager;
        }

        //Xuất các phiếu đăng ký theo UserId
        public async Task<IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("https://localhost:7107/Identity/Account/Login"); // Chuyển hướng đến trang đăng nhập
            }
            else
            {
                var user = await _userManager.GetUserAsync(User);
                var applicationDbContext = _context.tbPhieuDangKy.Where(p => p.UserId == user.Id)
                                                            .Include(t => t.CuocThi)
                                                            .Include(t => t.User);
                return View(await applicationDbContext.ToListAsync());
            }
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbPhieuDangKy = await _context.tbPhieuDangKy
                .Include(t => t.CuocThi)
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tbPhieuDangKy == null)
            {
                return NotFound();
            }

            return View(tbPhieuDangKy);
        }

        // GET: Admin/PhieuDangKies/Create

        public async Task<IActionResult> Create(int cuocThiId)
        {
            ViewBag.CuocThiId = cuocThiId;

            var user = await _userManager.GetUserAsync(User);
            ViewBag.UserHoTen = user.HoTen;
            ViewBag.UserCCCD = user.CCCD;
            ViewBag.UserImage = user.ImageUrl;
            ViewBag.UserSDT = user.SoDienThoai;
            ViewBag.UserDiaChi = user.DiaChi;
            ViewBag.UserGioTinh = user.GioiTinh;
            ViewBag.UserTruong = user.TruongId;
            ViewBag.UserEmail = user.Email;
            ViewBag.UserId = user.Id;
            return View();
        }

        // POST: Admin/PhieuDangKies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NgayDangKy,CuocThiId,UserId")] tbPhieuDangKy tbPhieuDangKy)
        {

            //Kiểm tra xem cuộc thi đã đủ thí sinh chưa
            if (KiemTraSoLuong(tbPhieuDangKy.CuocThiId))
            {
                var user = await _userManager.GetUserAsync(User);
                tbPhieuDangKy.NgayDangKy = DateTime.Now;
                _context.tbPhieuDangKy.Add(tbPhieuDangKy);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { userId = user.Id });
            }
            ViewBag.CuocThiId = tbPhieuDangKy.CuocThiId;
            ViewBag.UserId = tbPhieuDangKy.UserId;
            return View(tbPhieuDangKy);
        }

        private bool tbPhieuDangKyExists(int id)
        {
            return _context.tbPhieuDangKy.Any(e => e.Id == id);
        }

        private bool KiemTraSoLuong(int cuocThiId)
        {
            var result = from c in _context.tbPhieuDangKy
                         where c.CuocThiId == cuocThiId
                         select c;
            int dem = result.Count();
            var cuocThi = _context.tbCuocThi.FirstOrDefault(e => e.Id == cuocThiId);
            return (dem < cuocThi.SoLuongThiSinh) ? true : false ;
        }
    }
}
