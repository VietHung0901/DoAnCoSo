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
    public class PhieuKetQuasController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;


        public PhieuKetQuasController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _context = context;
            _userManager = userManager;
        }
        //Trả về tất cả các phiếu kết quả thuộc user
        //public async Task<IActionResult> Index2()
        //{
        //    if (!User.Identity.IsAuthenticated)
        //    {
        //        return Redirect("https://localhost:7107/Identity/Account/Login"); // Chuyển hướng đến trang đăng nhập
        //    }
        //    else
        //    {
        //        var user = await _userManager.GetUserAsync(User);
        //        var applicationDbContext = _context.tbPhieuKetQua.Where(p => p.tbPh.UserId == user.Id)
        //                                                    .Include(t => t.CuocThi)
        //                                                    .Include(t => t.User);
        //        return View(await applicationDbContext.ToListAsync());
        //    }
        //}

        //Trả về kết quả theo cuộc thi
        public async Task<IActionResult> Ketqua(int cuocThiId)
        {
            var applicationDbContext = _context.tbPhieuKetQua
                .Where(p => p.PhieuDangKy.CuocThiId == cuocThiId)
                .Include(t => t.PhieuDangKy)
                .OrderByDescending(p => p.Diem) // Sắp xếp giảm dần theo trường Diem
                .ThenBy(p => p.Phut) // Sắp xếp tăng dần theo trường Phut (nếu điểm bằng nhau)
                .ThenBy(p => p.Giay); // Sắp xếp tăng dần theo trường Giay (nếu điểm và phút bằng nhau)

            return View(await applicationDbContext.ToListAsync());
        }
    }
}
