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
            var tbMonThis = await _monThiRepository.GetAllAsync();
            ViewBag.LoaiMonThiName = new SelectList(tbMonThis, "Id", "TenMonThi");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id, NgayThi, SoLuongThiSinh, DiaDiem, MonThiId")] tbCuocThi tbCuocThi)
        {
            var tbMonThis = await _monThiRepository.GetAllAsync();
            ViewBag.LoaiMonThiName = new SelectList(tbMonThis, "Id", "TenMonThi");

            // Lấy ngày giờ hiện tại


            // Kiểm tra nếu không nhập địa điểm thi hoặc không chọn ngày thi
            if (string.IsNullOrEmpty(tbCuocThi.DiaDiem))
            {
                TempData["ErrorMessage"] = "Vui lòng nhập địa điểm thi.";
                return View(tbCuocThi);
            }
            /*else if (tbCuocThi.NgayThi == null)
            {
                TempData["ErrorMessage"] = "Vui lòng nhập ngày thi.";
                return View(tbCuocThi);
            }
            else if (tbCuocThi.NgayThi.Date.Year > DateTime.Now.Year + 1)
            {
                TempData["ErrorMessage"] = "Ngày thi không được đặt quá năm sau so với năm hiện tại.";
                return View(tbCuocThi);
            }
            else if (tbCuocThi.NgayThi.Date.Year < DateTime.Now.Year)
            {
                TempData["ErrorMessage"] = "Ngày thi không được đặt trong quá khứ.";
                return View(tbCuocThi);
            }
            else if (tbCuocThi.NgayThi > DateTime.Now)
            {
                TempData["ErrorMessage"] = "Ngày thi không được đặt trong tương lai.";
                return View(tbCuocThi);
            }*/
            else if (tbCuocThi.SoLuongThiSinh == null || tbCuocThi.SoLuongThiSinh <= 50)
            {
                TempData["ErrorMessage"] = "Vui lòng nhập số lượng thí sinh hợp lệ.";
                return View(tbCuocThi);
            }

            _context.tbCuocThi.Add(tbCuocThi);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Đã thêm cuộc thi thành công.";

            return View(tbCuocThi);
        }

        // GET: Admin/CuocThis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbCuocThi = await _context.tbCuocThi.FindAsync(id);
            if (tbCuocThi == null)
            {
                return NotFound();
            }
            ViewData["MonThiId"] = new SelectList(_context.tbMonThi, "Id", "Id", tbCuocThi.MonThiId);
            return View(tbCuocThi);
        }

        // POST: Admin/CuocThis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NgayThi,SoLuongThiSinh,DiaDiem,MonThiId")] tbCuocThi tbCuocThi)
        {
            if (id != tbCuocThi.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbCuocThi);
                    await _context.SaveChangesAsync();
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
                return RedirectToAction(nameof(Index));
            }
            ViewData["MonThiId"] = new SelectList(_context.tbMonThi, "Id", "Id", tbCuocThi.MonThiId);
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

        private bool tbCuocThiExists(int id)
        {
            return _context.tbCuocThi.Any(e => e.Id == id);
        }

       /* private bool NameCuocThiExists(string name)
        {
            return _context.tbCuocThi.Any(e => e.t   == name);
        }*/
    }
}
