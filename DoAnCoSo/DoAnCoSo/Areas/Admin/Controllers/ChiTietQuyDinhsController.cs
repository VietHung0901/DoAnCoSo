using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DoAnCoSo.Models;
using DoAnCoSo.Repositories;

namespace DoAnCoSo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ChiTietQuyDinhsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IChiTietQuyDinhRepository _chiTietQuyDinhRepository;
        private readonly IQuyDinhRepository _quyDinhRepository;
        private readonly ICuocThiRepository _cuocThiRepository;

        public ChiTietQuyDinhsController(ApplicationDbContext context, IChiTietQuyDinhRepository chiTietQuyDinhRepository, IQuyDinhRepository quyDinhRepository, ICuocThiRepository cuocThiRepository)
        {
            _context = context;
            _chiTietQuyDinhRepository = chiTietQuyDinhRepository;
            _quyDinhRepository = quyDinhRepository;
            _cuocThiRepository = cuocThiRepository;
        }

        // GET: Admin/ChiTietQuyDinhs
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.tbChiTietQuyDinh.Include(t => t.CuocThi).Include(t => t.QuyDinh);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/ChiTietQuyDinhs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbChiTietQuyDinh = await _context.tbChiTietQuyDinh
                .Include(t => t.CuocThi)
                .Include(t => t.QuyDinh)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tbChiTietQuyDinh == null)
            {
                return NotFound();
            }

            return View(tbChiTietQuyDinh);
        }

        // GET: Admin/ChiTietQuyDinhs/Create
        public IActionResult Create()
        {
            ViewData["CuocThiId"] = new SelectList(_context.tbCuocThi, "Id", "TenCuocThi");
            ViewData["QuyDinhId"] = new SelectList(_context.tbQuyDinh, "Id", "TenQuyDinh");
            return View();
        }

        // POST: Admin/ChiTietQuyDinhs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CuocThiId,QuyDinhId")] tbChiTietQuyDinh tbChiTietQuyDinh)
        {


            if (!NameChiTietQuyDinhExists(tbChiTietQuyDinh.QuyDinhId))
            {
                _context.Add(tbChiTietQuyDinh);
                await _context.SaveChangesAsync();
            }
            else
            {
                TempData["ErrorMessage"] = "Đã tồn tại quy định, vui lòng nhập tên khác";
            }
            ViewData["CuocThiId"] = new SelectList(_context.tbCuocThi, "Id", "TenCuocThi", tbChiTietQuyDinh.CuocThiId);
            ViewData["QuyDinhId"] = new SelectList(_context.tbQuyDinh, "Id", "TenQuyDinh", tbChiTietQuyDinh.QuyDinhId);
            TempData["SuccessMessage"] = "Đã thêm quy định thành công.";
            return View(tbChiTietQuyDinh);
        }

        private bool NameChiTietQuyDinhExists(int id)
        {
            return _context.tbChiTietQuyDinh.Any(e => e.QuyDinhId == id);
        }

        // GET: Admin/ChiTietQuyDinhs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbChiTietQuyDinh = await _context.tbChiTietQuyDinh.FindAsync(id);
            if (tbChiTietQuyDinh == null)
            {
                return NotFound();
            }
            ViewData["CuocThiId"] = new SelectList(_context.tbCuocThi, "Id", "Id", tbChiTietQuyDinh.CuocThiId);
            ViewData["QuyDinhId"] = new SelectList(_context.tbQuyDinh, "Id", "Id", tbChiTietQuyDinh.QuyDinhId);
            return View(tbChiTietQuyDinh);
        }

        // POST: Admin/ChiTietQuyDinhs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CuocThiId,QuyDinhId")] tbChiTietQuyDinh tbChiTietQuyDinh)
        {
            if (id != tbChiTietQuyDinh.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbChiTietQuyDinh);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!tbChiTietQuyDinhExists(tbChiTietQuyDinh.Id))
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
            ViewData["CuocThiId"] = new SelectList(_context.tbCuocThi, "Id", "Id", tbChiTietQuyDinh.CuocThiId);
            ViewData["QuyDinhId"] = new SelectList(_context.tbQuyDinh, "Id", "Id", tbChiTietQuyDinh.QuyDinhId);
            return View(tbChiTietQuyDinh);
        }

        // GET: Admin/ChiTietQuyDinhs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbChiTietQuyDinh = await _context.tbChiTietQuyDinh
                .Include(t => t.CuocThi)
                .Include(t => t.QuyDinh)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tbChiTietQuyDinh == null)
            {
                return NotFound();
            }

            return View(tbChiTietQuyDinh);
        }

        // POST: Admin/ChiTietQuyDinhs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tbChiTietQuyDinh = await _context.tbChiTietQuyDinh.FindAsync(id);
            if (tbChiTietQuyDinh != null)
            {
                _context.tbChiTietQuyDinh.Remove(tbChiTietQuyDinh);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool tbChiTietQuyDinhExists(int id)
        {
            return _context.tbChiTietQuyDinh.Any(e => e.Id == id);
        }
    }
}
