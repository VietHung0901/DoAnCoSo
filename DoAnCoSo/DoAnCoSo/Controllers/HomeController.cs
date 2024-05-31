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

namespace DoAnCoSo.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public HomeController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        // GET: Users/Edit/{id}
        public async Task<IActionResult> Edit()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("https://localhost:7107/Identity/Account/Login");
            }

            var user = await _userManager.GetUserAsync(User);
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
            var user = await _userManager.GetUserAsync(User);
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
                    return View(model);
                }
            }
            TempData["ErrorMessage"] = "Số điện thoại này đã được sử dụng cho tài khoản khác!";
            return View(model1);
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
