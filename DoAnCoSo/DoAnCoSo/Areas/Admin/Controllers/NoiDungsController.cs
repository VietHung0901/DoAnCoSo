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
using Microsoft.Extensions.Hosting.Internal;

namespace DoAnCoSo.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class NoiDungsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IQuyDinhRepository _quyDinhRepository;

        public NoiDungsController(ApplicationDbContext context, IQuyDinhRepository quyDinhRepository)
        {
            _context = context;
            _quyDinhRepository = quyDinhRepository;
        }

        // GET: Admin/NoiDungs
        public async Task<IActionResult> Index()
        {
            return View(await _context.tbNoiDung.ToListAsync());
        }

        // GET: Admin/NoiDungs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbNoiDung = await _context.tbNoiDung
                .FirstOrDefaultAsync(m => m.Id == id);

            if (tbNoiDung == null)
            {
                return NotFound();
            }

            return View(tbNoiDung);
        }

        // GET: Admin/NoiDungs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/NoiDungs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NoiDungThi,TenNoiDung,imageUrl")] tbNoiDung tbNoiDung, IFormFile imageUrL, IFormFile NoiDungThi)
        {
            if (string.IsNullOrEmpty(tbNoiDung.TenNoiDung) && string.IsNullOrEmpty(tbNoiDung.NoiDungThi))
            {
                TempData["ErrorMessage"] = "Vui lòng nhập thông tin đầy đủ";
                return View(tbNoiDung);
            }
            if (!NameNoiDungThiExists(tbNoiDung.TenNoiDung))
            {

                if (imageUrL != null)
                {
                    // Lưu hình ảnh đại diện
                    tbNoiDung.imageUrl = await SaveImage(imageUrL);
                    /*tbNoiDung.NoiDungThi = await SaveFile(NoiDungThi);*/
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Đã tồn tại nội dung, vui lòng nhập tên khác";
            }
            _context.tbNoiDung.Add(tbNoiDung);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Đã thêm nội dung thành công.";
            return View(tbNoiDung);
        }

        private async Task<string> SaveImage(IFormFile image)
        {
            var savePath = Path.Combine("wwwroot/images", image.FileName);

            using (var fileStream = new FileStream(savePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }

            return "/images/" + image.FileName;
        }

        /*private async Task<string> SaveFile(IFormFile noiDung)
        {
            var savePath = Path.Combine("wwwroot/upload", noiDung.FileName);

            using (var fileStream = new FileStream(savePath, FileMode.Create))
            {
                await noiDung.CopyToAsync(fileStream);
            }

            return "/upload/" + noiDung.FileName;
        }
        phần này chua thuc hien xong, dung quan tam toi thu muc upload nha*/
        private bool NameNoiDungThiExists(string name)
        {
            return _context.tbNoiDung.Any(e => e.TenNoiDung == name);
        }

        // GET: Admin/NoiDungs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbNoiDung = await _context.tbNoiDung.FindAsync(id);
            if (tbNoiDung == null)
            {
                return NotFound();
            }
            return View(tbNoiDung);
        }

        // POST: Admin/NoiDungs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NoiDungThi,TenNoiDung,imageUrl")] tbNoiDung tbNoiDung)
        {
            if (id != tbNoiDung.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbNoiDung);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!tbNoiDungExists(tbNoiDung.Id))
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
            return View(tbNoiDung);
        }

        // GET: Admin/NoiDungs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbNoiDung = await _context.tbNoiDung
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tbNoiDung == null)
            {
                return NotFound();
            }

            return View(tbNoiDung);
        }

        // POST: Admin/NoiDungs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tbNoiDung = await _context.tbNoiDung.FindAsync(id);
            if (tbNoiDung != null)
            {
                _context.tbNoiDung.Remove(tbNoiDung);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool tbNoiDungExists(int id)
        {
            return _context.tbNoiDung.Any(e => e.Id == id);
        }
    }
}
