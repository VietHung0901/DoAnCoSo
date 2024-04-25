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
using Microsoft.AspNetCore.Identity;

namespace DoAnCoSo.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class TruongsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ITruongRepository _truongRepository;
        private readonly ILoaiTruongRepository _loaitruongRepository;
        private readonly IUserRepository _userRepository;


        public TruongsController(ApplicationDbContext context, ITruongRepository truongRepository, ILoaiTruongRepository loaitruongRepository, IUserRepository userRepository)
        {
            _context = context;
            _truongRepository = truongRepository;
            _loaitruongRepository = loaitruongRepository;
            _userRepository = userRepository;
        }

        // GET: Admin/Truongs
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.tbTruong.Include(t => t.LoaiTruong);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/Truongs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbTruong = await _context.tbTruong
                .Include(t => t.LoaiTruong)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tbTruong == null)
            {
                return NotFound();
            }

            return View(tbTruong);
        }

        // GET: Admin/Truongs/Create
        public async Task<IActionResult> Create()
        {
            var tbLoaiTruong = await _loaitruongRepository.GetAllAsync();
            ViewBag.LoaiTruongName = new SelectList(tbLoaiTruong, "Id", "TenLoaiTruong");
            return View();
        }

        // POST: Admin/Truongs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TenTruong,LoaiTruongId")] tbTruong tbTruong)
        {
            //Kiểm tra tên trường thuộc loại trường có tồn tại chưa
            if (!tontaiCungTruongCungLoaiTruong(tbTruong))
            {
                _context.tbTruong.Add(tbTruong);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //Chưa xử lý được việc xuất thông báo "Tên trường thuộc loại trường đã tồn tại"
            var tbLoaiTruong = await _loaitruongRepository.GetAllAsync();
            ViewBag.LoaiTruongName = new SelectList(tbLoaiTruong, "Id", "TenLoaiTruong");
            return View(tbTruong);
        }

        // GET: Admin/Truongs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbTruong = await _context.tbTruong.FindAsync(id);
            if (tbTruong == null)
            {
                return NotFound();
            }
            var tbLoaiTruong = await _loaitruongRepository.GetAllAsync();
            ViewBag.LoaiTruongName = new SelectList(tbLoaiTruong, "Id", "TenLoaiTruong");
            return View(tbTruong);
        }

        // POST: Admin/Truongs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TenTruong,LoaiTruongId")] tbTruong tbTruong)
        {
            if (id != tbTruong.Id)
            {
                return NotFound();
            }

            if (!tontaiCungTruongCungLoaiTruong(tbTruong))
            {
                try
                {
                    _context.Update(tbTruong);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!tbTruongExists(tbTruong.Id))
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
            //Chưa xử lý được việc xuất thông báo "Tên trường thuộc loại trường đã tồn tại"
            var tbLoaiTruong = await _loaitruongRepository.GetAllAsync();
            ViewBag.LoaiTruongName = new SelectList(tbLoaiTruong, "Id", "TenLoaiTruong");
            return View(tbTruong);
        }

        // GET: Admin/Truongs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbTruong = await _context.tbTruong
                .Include(t => t.LoaiTruong)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tbTruong == null)
            {
                return NotFound();
            }

            return View(tbTruong);
        }

        // POST: Admin/Truongs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //Kiểm tra có User nào tồn tại trong Truong.Id == id
            if(!await UserExists(id))
            {
                var tbTruong = await _context.tbTruong.FindAsync(id);
                if (tbTruong != null)
                {
                    _context.tbTruong.Remove(tbTruong);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return Content("Tồn tại user đang học trường này");
        }

        private bool tbTruongExists(int id)
        {
            return _context.tbTruong.Any(e => e.Id == id);
        }

        private bool NameTruongExists(string name)
        {
            return _context.tbTruong.Any(e => e.TenTruong == name);
        }

        //Hàm kiểm tra tên trường thuộc loại trường có tồn tại chưa
        private bool tontaiCungTruongCungLoaiTruong(tbTruong truong)
        {
            return _context.tbTruong.Any(e => e.LoaiTruongId == truong.LoaiTruongId && e.TenTruong == truong.TenTruong && e.Id != truong.Id);
        }

        //Hàm kiểm tra Truong.Id có tồn tại User
        private async Task<bool> UserExists(int id)
        {
            var users = await _userRepository.GetList();
            return users.Any(user => user.TruongId == id);
        }
    }
}
