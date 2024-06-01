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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DoAnCoSo.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
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

        // GET: Admin/CuocThis
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.tbCuocThi.Include(t => t.MonThi);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/CuocThis/Details/5
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

        // GET: Admin/CuocThis/Create
        public async Task<IActionResult> Create()
        {
            var tbMonThis = from c in _context.tbMonThi
                            where c.TrangThai != 0
                            select c;
            ViewBag.MonThiName = new SelectList(tbMonThis, "Id", "TenMonThi");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id, TenCuocThi, NgayThi, SoLuongThiSinh, DiaDiem, MonThiId, LoaiTruongId")] tbCuocThi tbCuocThi, List<int> selectedQuyDinhs, List<int> selectedNoiDungs)
        {
            var tbMonThis = from c in _context.tbMonThi
                               where c.TrangThai != 0
                            select c;
            ViewBag.MonThiName = new SelectList(tbMonThis, "Id", "TenMonThi");

            // Kiểm tra nếu không nhập địa điểm thi hoặc không chọn ngày thi
            if (string.IsNullOrEmpty(tbCuocThi.TenCuocThi) || string.IsNullOrEmpty(tbCuocThi.DiaDiem) || tbCuocThi.NgayThi == DateTime.MinValue)
            {
                TempData["ErrorMessage"] = "Thông tin không hợp lệ!";
                return View(tbCuocThi);
            }
    
            if (tbCuocThi.SoLuongThiSinh == null || tbCuocThi.SoLuongThiSinh <= 50)
            {
                TempData["ErrorMessage"] = "Vui lòng nhập số lượng thí sinh hợp lệ!";
                return View(tbCuocThi);
            }

            if(selectedQuyDinhs == null)
            {
                TempData["ErrorMessage"] = "Vui lòng chọn quy định thi!";
                return View(tbCuocThi);
            }

            if (selectedNoiDungs == null)
            {
                TempData["ErrorMessage"] = "Vui lòng chọn nội dung thi!";
                return View(tbCuocThi);
            }

            tbCuocThi.TrangThai = 1;
            _context.tbCuocThi.Add(tbCuocThi);
            await _context.SaveChangesAsync();

            //Thêm quy định vào cuộc thi
            foreach (var quyDinhId in selectedQuyDinhs)
            {
                tbChiTietQuyDinh ctqd = new tbChiTietQuyDinh
                {
                    CuocThiId = tbCuocThi.Id,
                    QuyDinhId = quyDinhId,
                };
                _context.tbChiTietQuyDinh.Add(ctqd);
                await _context.SaveChangesAsync();
            }

            foreach (var noiDungId in selectedNoiDungs)
            {
                tbChiTietNoiDung ctnd = new tbChiTietNoiDung
                {
                    CuocThiId = tbCuocThi.Id,
                    NoiDungId = noiDungId,
                };
                _context.tbChiTietNoiDung.Add(ctnd);
                await _context.SaveChangesAsync();
            }
            
            TempData["SuccessMessage"] = "Đã thêm cuộc thi thành công!";
            return View(tbCuocThi);
        }

        // GET: Admin/CuocThis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var tbMonThis = from c in _context.tbMonThi
                            where c.TrangThai != 0
                            select c;
            ViewBag.MonThiName = new SelectList(tbMonThis, "Id", "TenMonThi");
            if (id == null)
            {
                return NotFound();
            }

            var tbCuocThi = await _context.tbCuocThi.FindAsync(id);
            if (tbCuocThi == null)
            {
                return NotFound();
            }
            
            return View(tbCuocThi);
        }

        // POST: Admin/CuocThis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, TenCuocThi, NgayThi,SoLuongThiSinh,DiaDiem,MonThiId, LoaiTruongId")] tbCuocThi tbCuocThi)
        {
            var tbMonThis = from c in _context.tbMonThi
                            where c.TrangThai != 0
                            select c;
            ViewBag.MonThiName = new SelectList(tbMonThis, "Id", "TenMonThi");
            if (id != tbCuocThi.Id)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(tbCuocThi.TenCuocThi) && !string.IsNullOrEmpty(tbCuocThi.DiaDiem) && tbCuocThi.SoLuongThiSinh != null && tbCuocThi.SoLuongThiSinh > 50)
            {
                try
                {
                    _context.tbCuocThi.Update(tbCuocThi);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Cập nhật cuộc thi thành công.";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!tbCuocThiExists(tbCuocThi.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Dữ liệu không hợp lệ.";
                return View(tbCuocThi);
            }
            return View(tbCuocThi);
        }

        // GET: Admin/CuocThis/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        // POST: Admin/CuocThis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tbCuocThi = await _context.tbCuocThi.FindAsync(id);
            if (tbCuocThi != null)
            {
                _context.tbCuocThi.Remove(tbCuocThi);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> AnHien(int id)
        {
            var cuocThi = _context.tbCuocThi.FirstOrDefault(l => l.Id == id);
            if (cuocThi == null)
                return NotFound();
            if (cuocThi.TrangThai != 0)
                cuocThi.TrangThai = 0;
            else
                cuocThi.TrangThai = 1;
            _context.tbCuocThi.Update(cuocThi);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "CuocThis");
        }

        private bool tbCuocThiExists(int id)
        {
            return _context.tbCuocThi.Any(e => e.Id == id);
        }
    }
}
