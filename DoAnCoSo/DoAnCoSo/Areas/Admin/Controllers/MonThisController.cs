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
    public class MonThisController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MonThisController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/MonThis
        public async Task<IActionResult> Index()
        {
            return View(await _context.tbMonThi.ToListAsync());
        }

        // GET: Admin/MonThis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbMonThi = await _context.tbMonThi
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tbMonThi == null)
            {
                return NotFound();
            }

            return View(tbMonThi);
        }

        // GET: Admin/MonThis/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/MonThis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(tbMonThi monThi)
        {
            if (string.IsNullOrEmpty(monThi.TenMonThi))
            {
                TempData["ErrorMessage"] = "Vui lòng nhập đầy đủ";
                return View(monThi);
            }
            if (!NameMonThiExists(monThi.TenMonThi))
            {
                _context.tbMonThi.Add(monThi);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Đã thêm mon thi thành công.";
            }
            else
            {
                TempData["ErrorMessage"] = "Đã tồn tại tên mon thi, vui lòng nhập tên khác";
            }
            //Chưa xử lý được việc xuất thông báo "Tên loại trường đã tồn tại"
            return View(monThi);
        }

       

        // GET: Admin/MonThis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbMonThi = await _context.tbMonThi.FindAsync(id);
            if (tbMonThi == null)
            {
                return NotFound();
            }
            return View(tbMonThi);
        }

        // POST: Admin/MonThis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TenMonThi")] tbMonThi monThi)
        {
            if (id != monThi.Id)
            {
                return NotFound();
            }

            if (string.IsNullOrEmpty(monThi.TenMonThi))
            {
                TempData["ErrorMessage"] = "Vui lòng nhập đầy đủ thông tin.";
                return View(monThi);
            }

            if (!NameMonThiExists(monThi.TenMonThi))
            {
                try
                {
                    _context.tbMonThi.Update(monThi);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Dữ liệu đã được cập nhật thành công.";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!tbMonThiExists(monThi.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            TempData["ErrorMessage"] = "Tên trùng vui lòng chọn tên khác.";
            return View(monThi);
        }

        // GET: Admin/MonThis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbMonThi = await _context.tbMonThi
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tbMonThi == null)
            {
                return NotFound();
            }

            return View(tbMonThi);
        }

        // POST: Admin/MonThis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tbMonThi = await _context.tbMonThi.FindAsync(id);
            if (tbMonThi != null)
            {
                _context.tbMonThi.Remove(tbMonThi);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NameMonThiExists(string name)
        {
            return _context.tbMonThi.Any(e => e.TenMonThi == name);
        }
        private bool tbMonThiExists(int id)
        {
            return _context.tbMonThi.Any(e => e.Id == id);
        }

    }
}
