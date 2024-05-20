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

            var tbPhieuDangKy = await _context.tbPhieuDangKy.FindAsync(id);
            if (tbPhieuDangKy == null)
            {
                return NotFound();
            }
            ViewData["CuocThiId"] = new SelectList(_context.tbCuocThi, "Id", "Id", tbPhieuDangKy.CuocThiId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", tbPhieuDangKy.UserId);
            return View(tbPhieuDangKy);
        }

        // POST: Admin/PhieuDangKies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NgayDangKy,CuocThiId,UserId")] tbPhieuDangKy tbPhieuDangKy)
        {
            if (id != tbPhieuDangKy.Id)
            {
                return NotFound();
            }

            //Cần sửa điều kiện này-----------------------
            if (KiemTraSoLuong(tbPhieuDangKy.CuocThiId))
            {
                try
                {
                    _context.tbPhieuDangKy.Update(tbPhieuDangKy);
                    await _context.SaveChangesAsync();
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
                return RedirectToAction(nameof(Index), new { cuocThiId = tbPhieuDangKy.CuocThiId });
            }
            ViewData["CuocThiId"] = new SelectList(_context.tbCuocThi, "Id", "Id", tbPhieuDangKy.CuocThiId);
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

        public async Task<IActionResult> ThongKe()
        {
            var thongKe = new tbThongKe
            {
                SoLuongUser = await _userManager.Users.CountAsync(),
                SoLuongCuocThi = await _context.tbCuocThi.CountAsync(),

                SoLuongUserDK = await _context.tbPhieuDangKy.Select(p => p.UserId).Distinct().CountAsync(),
                SoLuongUserMoi = await _userManager.Users.CountAsync(u => !u.HasBeenViewed)
            };
            return View(thongKe);
        }
    }
}
