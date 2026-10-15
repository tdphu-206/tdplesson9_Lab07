using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TdpLab07_Lesson9.Data;
using TdpLab07_Lesson9.Areas.Admins.Models;

namespace TdpLab07_Lesson9.Areas.Admins.Controllers
{
    [Area("Admins")]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 1. XEM DANH SÁCH SẢN PHẨM (INDEX)
        public async Task<IActionResult> Index()
        {
            // Sử dụng .Include() để lấy kèm thông tin bảng Category (Khóa ngoại)
            var products = await _context.Products.Include(p => p.Category).ToListAsync();
            return View(products);
        }
        // 2. HIỂN THỊ FORM THÊM MỚI SẢN PHẨM (CREATE - GET)
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            // Lấy danh sách Category đổ vào ViewBag để hiển thị thẻ <select> trên giao diện
            ViewBag.Categories = await _context.Categories.ToListAsync();
            return View();
        }

        // 3. XỬ LÝ LƯU SẢN PHẨM MỚI (CREATE - POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                product.CreatedDate = DateTime.Now;
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Nếu dữ liệu không hợp lệ, load lại danh sách Category để không bị mất dropdown
            ViewBag.Categories = await _context.Categories.ToListAsync();
            return View(product);
        }
        // 4. HIỂN THỊ FORM SỬA SẢN PHẨM (EDIT - GET)
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            // Đổ danh sách Category ra ViewBag để hiển thị dropdown chọn danh mục
            ViewBag.Categories = await _context.Categories.ToListAsync();
            return View(product);
        }

        // 5. XỬ LÝ CẬP NHẬT SẢN PHẨM (EDIT - POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
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

            ViewBag.Categories = await _context.Categories.ToListAsync();
            return View(product);
        }

        // Hàm kiểm tra sự tồn tại của sản phẩm
        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
        // 6. XÓA SẢN PHẨM (DELETE)
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}