using Microsoft.EntityFrameworkCore;
using TdpLab07_Lesson9.Areas.Admins.Models;

namespace TdpLab07_Lesson9.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Khai báo DbSet quản lý bảng Category
        public DbSet<Category> Categories { get; set; }
        public DbSet<TdpLab07_Lesson9.Areas.Admins.Models.Product> Products { get; set; }
    }
}