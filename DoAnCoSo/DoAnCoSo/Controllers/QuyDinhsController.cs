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

namespace DoAnCoSo.Controllers {
    public class QuyDinhsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public QuyDinhsController(ApplicationDbContext context)
        {
            _context = context;
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
    }
        
}
