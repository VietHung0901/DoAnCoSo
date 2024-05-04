using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DoAnCoSo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

namespace DoAnCoSo.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class NoiDungsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NoiDungsController(ApplicationDbContext context)
        {
            _context = context;
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
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NoiDungThi,TenNoiDung,imageUrl,TrangThai")] tbNoiDung tbNoiDung, IFormFile imageUrl)
        {
            if (TonTaiTenNoiDung(tbNoiDung.TenNoiDung))
            {
                TempData["ErrorMessage"] = "Đã tồn tại tên nội dung";
                return View(tbNoiDung);
            }

            if (!String.IsNullOrEmpty(tbNoiDung.TenNoiDung) && !String.IsNullOrEmpty(tbNoiDung.NoiDungThi) && imageUrl != null)
            {
                tbNoiDung.imageUrl = await SaveImage(imageUrl);
                tbNoiDung.TrangThai = 1;
                _context.tbNoiDung.Add(tbNoiDung);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Đã thêm nội dung thành công.";
                return View(tbNoiDung);
            }
            else
            {
                TempData["ErrorMessage"] = "Vui lòng nhập đầy đủ";
                return View(tbNoiDung);
            }
        }

        private async Task<string> SaveImage(IFormFile image)
        {
            var savePath = Path.Combine("wwwroot/images-NoiDung", image.FileName);

            using (var fileStream = new FileStream(savePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }
            return "/images-NoiDung/" + image.FileName; // Trả về đường dẫn tương đối
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
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NoiDungThi,TenNoiDung,imageUrl,TrangThai")] tbNoiDung tbNoiDung, IFormFile imageUrl)
        {
            if (id != tbNoiDung.Id)
            {
                return NotFound();
            }

            try
            {
                if (imageUrl != null)
                {
                    tbNoiDung.imageUrl = await SaveImage(imageUrl);
                }
                _context.tbNoiDung.Update(tbNoiDung);
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
            TempData["SuccessMessage"] = "sửa thông tin nội dung thành công.";
            return RedirectToAction(nameof(Index));
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

        public async Task<IActionResult> AnHien(int id)
        {
            var noiDung = _context.tbNoiDung.FirstOrDefault(l => l.Id == id);
            if (noiDung == null)
                return NotFound();
            if (noiDung.TrangThai != 0)
                noiDung.TrangThai = 0;
            else
                noiDung.TrangThai = 1;
            _context.tbNoiDung.Update(noiDung);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "NoiDungs");
        }

        private bool tbNoiDungExists(int id)
        {
            return _context.tbNoiDung.Any(e => e.Id == id);
        }

        private bool TonTaiTenNoiDung(string name)
        {
            return _context.tbNoiDung.Any(e => e.TenNoiDung == name);
        }
    }
}
