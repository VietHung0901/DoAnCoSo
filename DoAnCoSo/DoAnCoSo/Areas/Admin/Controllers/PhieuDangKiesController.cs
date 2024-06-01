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
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace DoAnCoSo.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class PhieuDangKiesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public PhieuDangKiesController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _context = context;
            _userManager = userManager;
        }

        //Xuất các phiếu đăng ký theo cuộc thi---------------------
        public async Task<IActionResult> Index(int cuocThiId)
        {
            var applicationDbContext = _context.tbPhieuDangKy.Where(p => p.CuocThiId == cuocThiId)
                                                            .Include(t => t.CuocThi)
                                                            .Include(t => t.User);
            ViewBag.CuocThiId = cuocThiId;
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/PhieuDangKies/Details/5
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

        // GET: Admin/PhieuDangKies/Edit/5
        public async Task<IActionResult> Edit(int? id)
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
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", tbPhieuDangKy.UserId);
            return View(tbPhieuDangKy);
        }

        // POST: Admin/PhieuDangKies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NgayDangKy,CuocThiId,UserId,Email,SoDienThoai,TruongId")] tbPhieuDangKy tbPhieuDangKy)
        {
            if (id != tbPhieuDangKy.Id)
            {
                return NotFound();
            }

            try
            {
                _context.tbPhieuDangKy.Update(tbPhieuDangKy);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), new { Id = tbPhieuDangKy.Id });
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbPhieuDangKyExists(tbPhieuDangKy.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", tbPhieuDangKy.UserId);
            return View(tbPhieuDangKy);
        }

        string laytenUser(string userId)
        {
            var user = _context.Users.FirstOrDefault(p => p.Id == userId);
            if (user == null)
                return "";
            return user.HoTen;

        }

        string laytenTruong(int? truongId)
        {
            var truong = _context.tbTruong.FirstOrDefault(p => p.Id == truongId);
            if (truong == null)
                return "";
            return truong.TenTruong;

        }
        //Hàm kiểm tra số điện thoại
        [HttpGet]
        public IActionResult CheckPhone(string phoneNumber, int competitionId)
        {
            // Truy vấn cơ sở dữ liệu và xử lý logic...
            var registration = _context.tbPhieuDangKy.FirstOrDefault(r => r.SoDienThoai == phoneNumber && r.CuocThiId == competitionId);
            return Json(new
            {
                userName = laytenUser(registration.UserId),
                soDienThoai = registration.SoDienThoai,
                email = registration.Email,
                truongName = laytenTruong(registration.TruongId),
                mapdk = registration.Id
            });
        }
        // GET: Admin/PhieuDangKies/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        // POST: Admin/PhieuDangKies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tbPhieuDangKy = await _context.tbPhieuDangKy.FindAsync(id);
            if (tbPhieuDangKy != null)
            {
                _context.tbPhieuDangKy.Remove(tbPhieuDangKy);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { cuocThiId = tbPhieuDangKy.CuocThiId });
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
            return (dem < cuocThi.SoLuongThiSinh) ? true : false;
        }

        public IActionResult ViewChart()
        {
            var usersWithCity = _context.Users
                .Where(u => !string.IsNullOrEmpty(u.DiaChi))
                .AsEnumerable() // Chuyển sang kiểu IEnumerable để thực hiện thao tác trong bộ nhớ
                .GroupBy(u => GetLastCityName(u.DiaChi))
                .Select(g => new { City = g.Key, Count = g.Count() })
                .ToList();

            ViewBag.Labels = usersWithCity.Select(u => u.City).ToList();
            ViewBag.Data = usersWithCity.Select(u => u.Count).ToList();

            return View();
        }

        // Phương thức để lấy tên thành phố cuối cùng từ địa chỉ
        private string GetLastCityName(string address)
        {
            // Tách địa chỉ thành các phần bằng dấu phẩy
            var parts = address.Split(',', StringSplitOptions.RemoveEmptyEntries);

            // Lấy thành phố từ phần cuối cùng của địa chỉ
            var lastPart = parts.LastOrDefault()?.Trim();

            // Trả về tên thành phố cuối cùng
            return lastPart;
        }
    }
}
