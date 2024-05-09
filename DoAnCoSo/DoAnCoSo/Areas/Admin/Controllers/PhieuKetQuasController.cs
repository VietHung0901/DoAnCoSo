using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DoAnCoSo.Models;
using Microsoft.AspNetCore.Authorization;

namespace DoAnCoSo.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class PhieuKetQuasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PhieuKetQuasController(ApplicationDbContext context)
        {
            _context = context;
        }


        //Hàm lưu điểm
        [HttpPost]
        public IActionResult SaveDiem(int PhieuDangKyId, int Phut, int Giay, int Diem)
        {
            try
            {
                // Xử lý lưu dữ liệu điểm vào cơ sở dữ liệu
                tbPhieuKetQua pkq = new tbPhieuKetQua
                {
                    PhieuDangKyId = PhieuDangKyId,
                    Phut = Phut,
                    Giay = Giay,
                    Diem = Diem,
                    TrangThai = 1,
                };

                _context.tbPhieuKetQua.Add(pkq);
                _context.SaveChanges();

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }


        //Hàm sửa điểm
        [HttpPost]
        public IActionResult UpdateDiem(int phieuDangKyId, int Phut, int Giay, int Diem)
        {
            try
            {
                var pkq = _context.tbPhieuKetQua.FirstOrDefault(p => p.PhieuDangKyId == phieuDangKyId);

                pkq.Phut = Phut;
                pkq.Giay = Giay;
                pkq.Diem = Diem;

                _context.tbPhieuKetQua.Update(pkq);
                _context.SaveChanges();

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult LayDiem(int phieuDangKyId)
        {
            // Truy vấn cơ sở dữ liệu hoặc thực hiện các thao tác để lấy thông tin điểm dựa vào phieuDangKyId
            var pkq = _context.tbPhieuKetQua.FirstOrDefault(p => p.PhieuDangKyId == phieuDangKyId);

            // Trả về thông tin điểm dưới dạng JSON
            return Json(new { diem = pkq.Diem, phut = pkq.Phut, giay = pkq.Giay });
            //return Content($"{pkq.Phut},{pkq.Giay}, {pkq.Diem}");
        }

        //Xuất danh sách các phiếu kết quả theo cuộc thi
        public async Task<IActionResult> Index(int cuocThiId)
        {
            var applicationDbContext = _context.tbPhieuKetQua
                .Where(p => p.PhieuDangKy.CuocThiId == cuocThiId)
                .Include(t => t.PhieuDangKy)
                .OrderByDescending(p => p.Diem) // Sắp xếp giảm dần theo trường Diem
                .ThenBy(p => p.Phut) // Sắp xếp tăng dần theo trường Phut (nếu điểm bằng nhau)
                .ThenBy(p => p.Giay); // Sắp xếp tăng dần theo trường Giay (nếu điểm và phút bằng nhau)

            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/PhieuKetQuas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbPhieuKetQua = await _context.tbPhieuKetQua
                .Include(t => t.PhieuDangKy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tbPhieuKetQua == null)
            {
                return NotFound();
            }

            return View(tbPhieuKetQua);
        }

        // GET: Admin/PhieuKetQuas/Create
        public IActionResult Create()
        {
            ViewData["PhieuDangKyId"] = new SelectList(_context.tbPhieuDangKy, "Id", "Id");
            return View();
        }

        // POST: Admin/PhieuKetQuas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PhieuDangKyId,Phut,Giay,Diem,TrangThai")] tbPhieuKetQua tbPhieuKetQua)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbPhieuKetQua);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PhieuDangKyId"] = new SelectList(_context.tbPhieuDangKy, "Id", "Id", tbPhieuKetQua.PhieuDangKyId);
            return View(tbPhieuKetQua);
        }

        // GET: Admin/PhieuKetQuas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbPhieuKetQua = await _context.tbPhieuKetQua.FindAsync(id);
            if (tbPhieuKetQua == null)
            {
                return NotFound();
            }
            ViewData["PhieuDangKyId"] = new SelectList(_context.tbPhieuDangKy, "Id", "Id", tbPhieuKetQua.PhieuDangKyId);
            return View(tbPhieuKetQua);
        }

        // POST: Admin/PhieuKetQuas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PhieuDangKyId,Phut,Giay,Diem,TrangThai")] tbPhieuKetQua tbPhieuKetQua)
        {
            if (id != tbPhieuKetQua.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbPhieuKetQua);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!tbPhieuKetQuaExists(tbPhieuKetQua.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["PhieuDangKyId"] = new SelectList(_context.tbPhieuDangKy, "Id", "Id", tbPhieuKetQua.PhieuDangKyId);
            return View(tbPhieuKetQua);
        }

        // GET: Admin/PhieuKetQuas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbPhieuKetQua = await _context.tbPhieuKetQua
                .Include(t => t.PhieuDangKy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tbPhieuKetQua == null)
            {
                return NotFound();
            }

            return View(tbPhieuKetQua);
        }

        // POST: Admin/PhieuKetQuas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tbPhieuKetQua = await _context.tbPhieuKetQua.FindAsync(id);
            if (tbPhieuKetQua != null)
            {
                _context.tbPhieuKetQua.Remove(tbPhieuKetQua);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool tbPhieuKetQuaExists(int id)
        {
            return _context.tbPhieuKetQua.Any(e => e.Id == id);
        }
    }
}
