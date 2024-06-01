using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DoAnCoSo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace DoAnCoSo.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public HomeController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _context = context;
            _userManager = userManager;
        }


        // GET: Users/Edit/{id}
        public async Task<IActionResult> Edit(string userId)
        {

            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                return NotFound();
            }

            var model = new EditUserViewModel
            {
                Id = user.Id,
                CCCD = user.CCCD,
                HoTen = user.HoTen,
                NgaySinh = user.NgaySinh,
                DiaChi = user.DiaChi,
                GioiTinh = user.GioiTinh,
                SoDienThoai = user.SoDienThoai,
                ImageUrl = user.ImageUrl,
                TruongId = user.TruongId
            };

            return View(model);
        }

        private async Task<string> SaveImage(IFormFile image)
        {
            var savePath = Path.Combine("wwwroot/images", image.FileName); //Thay đổi đường dẫn theo cấu hình của bạn

            using (var fileStream = new FileStream(savePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }
            return "/images/" + image.FileName; // Trả về đường dẫn tương đối
        }

        // POST: Users/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditUserViewModel model, IFormFile imageUrl)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == model.Id);
            var model1 = new EditUserViewModel
            {
                Id = user.Id,
                CCCD = user.CCCD,
                HoTen = user.HoTen,
                NgaySinh = user.NgaySinh,
                DiaChi = user.DiaChi,
                GioiTinh = user.GioiTinh,
                SoDienThoai = user.SoDienThoai,
                ImageUrl = user.ImageUrl,
                TruongId = user.TruongId
            };
            if (!TonTaisdt(model.SoDienThoai, model.Id))
            {
                if (user == null)
                {
                    return NotFound();
                }

                if (imageUrl == null)
                {
                    model.ImageUrl = user.ImageUrl;
                }
                else
                {
                    // Lưu hình ảnh mới
                    model.ImageUrl = await SaveImage(imageUrl);
                }
                user.CCCD = model.CCCD;
                user.HoTen = model.HoTen;
                user.NgaySinh = model.NgaySinh;
                user.DiaChi = model.DiaChi;
                user.GioiTinh = model.GioiTinh;
                user.SoDienThoai = model.SoDienThoai;
                user.ImageUrl = model.ImageUrl;
                user.TruongId = model.TruongId;

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    TempData["SuccessMessage"] = "Cập nhật thông tin thành công!";
                    return RedirectToAction("DanhSachUser", "PhieuDangKies", new { area = "Admin" });
                }
            }
            TempData["ErrorMessage"] = "Số điện thoại này đã được sử dụng cho tài khoản khác!";
            return View(model1);
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.SoLuongUser = await _userManager.Users.CountAsync();
            ViewBag.SoLuongUserMoi = await _userManager.Users.CountAsync(u => !u.HasBeenViewed);

            ViewBag.SoLuongCuocThi = await _context.tbCuocThi.CountAsync();
            ViewBag.SoLuongUserDK = await _context.tbPhieuDangKy.Select(p => p.UserId).CountAsync();

            var listCapHocUser = _context.Users
                .Select(u => u.Truong.LoaiTruongId)
                .Select(id => _context.tbLoaiTruong.FirstOrDefault(lt => lt.Id == id))
                .Select(lt => lt.Id)
                .Where(lt => lt != null)
                .ToList();

            var listCapHoc = _context.tbLoaiTruong.Select(tl => tl.TenLoaiTruong).ToList();
            ViewBag.listTenLoaiTruong = listCapHoc;
            return View(listCapHocUser);
        }

        public async Task<IActionResult> DanhSachUser(string sortOrder)
        {
            var newUsers = await _userManager.Users.Where(u => !u.HasBeenViewed).ToListAsync();
            foreach (var user in newUsers)
            {
                user.HasBeenViewed = true;
            }
            await _context.SaveChangesAsync();

            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            var users = from u in _context.Users.Include(u => u.Truong)
                        select u;

            switch (sortOrder)
            {
                case "name_desc":
                    users = users.OrderByDescending(u => u.HoTen);
                    break;
                default:
                    users = users.OrderBy(u => u.HoTen);
                    break;
            }

            return View(users.ToList());
        }

        [HttpGet]
        public IActionResult GetListLoaiTruong()
        {
            var listCapHocUser = _context.Users
                .Select(u => u.Truong.LoaiTruongId)
                .Select(id => _context.tbLoaiTruong.FirstOrDefault(lt => lt.Id == id))
                .Select(lt => lt.Id)
                .Where(lt => lt != null)
                .ToList();

            return Json(listCapHocUser);
        }

        private bool TonTaisdt(string sdt, string userId)
        {

            var usersdt = _context.Users.FirstOrDefault(p => p.SoDienThoai == sdt && p.Id != userId);
            if(usersdt != null)
                return true;
            return false;
        }
    }
}
