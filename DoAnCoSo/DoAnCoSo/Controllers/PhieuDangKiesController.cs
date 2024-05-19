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
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("https://localhost:7107/Identity/Account/Login"); // Chuyển hướng đến trang đăng nhập
            }
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
            var user = await _userManager.GetUserAsync(User);

            if (LayMaLoaiTruongTheoCuocThi(tbPhieuDangKy.CuocThiId) != LayMaLoaiTruongTheoTruong(user.TruongId))
                TempData["ErrorMessage"] = "Cuộc thi không dành cho cấp học của bạn";
            else
            {
                //Kiểm tra xem cuộc thi đã đủ thí sinh vaf user đã đăng ký cuộc thi này chưa
                if (KiemTraSoLuong(tbPhieuDangKy.CuocThiId))
                {
                    if (KiemTraUser(tbPhieuDangKy.CuocThiId, tbPhieuDangKy.UserId))
                    {
                        var user1 = await _userManager.GetUserAsync(User);
                        tbPhieuDangKy.NgayDangKy = DateTime.Now;
                        _context.tbPhieuDangKy.Add(tbPhieuDangKy);
                        await _context.SaveChangesAsync();
                        TempData["SuccessMessage"] = "Đăng ký thành công.";
                        return RedirectToAction(nameof(Index), new { userId = user1.Id });
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Bạn đã đăng ký cuộc thi này";
                    }
                }
                else
                {
                    TempData["ErrorMessage"] = "Số lượng thí sinh cho cuộc thi đã đủ";
                }
            }
            
            ViewBag.CuocThiId = tbPhieuDangKy.CuocThiId;
            ViewBag.UserId = tbPhieuDangKy.UserId;
            ViewBag.UserHoTen = user.HoTen;
            ViewBag.UserCCCD = user.CCCD;
            ViewBag.UserImage = user.ImageUrl;
            ViewBag.UserSDT = user.SoDienThoai;
            ViewBag.UserDiaChi = user.DiaChi;
            ViewBag.UserGioTinh = user.GioiTinh;
            ViewBag.UserTruong = user.TruongId;
            ViewBag.UserEmail = user.Email;
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
            return dem < cuocThi.SoLuongThiSinh;
        }

        private bool KiemTraUser(int cuocThiId, string userId)
        {
            var result = from c in _context.tbPhieuDangKy
                         where c.CuocThiId == cuocThiId && c.UserId == userId
                         select c;
            if (result.Count() == 0)
                return true;
            return false;
        }

        private int? LayMaLoaiTruongTheoCuocThi(int cuocThiId)
        {
            var CuocThi = _context.tbCuocThi.FirstOrDefault(p => p.Id == cuocThiId);
            if (CuocThi == null) return 0;
            return CuocThi.LoaiTruongId;
        }

        private int? LayMaLoaiTruongTheoTruong(int truongId)
        {
            var truong = _context.tbTruong.FirstOrDefault(p => p.Id == truongId);
            if (truong == null) return 0;
            return truong.LoaiTruongId;
        }
    }
}
