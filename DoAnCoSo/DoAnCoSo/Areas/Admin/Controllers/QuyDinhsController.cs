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
    public class QuyDinhsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IQuyDinhRepository _quyDinhRepository;

        public QuyDinhsController(ApplicationDbContext context, IQuyDinhRepository quyDinhRepository)
        {
            _context = context;
            _quyDinhRepository = quyDinhRepository;
        }

        // GET: Admin/QuyDinhs
        public async Task<IActionResult> Index()
        {
            return View(await _context.tbQuyDinh.ToListAsync());
        }

        // GET: Admin/QuyDinhs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbQuyDinh = await _context.tbQuyDinh
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tbQuyDinh == null)
            {
                return NotFound();
            }

            return View(tbQuyDinh);
        }

        // GET: Admin/QuyDinhs/Create
            public IActionResult Create()
            {
                return View();
            }

            // POST: Admin/QuyDinhs/Create
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create(tbQuyDinh quyDinh, IFormFile imageURL)
            {

            if (NameQuyDinhExists(quyDinh.TenQuyDinh))
            {
                TempData["ErrorMessage"] = "Đã tồn tại tên quy định";
                return View(quyDinh);
            }

            if (!String.IsNullOrEmpty(quyDinh.TenQuyDinh) && !String.IsNullOrEmpty(quyDinh.NoiDungQuyDinh) && imageURL != null)
                {
                quyDinh.imageURL = await SaveImage(imageURL);
                _context.tbQuyDinh.Add(quyDinh);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Đã thêm quy định thành công.";
                return View(quyDinh);
            }
            else
            {
                TempData["ErrorMessage"] = "Vui lòng nhập thông tin đầy đủ";
                return View(quyDinh);
            }
        }

            private async Task<string> SaveImage(IFormFile image)
            {
                var savePath = Path.Combine("wwwroot/images-QuyDinh", image.FileName); //

                using (var fileStream = new FileStream(savePath, FileMode.Create))
                {
                    await image.CopyToAsync(fileStream);
                }

                return "/images-QuyDinh/" + image.FileName;
            }

            


        // GET: Admin/QuyDinhs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbQuyDinh = await _context.tbQuyDinh.FindAsync(id);
            if (tbQuyDinh == null)
            {
                return NotFound();
            }
            return View(tbQuyDinh);
        }

        // POST: Admin/QuyDinhs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TenQuyDinh,NoiDungQuyDinh,imageURL")] tbQuyDinh tbQuyDinh, IFormFile imageUrl)
        {
            if (id != tbQuyDinh.Id)
            {
                return NotFound();
            }
            try
            {
                if (imageUrl != null)
                {
                    tbQuyDinh.imageURL = await SaveImage(imageUrl);
                }
                _context.tbQuyDinh.Update(tbQuyDinh);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbQuyDinhExists(tbQuyDinh.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            TempData["SuccessMessage"] = "sửa thông tin nội dung thành công.";
            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/QuyDinhs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbQuyDinh = await _context.tbQuyDinh
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tbQuyDinh == null)
            {
                return NotFound();
            }

            return View(tbQuyDinh);
        }

        // POST: Admin/QuyDinhs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tbQuyDinh = await _context.tbQuyDinh.FindAsync(id);
            if (tbQuyDinh != null)
            {
                _context.tbQuyDinh.Remove(tbQuyDinh);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> AnHien(int id)
        {
            var quyDinh = _context.tbQuyDinh.FirstOrDefault(l => l.Id == id);
            if (quyDinh == null)
                return NotFound();
            if (quyDinh.TrangThai != 0)
                quyDinh.TrangThai = 0;
            else
                quyDinh.TrangThai = 1;
            _context.tbQuyDinh.Update(quyDinh);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "QuyDinhs");
        }

        private bool tbQuyDinhExists(int id)
        {
            return _context.tbQuyDinh.Any(e => e.Id == id);
        }

        private bool NameQuyDinhExists(string name)
        {
            return _context.tbQuyDinh.Any(e => e.TenQuyDinh == name);
        }
    }
}
