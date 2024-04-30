using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DoAnCoSo.Models;
using Microsoft.AspNetCore.Authorization;
using DoAnCoSo.Repositories;

namespace DoAnCoSo.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class LoaiTruongsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LoaiTruongsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/LoaiTruongs
        public async Task<IActionResult> Index()
        {
            return View(await _context.tbLoaiTruong.ToListAsync());
        }

        // GET: Admin/LoaiTruongs/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(tbLoaiTruong LoaiTruong)
        {
            if (string.IsNullOrEmpty(LoaiTruong.TenLoaiTruong))
            {
                TempData["ErrorMessage"] = "Vui lòng nhập đầy đủ";
                return View(LoaiTruong);
            }

            //Kiểm tra xem tên trường đã tồn tại hay chưa
            if (!NameLoaiTruongExists(LoaiTruong.TenLoaiTruong))
            {
                _context.tbLoaiTruong.Add(LoaiTruong);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Đã thêm loại trường thành công."; 
            }
            else
            {
                TempData["ErrorMessage"] = "Đã tồn tại tên loại trường, vui lòng nhập tên khác";
            }
            //Chưa xử lý được việc xuất thông báo "Tên loại trường đã tồn tại"
            return View(LoaiTruong);
        }

        // GET: Admin/LoaiTruongs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbLoaiTruong = await _context.tbLoaiTruong.FindAsync(id);
            if (tbLoaiTruong == null)
            {
                return NotFound();
            }
            return View(tbLoaiTruong);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TenLoaiTruong")] tbLoaiTruong LoaiTruong)
        {
            if (id != LoaiTruong.Id)
            {
                return NotFound();
            }

            if (string.IsNullOrEmpty(LoaiTruong.TenLoaiTruong))
            {
                TempData["ErrorMessage"] = "Vui lòng nhập đầy đủ thông tin.";
                return View(LoaiTruong);
            }

            if (!NameLoaiTruongExists(LoaiTruong.TenLoaiTruong))
            {
                try
                {
                    _context.tbLoaiTruong.Update(LoaiTruong);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Dữ liệu đã được cập nhật thành công.";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!tbLoaiTruongExists(LoaiTruong.Id))
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
                TempData["ErrorMessage"] = "Tên trùng vui lòng chọn tên khác.";
            }
            return View(LoaiTruong);
        }


        // GET: Admin/LoaiTruongs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbLoaiTruong = await _context.tbLoaiTruong.FirstOrDefaultAsync(m => m.Id == id);
            if (tbLoaiTruong == null)
            {
                return NotFound();
            }

            return View(tbLoaiTruong);
        }

        // POST: Admin/LoaiTruongs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, tbLoaiTruong LoaiTruong)
        {
            //Kiểm tra id của loại trường này đã được gán cho trường nào chưa
            if (!tbTruongExists(id))
            {
                var tbLoaiTruong = await _context.tbLoaiTruong.FindAsync(id);
                if (tbLoaiTruong != null)
                {
                    _context.tbLoaiTruong.Remove(tbLoaiTruong);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Loại trường đã được xóa thành công!";
                }
                else
                {
                    TempData["ErrorMessage"] = "Không thể xóa được loại trường này!";
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Không thể xóa được loại trường này!";
            }
            return View(LoaiTruong);
        }

        private bool tbLoaiTruongExists(int id)
        {
            return _context.tbLoaiTruong.Any(e => e.Id == id);
        }

        private bool NameLoaiTruongExists(string name)
        {
            return _context.tbLoaiTruong.Any(e => e.TenLoaiTruong == name);
        }

        //Hàm dùng để kiểm tra tbLoaiTruong.Id này đã được gán cho tbTruong nào chưa
        private bool tbTruongExists(int Id)
        {
            return _context.tbTruong.Any(e => e.LoaiTruongId == Id);
        }
    }
}
