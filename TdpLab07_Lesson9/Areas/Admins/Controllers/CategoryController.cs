using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TdpLab07_Lesson9.Areas.Admins.Models;
using TdpLab07_Lesson9.Data;

namespace TdpLab07_Lesson9.Areas.Admins.Controllers
{
    [Area("Admins")]
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 1. HIỂN THỊ DANH SÁCH (READ)
        public async Task<IActionResult> Index()
        {
            var categories = await _context.Categories.ToListAsync();
            return View(categories);
        }

        // 2. HIỂN THỊ FORM THÊM MỚI (CREATE - GET)
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // 3. XỬ LÝ DỮ LIỆU THÊM MỚI (CREATE - POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            category.CreatedDate = DateTime.Now;

            // Kiểm tra và in lỗi ra cửa sổ Output của Visual Studio nếu ModelState không hợp lệ
            if (!ModelState.IsValid)
            {
                foreach (var modelStateKey in ModelState.Keys)
                {
                    var modelStateVal = ModelState[modelStateKey];
                    foreach (var error in modelStateVal.Errors)
                    {
                        System.Diagnostics.Debug.WriteLine($"--- LỖI VALIDATION TẠI [{modelStateKey}]: {error.ErrorMessage} ---");
                    }
                }
                return View(category); // Trả lại form kèm theo lỗi
            }

            _context.Add(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        // 4. HIỂN THỊ FORM SỬA (EDIT - GET)
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // 5. XỬ LÝ DỮ LIỆU SỬA (EDIT - POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
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
            return View(category);
        }

        // Hàm hỗ trợ kiểm tra sự tồn tại của Category
        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
        // 6. XÓA DANH MỤC (DELETE)
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Tìm danh mục cần xóa theo Id
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            // Tiến hành xóa khỏi DbSet
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            // Xóa thành công, chuyển hướng về trang danh sách
            return RedirectToAction(nameof(Index));
        }
    }
}